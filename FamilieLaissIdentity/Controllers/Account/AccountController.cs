using IdentityModel;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4.Events;
using FamilieLaissIdentity.Attributes;
using FamilieLaissIdentity.Service;
using FamilieLaissIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using FamilieLaissIdentity.Interfaces;
using FamilieLaissIdentity.Models.Account;
using FamilieLaissIdentity.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using FamilieLaissIdentity.ViewHelper;
using Microsoft.Extensions.Logging;

namespace FamilieLaissIdentity.Controllers.Account
{
    [Authorize]
    [SecurityHeaders]
    public class AccountController : Controller
    {
        #region Private Members
        private readonly ILogger<AccountController> _Logger;
        private readonly IMapper _Mapper;
        private readonly IUserOperations _UserOperations;
        private readonly ISigninOperations _SigninOperations;
        private readonly Func<string, IMailSender> _MailSenderServiceAccessor;
        private readonly IMailGenerator _MailGenerator;
        private readonly IHostingEnvironment _HostingEnv;
        private readonly IStringLocalizer<AccountController> _Localizer;
        private readonly IStringLocalizer<GenderSelectList> _LocalizerGender;
        private readonly IStringLocalizer<CountrySelectList> _LocalizerCountry;
        private readonly IStringLocalizer<SecurityQuestionList> _LocalizerQuestion;
        #endregion

        #region C'tor
        public AccountController(
            ILogger<AccountController> logger,
            IMapper mapper, 
            IUserOperations userOperations,
            ISigninOperations signInOperations,
            Func<string, IMailSender> mailSenderServiceAccessor, 
            IMailGenerator mailGenerator,
            IHostingEnvironment hostingEnv,
            IStringLocalizer<AccountController> localizer,
            IStringLocalizer<GenderSelectList> localizerGender,
            IStringLocalizer<CountrySelectList> localizerCountry,
            IStringLocalizer<SecurityQuestionList> localizerQuestion)
        {
            //Den Logger übernehmen
            _Logger = logger;

            //Auto-Mapper übernehmen
            _Mapper = mapper;

            //User-Operations übernehmen
            _UserOperations = userOperations;

            //Signin-Operations übernehmen
            _SigninOperations = signInOperations;

            //Übernhemen der Factory für den Mail-Sender-Service
            _MailSenderServiceAccessor = mailSenderServiceAccessor;

            //Übernehmen des Mail-Generator
            _MailGenerator = mailGenerator;

            //Die aktuelle Hosting Umgebung übernehmen
            _HostingEnv = hostingEnv;

            //Die Lokalisierungen übernehmen
            _Localizer = localizer;
            _LocalizerGender = localizerGender;
            _LocalizerCountry = localizerCountry;
            _LocalizerQuestion = localizerQuestion;
        }
        #endregion

        #region Login (Show / Postback)
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            //Das bestehende externe Cookie entfernen um einen saubern Login Prozess zu gewährleisten
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            
            //Model erstellen
            LoginViewModel Model = new LoginViewModel();

            //Die Return-Url in das View-Bag hängen
            ViewBag.ReturnUrl = returnUrl;

            //Die View Rendern
            return View(Model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            //Initialisierung
            ViewBag.IsAllowed = true;
            ViewBag.EMailConfirmed = true;
            ViewBag.IsLockedOut = false;
            ViewBag.GeneralError = false;

            if (ModelState.IsValid)
            {
                //Anmeldung starten
                var Result = await _SigninOperations.SigninWithPassword(model.Username, model.Password);

                //Auswerten des Results
                if (Result.Succeeded)
                {
                    //Anmeldung erfolgreich
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    if (!Result.UsernameOrPasswordWrong)
                    {
                        //Auswerten des Fehlers
                        ViewBag.IsAllowed = Result.IsAllowed;
                        ViewBag.EMailConfirmed = Result.EMailConfirmed;
                        ViewBag.IsLockedOut = Result.IsLockedOut;
                        ViewBag.GeneralError = Result.GeneralError;

                        //Die Error-View rendern
                        return View("LoginError");
                    }
                    else
                    {
                        ModelState.AddModelError("UserPassword", _Localizer["Error_Wrong_Username_Or_Password"]);
                    }
                }
            }

            //Rendern der View
            return View(model);
        }
        #endregion

        //#region Logout (Show / Postback)
        ///// <summary>
        ///// Show logout page
        ///// </summary>
        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IActionResult> Logout(string logoutId)
        //{
        //    var vm = await _account.BuildLogoutViewModelAsync(logoutId);

