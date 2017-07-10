using FamilieLaissBackend.Helper;
using FamilieLaissBackend.Interfaces;
using FamilieLaissBackend.Model.Account;
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
                AccountNotificationModel notificationUser = new AccountNotificationModel(model.Gender, model.Country, model.SecurityQuestion)
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
