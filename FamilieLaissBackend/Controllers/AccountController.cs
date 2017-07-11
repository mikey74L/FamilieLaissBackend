using FamilieLaissBackend.Helper;
using FamilieLaissBackend.Interfaces;
using FamilieLaissBackend.Model.Account;
using FamilieLaissBackend.Resources;
using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FamilieLaissBackend.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        #region Private Members
        private static Logger logger;
        private IUserOperations _repo;
        #endregion

        #region C'tor
        public AccountController(IUserOperations userOperations)
        {
            //Das Repo über DI übernehmen
            _repo = userOperations;

            //Den Logger ermitteln
            logger = LogManager.GetLogger("AuthLogger");
        }
        #endregion

        #region Register
        //Neuen User registrieren
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody]RegisterUserDTO model)
        {
            try
            {
                //Logger-Info ausgeben
                logger.Info("Register called");
                if (model == null)
                {
                    logger.Info("userModel is null");
                }
                else
                {
                    logger.Info("Name            : {0}", model.UserName);
                    logger.Info("eMail           : {0}", model.eMail);
                    logger.Info("Gender          : {0}", model.Gender);
                    logger.Info("FirstName       : {0}", model.FirstName);
                    logger.Info("FamilyName      : {0}", model.FamilyName);
                    logger.Info("Street          : {0}", model.Street);
                    logger.Info("HNR             : {0}", model.HNR);
                    logger.Info("PLZ             : {0}", model.PLZ);
                    logger.Info("City            : {0}", model.City);
                    logger.Info("Country         : {0}", model.Country);
                    logger.Info("SecurityQuestion: {0}", model.SecurityQuestion);
                    logger.Info("SecurityAnswer  : {0}", model.SecurityAnswer);
                }

                //Überprüfen ob ein valides Registrier-Model übergeben wurde
                if (!ModelState.IsValid)
                {
                    logger.Info("ModelState is not valid");
                    return BadRequest(ModelState);
                }

                //Registrieren des Users bei ASP.NET-Identity
                logger.Info("Register user");
                IdentityResult result = await _repo.RegisterUser(model);

                //Auswerten des Fehlers
                IHttpActionResult errorResult = GetErrorResult(result);

                //Wenn ein Fehler aufgetreten ist, dann wird dieser zurückgemeldet
                if (errorResult != null)
                {
                    logger.Error("User could not be registered");
                    return errorResult;
                }

                //Hinzufügen der Standard-Rolle
                logger.Info("Add user to role User");
                IdentityResult roleResult = await _repo.AddToRole(model.UserName, "User");

                //Auswerten des Fehlers
                errorResult = GetErrorResult(roleResult);

                //Wenn ein Fehler aufgetreten ist, dann wird dieser zurückgemeldet
                if (errorResult != null)
                {
                    logger.Error("User could not be added to role");
                    return errorResult;
                }

                //Ermitteln des Mail-Confirmation-Tokens
                logger.Info("Getting mailToken");
                string mailToken = await _repo.GetMailConfirmationToken(model.UserName);
                logger.Info("MailToken: {0}", mailToken);

                //Ermitteln der URL für die Bestätigung des eMail-Accounts
                logger.Info("Getting URL for confirmation");
                string callbackUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/" +
                    string.Format("Index.html#/{0}/{1}/{2}", "confirmaccount", model.UserName, mailToken);
                logger.Info("URL for confirmation: {0}", callbackUrl);

                //Ermitteln des Users
                IdentityUserExtended currentUser = await _repo.FindUser(model.UserName);

                //Zusammenstellen des Notification-Models
                logger.Info("Setting Notification-Model with following data:");
                AccountNotificationModel notificationUser = new AccountNotificationModel(model.Country, model.SecurityQuestion)
                {
                    UserID = currentUser.Id,
                    VerificationToken = mailToken,
                    URL = callbackUrl,
                };
                AutoMapper.Mapper.Map<IdentityUserExtended, AccountNotificationModel>(currentUser, notificationUser);
                logger.Info("UserID          : {0}", notificationUser.UserID);
                logger.Info("Name            : {0}", notificationUser.UserName);
                logger.Info("eMail           : {0}", notificationUser.Email);
                logger.Info("Geschlecht      : {0}", notificationUser.Geschlecht);
                logger.Info("Vorname         : {0}", notificationUser.Vorname);
                logger.Info("Familienname    : {0}", notificationUser.Familienname);
                logger.Info("Strasse         : {0}", notificationUser.Strasse);
                logger.Info("HNR             : {0}", notificationUser.HNR);
                logger.Info("PLZ             : {0}", notificationUser.PLZ);
                logger.Info("Ort             : {0}", notificationUser.Stadt);
                logger.Info("Land            : {0}", notificationUser.Land);
                logger.Info("SecurityQuestion: {0}", notificationUser.SecurityQuestion);
                logger.Info("SecurityAnswer  : {0}", notificationUser.SecurityAnswer);

                //Ermitteln des Mail-Subject
                logger.Info("Getting Mail-Subject");
                string subject = FamilieLaissBackend.Resources.ConfirmationMail_Resources.Mail_Subject;

                //Ermitteln des Mail-Inhaltes mit Hilfe des Rendern einer Razor-View
                logger.Info("Rendering view for mail content");
                string body = ViewRenderer.RenderView("~/Views/Mailer/NewAccount.cshtml", notificationUser);

                //Senden einer Mail an den User
                logger.Info("Sending confirmation mail");
                await _repo.SendMailToUser(model.UserName, subject, body);

                //Return OK to client
                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unexpected exception occured while registering");
                return InternalServerError();
            }
        }
        #endregion

        #region Confirm Account per eMail from User
        //Bestätigen des Benutzer-Accounts
        [AllowAnonymous]
        [Route("ConfirmAccount")]
        [HttpPost]
        public async Task<IHttpActionResult> ConfirmAccount([FromBody]ConfirmAccountDTO model)
        {
            try
            {
                //Logger-Info ausgeben
                logger.Info("ConfirmAccount called");

                //Ermitteln des Users
                logger.Info("Get userAccount by username");
                IdentityUserExtended user = await _repo.FindUser(model.UserName);

                //Bestätigung des Accounts
                logger.Info("Confirming account");
                bool result = await _repo.ConfirmMail(user.Id, model.Token);

                //Für alle Administratoren eine Mail verschicken
                try
                {
                    //Ermitteln des Users
                    logger.Info("Get user data from database");
                    IdentityUserExtended currentUser = await _repo.FindUser(model.UserName);

                    //Zusammenstellen des Notification-Models
                    logger.Info("Setting Notification-Model with following data:");
                    AccountNotificationModel notifyModel = new AccountNotificationModel(currentUser.Stadt, currentUser.SecurityQuestion);
                    AutoMapper.Mapper.Map<IdentityUserExtended, AccountNotificationModel>(currentUser, notifyModel);
                    logger.Info("UserID          : {0}", notifyModel.UserID);
                    logger.Info("Name            : {0}", notifyModel.UserName);
                    logger.Info("eMail           : {0}", notifyModel.Email);
                    logger.Info("Geschlecht      : {0}", notifyModel.Geschlecht);
                    logger.Info("Vorname         : {0}", notifyModel.Vorname);
                    logger.Info("Familienname    : {0}", notifyModel.Familienname);
                    logger.Info("Strasse         : {0}", notifyModel.Strasse);
                    logger.Info("HNR             : {0}", notifyModel.HNR);
                    logger.Info("PLZ             : {0}", notifyModel.PLZ);
                    logger.Info("Ort             : {0}", notifyModel.Stadt);
                    logger.Info("Land            : {0}", notifyModel.Land);
                    logger.Info("SecurityQuestion: {0}", notifyModel.SecurityQuestion);
                    logger.Info("SecurityAnswer  : {0}", notifyModel.SecurityAnswer);

                    //Ermitteln des Mail-Subject
                    logger.Info("Getting Mail-Subject");
                    string subject = FamilieLaissBackend.Resources.AdminConfirmAccount_Resources.Mail_Subject;

                    //Für jeden Administrator die entsprechende Mail versenden
                    foreach (IdentityUserExtended AdminUser in await _repo.GetAllAdministrators())
                    {
                        //Ermitteln des Mail-Inhaltes
                        logger.Info("Rendering view for mail content");
                        string body = ViewRenderer.RenderView("~/Views/Mailer/AdminConfirmAccount.cshtml", notifyModel);

                        //Senden einer Mail an den User
                        logger.Info("Sending mail to admin");
                        await _repo.SendMailToUser(AdminUser.UserName, subject, body);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Could not send mail to admin user");
                }

                //Funktionsergebnis
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unexpected exception occured while confirming account");
                return InternalServerError();
            }
        }
        #endregion

        #region Reset Password 
        [AllowAnonymous]
        [Route("CreatePasswordResetToken")]
        [HttpPost]
        public async Task<IHttpActionResult> CreatePasswordResetToken([FromBody]ForgotPasswordDTO model)
        {
            //Ermitteln des Users
            IdentityUserExtended user = await _repo.FindUserByMail(model.eMail);

            //Wenn die eMail gefunden wurde, dann weitermachen
            if (user != null)
            {
                //Ermitteln ob Sicherheitsfrage und Antwort stimmen
                if (user.SecurityQuestion == model.SecurityQuestion && user.SecurityAnswer == model.SecurityAnswer)
                {
                    //Ermitteln des Tokens
                    string ResetToken = await _repo.GeneratePasswordToken(user.Id);

                    //Wenn Generierung des Tokens erfolgreich dann wird eine Mail an den User versendet
                    if (!string.IsNullOrEmpty(ResetToken))
                    {
                        try
                        {
                            //Zusammenstellen des Notification-Models
                            AccountNotificationModel notifyModel = new AccountNotificationModel(user.Land, user.SecurityQuestion)
                            {
                                VerificationToken = ResetToken
                            };
                            AutoMapper.Mapper.Map<IdentityUserExtended, AccountNotificationModel>(user, notifyModel);
                            logger.Info("UserID          : {0}", notifyModel.UserID);
                            logger.Info("Name            : {0}", notifyModel.UserName);
                            logger.Info("eMail           : {0}", notifyModel.Email);
                            logger.Info("Geschlecht      : {0}", notifyModel.Geschlecht);
                            logger.Info("Vorname         : {0}", notifyModel.Vorname);
                            logger.Info("Familienname    : {0}", notifyModel.Familienname);
                            logger.Info("Strasse         : {0}", notifyModel.Strasse);
                            logger.Info("HNR             : {0}", notifyModel.HNR);
                            logger.Info("PLZ             : {0}", notifyModel.PLZ);
                            logger.Info("Ort             : {0}", notifyModel.Stadt);
                            logger.Info("Land            : {0}", notifyModel.Land);
                            logger.Info("SecurityQuestion: {0}", notifyModel.SecurityQuestion);
                            logger.Info("SecurityAnswer  : {0}", notifyModel.SecurityAnswer);

                            //Ermitteln des Mail-Subject
                            string subject = FamilieLaissBackend.Resources.PasswordResetToken_Resources.Mail_Subject;

                            //Zusammenstellen der URL
                            notifyModel.URL = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/" +
                                string.Format("Index.html#/{0}/{1}/{2}", "changePassword", notifyModel.UserName, ResetToken);

                            //Ermitteln des Mail-Inhaltes
                            string body = ViewRenderer.RenderView("~/Views/Mailer/ResetPassword.cshtml", notifyModel);

                            //Senden einer Mail an den User
                            await _repo.SendMailToUser(user.UserName, subject, body);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Could not send mail to user");
                        }

                        //OK zurückmelden
                        return Ok();
                    }
                    else
                    {
                        //Fehler beim Generieren des Tokens
                        ModelState.AddModelError("Error_Token", PasswordResetToken_Resources.Mail_Error);
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    //Falsche Security-Question oder Security-Answer
                    ModelState.AddModelError("Error_Security", PasswordResetToken_Resources.Wrong_Security);
                    return BadRequest(ModelState);
                }
            }
            else
            {
                //Fehler falsche eMail
                ModelState.AddModelError("Error_Mail", PasswordResetToken_Resources.Wrong_Mail);
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region New Password
        [AllowAnonymous]
        [Route("NewPassword")]
        [HttpPost]
        public async Task<IHttpActionResult> NewPassword([FromBody]NewPasswordDTO model)
        {
            //Deklaration
            IdentityResult Result = await _repo.SetNewPassword(model);

            //Auswerten des Results
            if (Result.Errors.Count() > 0)
            {
                //Es ist ein Fehler aufgetreten
                return BadRequest();
            }
            else
            {
                //Alles OK. Passwort wurde geändert
                return Ok();
            }
        }
        #endregion

        #region Allow Account
        [Authorize]
        [Route("AllowAccount")]
        [HttpPost]
        public async Task<IHttpActionResult> AllowAccount([FromBody]AllowAccountDTO model)
        {
            //Logger-Info ausgeben
            logger.Info("AllowAccount for user '{0}' called", model.UserName);

            //Ausführen des Updates
            IdentityResult Result = await _repo.LockUnlockAccount(model.UserName, true);

            try
            {
                //Ermitteln des Users
                logger.Info("Get user data from database");
                IdentityUserExtended currentUser = await _repo.FindUser(model.UserName);

                //Zusammenstellen des Notification-Models
                logger.Info("Setting Notification-Model with following data:");
                AccountNotificationModel notifyModel = new AccountNotificationModel(currentUser.Land, currentUser.SecurityQuestion);
                AutoMapper.Mapper.Map<IdentityUserExtended, AccountNotificationModel>(currentUser, notifyModel);
                logger.Info("UserID          : {0}", notifyModel.UserID);
                logger.Info("Name            : {0}", notifyModel.UserName);
                logger.Info("eMail           : {0}", notifyModel.Email);
                logger.Info("Geschlecht      : {0}", notifyModel.Geschlecht);
                logger.Info("Vorname         : {0}", notifyModel.Vorname);
                logger.Info("Familienname    : {0}", notifyModel.Familienname);
                logger.Info("Strasse         : {0}", notifyModel.Strasse);
                logger.Info("HNR             : {0}", notifyModel.HNR);
                logger.Info("PLZ             : {0}", notifyModel.PLZ);
                logger.Info("Ort             : {0}", notifyModel.Stadt);
                logger.Info("Land            : {0}", notifyModel.Land);
                logger.Info("SecurityQuestion: {0}", notifyModel.SecurityQuestion);
                logger.Info("SecurityAnswer  : {0}", notifyModel.SecurityAnswer);

                //Ermitteln des Mail-Subject
                logger.Info("Getting Mail-Subject");
                string subject = FamilieLaissBackend.Resources.UnlockAccountMail_Resources.Mail_Subject_Unlock;

                //Ermitteln des Mail-Inhaltes
                logger.Info("Rendering view for mail content");
                string body = ViewRenderer.RenderView("~/Views/Mailer/UnlockAccount.cshtml", notifyModel);

                //Senden einer Mail an den User
                logger.Info("Sending confirmation mail");
                await _repo.SendMailToUser(model.UserName, subject, body);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Could not send mail to user");
            }

            //Auswerten des Results
            if (Result.Errors.Count() > 0)
            {
                logger.Info("AllowAccount with errors executed");
                return BadRequest();
            }
            else
            {
                logger.Info("AllowAccount successfully done");
                return Ok();
            }
        }
        #endregion

        #region Lock Account
        [Authorize]
        [Route("LockAccount")]
        [HttpPost]
        public async Task<IHttpActionResult> LockAccount([FromBody]LockAccountDTO model)
        {
            //Logger-Info ausgeben
            logger.Info("LockAccount for user '{0}' called", model.UserName);

            //Ausführen des Updates
            IdentityResult Result = await _repo.LockUnlockAccount(model.UserName, false);

            try
            {
                //Ermitteln des Users
                logger.Info("Get user data from database");
                IdentityUserExtended currentUser = await _repo.FindUser(model.UserName);

                //Zusammenstellen des Notification-Models
                logger.Info("Setting Notification-Model with following data:");
                AccountNotificationModel notifyModel = new AccountNotificationModel(currentUser.Land, currentUser.SecurityQuestion);
                AutoMapper.Mapper.Map<IdentityUserExtended, AccountNotificationModel>(currentUser, notifyModel);
                logger.Info("UserID          : {0}", notifyModel.UserID);
                logger.Info("Name            : {0}", notifyModel.UserName);
                logger.Info("eMail           : {0}", notifyModel.Email);
                logger.Info("Geschlecht      : {0}", notifyModel.Geschlecht);
                logger.Info("Vorname         : {0}", notifyModel.Vorname);
                logger.Info("Familienname    : {0}", notifyModel.Familienname);
                logger.Info("Strasse         : {0}", notifyModel.Strasse);
                logger.Info("HNR             : {0}", notifyModel.HNR);
                logger.Info("PLZ             : {0}", notifyModel.PLZ);
                logger.Info("Ort             : {0}", notifyModel.Stadt);
                logger.Info("Land            : {0}", notifyModel.Land);
                logger.Info("SecurityQuestion: {0}", notifyModel.SecurityQuestion);
                logger.Info("SecurityAnswer  : {0}", notifyModel.SecurityAnswer);

                //Ermitteln des Mail-Subject
                logger.Info("Getting Mail-Subject");
                string subject = FamilieLaissBackend.Resources.UnlockAccountMail_Resources.Mail_Subject_Lock;

                //Ermitteln des Mail-Inhaltes
                logger.Info("Rendering view for mail content");
                string body = ViewRenderer.RenderView("~/Views/Mailer/LockAccount.cshtml", notifyModel);

                //Senden einer Mail an den User
                logger.Info("Sending confirmation mail");
                await _repo.SendMailToUser(model.UserName, subject, body);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Could not send mail to user");
            }

            //Auswerten des Results
            if (Result.Errors.Count() > 0)
            {
                logger.Info("LockAccount with errors executed");
                return BadRequest();
            }
            else
            {
                logger.Info("LockAccount successfully done");
                return Ok();
            }
        }
        #endregion

        #region Delete Account
        [Authorize]
        [Route("DeleteAccount")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteAccount([FromBody]DeleteAccountDTO model)
        {
            //Logger-Info ausgeben
            logger.Info("DeleteAccount for user '{0}' called", model.UserName);

            //Ausführen des Delete
            IdentityResult Result = await _repo.DeleteAccount(model.UserName);

            //Auswerten des Results
            if (Result.Errors.Count() > 0)
            {
                logger.Info("DeleteAccount with errors executed");
                return BadRequest();
            }
            else
            {
                logger.Info("DeleteAccount successfully done");
                return Ok();
            }
        }
        #endregion

        #region Get User Info
        //Ermitteln der UserInfo die für den Authorization Helper benötigt wird
        [Authorize]
        [Route("GetUserInfo")]
        [HttpGet]
        public async Task<UserInfoDTO> GetUserInfo()
        {
            try
            {
                //Logger-Info ausgeben
                logger.Info("GetUserName called");

                //Ermitteln des Users
                logger.Info("Get userAccount by username");
                IdentityUserExtended user = await _repo.FindUser(this.User.Identity.Name);

                //Ermitteln der Rollen
                IList<string> Roles = null;
                Roles = await _repo.GetRolesForUser(user.Id);

                //Zurückliefern des Funktionsergebnisses
                if (user != null)
                {
                    return new UserInfoDTO()
                    {
                        UserName = this.User.Identity.Name,
                        FirstName = user.Vorname,
                        FamilyName = user.Familienname,
                        Roles = String.Join(",", Roles.ToArray())
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unexpected exception occured while getting username");
                return null;
            }
        }
        #endregion

        #region Private Methods
        //Ermitteln des Fehler-Result-Wertes
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            //Wenn das Result null ist, dann ist ein interner Server-Fehler aufgetreten
            if (result == null)
            {
                return InternalServerError();
            }

            //Überprüfen des Identity Fehlers
            if (!result.Succeeded)
            {
                //Auswerten der Model-Fehler
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    //Keine Modelfehler gefunden, dann einen Bad-Request zurückliefern
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
        #endregion
    }
}