        //    if (vm.ShowLogoutPrompt == false)
        //    {
        //        // no need to show prompt
        //        return await Logout(vm);
        //    }

        //    return View(vm);
        //}

        ///// <summary>
        ///// Handle logout page postback
        ///// </summary>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        //public async Task<IActionResult> Logout(LogoutViewModel model)
        //{
        //    var vm = await _account.BuildLoggedOutViewModelAsync(model.LogoutId);
        //    if (vm.TriggerExternalSignout)
        //    {
        //        string url = Url.Action("Logout", new { logoutId = vm.LogoutId });
        //        try
        //        {
        //            // hack: try/catch to handle social providers that throw
        //            await HttpContext.Authentication.SignOutAsync(vm.ExternalAuthenticationScheme,
        //                new AuthenticationProperties { RedirectUri = url });
        //        }
        //        catch (NotSupportedException) // this is for the external providers that don't have signout
        //        {
        //        }
        //        catch (InvalidOperationException) // this is for Windows/Negotiate
        //        {
        //        }
        //    }

        //    // delete authentication cookie
        //    await _signInManager.SignOutAsync();

        //    return View("LoggedOut", vm);
        //}
        //#endregion

        #region External Provider Stuff
        ////
        //// POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public IActionResult ExternalLogin(string provider, string returnUrl = null)
        //{
        //    // Request a redirect to the external login provider.
        //    var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
        //    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        //    return Challenge(properties, provider);
        //}

        ////
        //// GET: /Account/ExternalLoginCallback
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        //{
        //    if (remoteError != null)
        //    {
        //        ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
        //        return View(nameof(Login));
        //    }
        //    var info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        return RedirectToAction(nameof(Login));
        //    }

        //    // Sign in the user with this external login provider if the user already has a login.
        //    var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        //    if (result.Succeeded)
        //    {
        //        _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
        //        return RedirectToLocal(returnUrl);
        //    }
        //    if (result.RequiresTwoFactor)
        //    {
        //        return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
        //    }
        //    if (result.IsLockedOut)
        //    {
        //        return View("Lockout");
        //    }
        //    else
        //    {
        //        // If the user does not have an account, then ask the user to create an account.
        //        ViewData["ReturnUrl"] = returnUrl;
        //        ViewData["LoginProvider"] = info.LoginProvider;
        //        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
        //    }
        //}

        ////
        //// POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await _signInManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await _userManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await _userManager.AddLoginAsync(user, info);
        //            if (result.Succeeded)
        //            {
        //                await _signInManager.SignInAsync(user, isPersistent: false);
        //                _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View(model);
        //}
        #endregion

        #region Register (Show / Postback)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            //Hinzufügen der Listendaten für Gender, Country und Questions
            AddListDataToRegisterView(ViewBag, returnUrl);

            //Die View Rendern
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            //Das View-Bag mit der Return-URL bestücken
            ViewBag.ReturnUrl = returnUrl;

            try
            {
                //Nur wenn die Model-Daten valide sind wird etwas gemacht
                if (ModelState.IsValid)
                {
                    //Erstellen eines neuen Users mit Automapper
                    var user = _Mapper.Map<FamilieLaissIdentityUser>(model);
                    user.IsAllowed = false;

                    //Hinzufügen des Users zum Identity-Store über die User-Operations
                    var result = await _UserOperations.CreateUser(user, model.Password);

                    //Wenn das Anlegen des Benutzers erfolgreich war, dann wird eine Mail
                    //an den User versendet, die zur Bestätigung der eMail-Adresse auffordert
                    if (result.Succeeded)
                    {
                        //Ermitteln eines neuen Tokens für das Bestätigen der eMail-Adresse
                        string Token = await _UserOperations.CreateMailConfirmationToken(user);

                        //Ermitteln der Callback-URL zur Bestätigung der eMail-Adresse
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = Token }, protocol: HttpContext.Request.Scheme);

                        //Erstellen der Mail für das Bestätigen des Passworts
                        SendMailModel mailData = await _MailGenerator.GenerateRegisterMail(user, Token, callbackUrl);

                        //Versenden der Mail an den User
                        await GetMailSenderService().SendEmailAsync(mailData);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        //Hinzufügen der Fehler aus dem Identity-Store
                        AddErrors(result);
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("Exception", _Localizer["ExceptionRegistration"]);
            }

            //Hinzufügen der Listen-Daten für Gender, Country, und Questions
            AddListDataToRegisterView(ViewBag, returnUrl);

            //Die View wird nur dann angezeigt wenn bei der Registrierung ein Fehler aufgetreten ist
            return View(model);
        }
        #endregion

        #region Confirm Email (Show)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            //Deklarationen
            bool result = false;

            try
            {
                //Überprüfen ob auch eine UserID und ein Token übergeben wurde. Wenn nicht wird auf die Fehlerseite gesprungen
                if (userId == null || code == null)
                {
                    return View("ErrorConfirmEMail");
                }

                //Ermitteln des Users
                var user = await _UserOperations.FindUserById(userId);

                //Wenn keine User gefunden wurde dann wird auf die Fehlerseite gesprungen
                if (user == null)
                {
                    return View("ErrorConfirmEMail");
                }

                //Bestätigen des EMail-Adresse in ASP.NET Core Identity
                result = await _UserOperations.ConfirmMail(user, code);

                //Mail an alle Admin-User versenden
                try
                {
                    //Ermitteln der Liste der Admin-User
                    IEnumerable<FamilieLaissIdentityUser> adminUserList = await _UserOperations.GetAdminUsers();

                    //Mit einer Schleife die Mail an alle Admin-User versenden
                    foreach (var adminUser in adminUserList)
                    {
                        //Erstellen der Mail an den Adminstrator
                        SendMailModel mailData = await _MailGenerator.GenerateAdminUnlockAccountMail(user, adminUser);

                        //Versenden der Mail an den User
                        await GetMailSenderService().SendEmailAsync(mailData);
                    }
                }
                catch
                {
                    //Wenn das Versenden der Mail schiefläuft, dann wurde aber trotzdem das freischalten erfolgreich durchgeführt
                    //TODO: Hier muss der Administrator noch benachrichtigt werden
                }
            }
            catch
            {
                result = false;
            }

            //Wenn die Bestätigung funktioniert hat wird auf die Hinweisseite gesprungen und ansonsten wird auf die Fehlerseite gesprungen
            return View(result ? "ConfirmEmail" : "ErrorConfirmEMail");
        }
        #endregion

        #region Reset Password Request (Show / Postback)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordRequest()
        {
            //Logging ausgeben
            _Logger.LogInformation("[{0}] is called", "ResetPasswordRequest");

            //Deklaration
            _Logger.LogDebug("[{0}] Creating model", "ResetPasswordRequest");
            ResetPasswordRequestViewModel model = new ResetPasswordRequestViewModel();

            //Hinzufügen der Country-Liste zum View-Bag
            _Logger.LogDebug("[{0}] Adding Country-List to viewbag", "ResetPasswordRequest");
            AddListDataQuestionToViewbag(ViewBag);

            //Die View zurückliefern
            _Logger.LogDebug("[{0}] View rendern", "ResetPasswordRequest");
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPasswordRequest(ResetPasswordRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Viewbag initialisieren
                    ViewBag.EMailNotFound = false;
                    ViewBag.EMailNotConfirmed = false;
                    ViewBag.SecurityWrong = false;

                    //Ermitteln des Users anhand der EMail-Adresse
                    var user = await _UserOperations.FindUserByMail(model.Email);

                    //Überprüfen ob zu der angegebenen EMail-Adresse ein User gefunden wurde. Wenn nicht wird auf die
                    //Fehlerseite gesprungen
                    if (user == null)
                    {
                        //Das entsprechende Fehlerflag setzen
                        ViewBag.EMailNotFound = true;

                        //Die View für einen Fehler anzeigen
                        return View("ResetPasswordRequestError");
                    }

                    //Überprüfen ob der User seine EMail-Adresse schon bestätigt hat. Wenn nicht wird auf die entsprechende
                    //Fehlerseite gesprungen
                    if (await _UserOperations.IsEMailConfirmed(user))
                    {
                        //Das entsprechende Fehlerflag setzen
                        ViewBag.EMailNotConfirmed = true;

                        //Die View für einen Fehler anzeigen
                        return View("ResetPasswordRequestError");
                    }

                    //Überprüfen ob die Sicherheitsfrage und die Sicherheitsantwort übereinstimmen
                    if (model.SecurityQuestion != user.SecurityQuestion || model.SecurityAnswer.Trim() != user.SecurityAnswer.Trim())
                    {
                        //Das entsprechende Fehlerflag setzen
                        ViewBag.SecurityWrong = true;

                        //Die View für einen Fehler anzeigen
                        return View("ResetPasswordRequestError");
                    }

                    //Erstellen eines Tokens zum Ändern des Passworts
                    string Token = await _UserOperations.GeneratePasswordToken(user.Id);

                    //Zusammenstellen der Callback-Url
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = Token }, protocol: HttpContext.Request.Scheme);

                    //Die Mail für den Anwender erstellen
                    SendMailModel mailData = await _MailGenerator.GenerateResetPasswordMail(user, Token, callbackUrl);

                    //Die Mail über den entsprechenden Service versenden
                    await GetMailSenderService().SendEmailAsync(mailData);

                    //Auf die entsprechende Erfolgsseite wechseln
                    return View("ResetPasswordRequestSuccess");
                }
                catch
                {
                    return View("ResetPasswordRequestError");
                }
            }

            //Die Security-Questions zum View-Bag hinzufügen
            AddListDataQuestionToViewbag(ViewBag);

            //Die View wird nur dann angezeigt wenn bei der Registrierung ein Fehler aufgetreten ist
            return View(model);
        }
        #endregion

        #region Reset Password (Show / Postback)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string userId = null, string code = null)
        {
            //Deklaration
            ResetPasswordViewModel Model;

            //Fehlerseite oder Model erzeugen
            if (code == null || userId == null)
            {
                //Wenn keine userId oder kein Code mitgegeben wurde, wird auf die Error-View gesprungen
                return View("ResetPasswordError");
            }
            else
            {
                //Wenn alles OK ist wird das Model erstellt
                Model = new ResetPasswordViewModel();
                Model.UserId = userId;
                Model.Token = code;
            }

            //Rendern der View
            return View(Model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Den User anhand der UserID ermitteln
                    var User = await _UserOperations.FindUserById(model.UserId);

                    //Es wurde kein User zu der UserId gefunden
                    if (User == null)
                    {
                        return View("ResetPasswordError");
                    }

                    //Zurücksetzen des Passwortes
                    var Result = await _UserOperations.ResetPassword(User, model.Token, model.NewPassword);

                    //
                    if (Result.Succeeded)
                    {
                        return View("ResetPasswordSuccess");
                    }
                    else
                    {
                        AddErrors(Result);
                    }
                }
                catch
                {
                    return View("ResetPasswordError");
                }
            }

            //Die Eingaben in der Maske sind nicht korrekt. Maske nochmals anzeigen
            return View(model);
        }
        #endregion

        #region Helpers
        private void AddListDataGenderToViewbag(dynamic viewbag)
        {
            //Erstellen der Gender-Liste
            GenderSelectList GenderList = new GenderSelectList(_LocalizerGender);

            //Das View-Bag bestücken
            ViewBag.GenderList = GenderList;
        }

        private void AddListDataCountryToViewbag(dynamic viewbag)
        {
            //Erstellen der Country-Liste
            CountrySelectList CountryList = new CountrySelectList(_LocalizerCountry);

            //Das View-Bag bestücken
            ViewBag.CountryList = CountryList;
        }

        private void AddListDataQuestionToViewbag(dynamic viewbag)
        {
            //Erstellen der Question-Liste
            SecurityQuestionList QuestionList = new SecurityQuestionList(_LocalizerQuestion);

            //Das View-Bag bestücken
            ViewBag.QuestionList = QuestionList;
        }

        private void AddListDataToRegisterView(dynamic ViewBag, string returnUrl)
        {
            //Das View-Bag bestücken
            ViewBag.ReturnUrl = returnUrl;

            //Hinzufügen der Gender-Liste zum Viewbag
            AddListDataGenderToViewbag(ViewBag);

            //Hinzufügen der Country-Liste zum View-Bag
            AddListDataCountryToViewbag(ViewBag);

            //Hinzufügen der Security-Questions zum View-Bag
            AddListDataQuestionToViewbag(ViewBag);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                //return RedirectToAction(nameof(HomeController.Index), "Home");
                return Redirect("http://wwww.google.de");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        //Ermittelt den benötigten Mail-Sender-Service aus dem IOC-Container
        //je nach dem ob sich die Anwendung im Debug-Mode oder in der Produktion befindet
        private IMailSender GetMailSenderService()
        {
            if (_HostingEnv.IsDevelopment())
            {
                return _MailSenderServiceAccessor("Dev");
            }
            else
            {
                return _MailSenderServiceAccessor("Prod");
            }
        }
        #endregion
    }
}