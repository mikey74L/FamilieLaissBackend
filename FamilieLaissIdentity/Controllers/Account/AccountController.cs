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

namespace FamilieLaissIdentity.Controllers
{
    [Authorize]
    [SecurityHeaders]
    public class AccountController : Controller
    {
        #region Private Members
        private readonly ILogger<AccountController> _Logger;
        private readonly IMapper _mapper;
        private readonly IUserOperations _userOperations;
        private readonly Func<string, IMailSender> _mailSenderServiceAccessor;
        private readonly IMailGenerator _mailGenerator;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IStringLocalizer<AccountController> Localizer;
        private readonly IStringLocalizer<GenderSelectList> LocalizerGender;
        private readonly IStringLocalizer<CountrySelectList> LocalizerCountry;
        private readonly IStringLocalizer<SecurityQuestionList> LocalizerQuestion;
        //private readonly IUserOperations _UserOperations;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IEmailSender _emailSender;
        //private readonly ISmsSender _smsSender;
        //private readonly ILogger _logger;
        //private readonly IIdentityServerInteractionService _interaction;
        //private readonly IClientStore _clientStore;
        //private readonly AccountService _account;
        #endregion

        #region C'tor
        public AccountController(
            ILogger<AccountController> logger,
            IMapper mapper, 
            IUserOperations userOperations, 
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
            _mapper = mapper;

            //User-Operations übernehmen
            _userOperations = userOperations;

            //Übernhemen der Factory für den Mail-Sender-Service
            _mailSenderServiceAccessor = mailSenderServiceAccessor;

            //Übernehmen des Mail-Generator
            _mailGenerator = mailGenerator;

            //Die aktuelle Hosting Umgebung übernehmen
            _hostingEnv = hostingEnv;

            //Die Lokalisierungen übernehmen
            Localizer = localizer;
            LocalizerGender = localizerGender;
            LocalizerCountry = localizerCountry;
            LocalizerQuestion = localizerQuestion;
        }
        //public AccountController(
        //    IUserOperations userOperations,
        //    UserManager<ApplicationUser> userManager,
        //    SignInManager<ApplicationUser> signInManager,
        //    IEmailSender emailSender,
        //    ISmsSender smsSender,
        //    ILoggerFactory loggerFactory,
        //    IIdentityServerInteractionService interaction,
        //    IHttpContextAccessor httpContext,
        //    IClientStore clientStore)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _emailSender = emailSender;
        //    _smsSender = smsSender;
        //    _logger = loggerFactory.CreateLogger<AccountController>();
        //    _interaction = interaction;
        //    _clientStore = clientStore;

        //    _account = new AccountService(interaction, httpContext, clientStore);
        //}
        #endregion

        //#region Login (Show / Postback)
        ////
        //// GET: /Account/Login
        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IActionResult> Login(string returnUrl)
        //{
        //    var vm = await _account.BuildLoginViewModelAsync(returnUrl);

        //    if (vm.IsExternalLoginOnly)
        //    {
        //        // only one option for logging in
        //        return ExternalLogin(vm.ExternalProviders.First().AuthenticationScheme, returnUrl);
        //    }

        //    return View(vm);
        //}

        ////
        //// POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginInputModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // This doesn't count login failures towards account lockout
        //        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        //        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberLogin, lockoutOnFailure: false);
        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation(1, "User logged in.");
        //            return RedirectToLocal(model.ReturnUrl);
        //        }
        //        if (result.RequiresTwoFactor)
        //        {
        //            return RedirectToAction(nameof(SendCode), new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberLogin });
        //        }
        //        if (result.IsLockedOut)
        //        {
        //            _logger.LogWarning(2, "User account locked out.");
        //            return View("Lockout");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //            return View(await _account.BuildLoginViewModelAsync(model));
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(await _account.BuildLoginViewModelAsync(model));
        //}
        //#endregion

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
                    var user = _mapper.Map<FamilieLaissIdentityUser>(model);
                    user.IsAllowed = false;

                    //Hinzufügen des Users zum Identity-Store über die User-Operations
                    var result = await _userOperations.CreateUser(user, model.Password);

                    //Wenn das Anlegen des Benutzers erfolgreich war, dann wird eine Mail
                    //an den User versendet, die zur Bestätigung der eMail-Adresse auffordert
                    if (result.Succeeded)
                    {
                        //Ermitteln eines neuen Tokens für das Bestätigen der eMail-Adresse
                        string Token = await _userOperations.CreateMailConfirmationToken(user);

                        //Ermitteln der Callback-URL zur Bestätigung der eMail-Adresse
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = Token }, protocol: HttpContext.Request.Scheme);

                        //Erstellen der Mail für das Bestätigen des Passworts
                        SendMailModel mailData = await _mailGenerator.GenerateRegisterMail(user, Token, callbackUrl);

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
                ModelState.AddModelError("Exception", Localizer["ExceptionRegistration"]);
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
                var user = await _userOperations.FindUserById(userId);

                //Wenn keine User gefunden wurde dann wird auf die Fehlerseite gesprungen
                if (user == null)
                {
                    return View("ErrorConfirmEMail");
                }

                //Bestätigen des EMail-Adresse in ASP.NET Core Identity
                result = await _userOperations.ConfirmMail(user, code);

                //Mail an alle Admin-User versenden
                try
                {
                    //Ermitteln der Liste der Admin-User
                    IEnumerable<FamilieLaissIdentityUser> adminUserList = await _userOperations.GetAdminUsers();

                    //Mit einer Schleife die Mail an alle Admin-User versenden
                    foreach (var adminUser in adminUserList)
                    {
                        //Erstellen der Mail an den Adminstrator
                        SendMailModel mailData = await _mailGenerator.GenerateAdminUnlockAccountMail(user, adminUser);

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

        #region Change Password (Show / Postback)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ChangePassword()
        {
            //Logging ausgeben
            _Logger.LogInformation("[{0}] is called", "ChangePassword");

            //Deklaration
            _Logger.LogDebug("[{0}] Creating model", "ChangePassword");
            ChangePasswordViewModel model = new ChangePasswordViewModel();

            //Hinzufügen der Country-Liste zum View-Bag
            _Logger.LogDebug("[{0}] Adding Country-List to viewbag", "ChangePassword");
            AddListDataQuestionToViewbag(ViewBag);

            //Die View zurückliefern
            _Logger.LogDebug("[{0}] View rendern", "ChangePassword");
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
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
                    var user = await _userOperations.FindUserByMail(model.Email);

                    //Überprüfen ob zu der angegebenen EMail-Adresse ein User gefunden wurde. Wenn nicht wird auf die
                    //Fehlerseite gesprungen
                    if (user == null)
                    {
                        //Das entsprechende Fehlerflag setzen
                        ViewBag.EMailNotFound = true;

                        //Die View für einen Fehler anzeigen
                        return View("ChangePasswordError");
                    }

                    //Überprüfen ob der User seine EMail-Adresse schon bestätigt hat. Wenn nicht wird auf die entsprechende
                    //Fehlerseite gesprungen
                    if (await _userOperations.IsEMailConfirmed(user))
                    {
                        //Das entsprechende Fehlerflag setzen
                        ViewBag.EMailNotConfirmed = true;

                        //Die View für einen Fehler anzeigen
                        return View("ChangePasswordError");
                    }

                    //Überprüfen ob die Sicherheitsfrage und die Sicherheitsantwort übereinstimmen
                    if (model.SecurityQuestion != user.SecurityQuestion || model.SecurityAnswer.Trim() != user.SecurityAnswer.Trim())
                    {
                        //Das entsprechende Fehlerflag setzen
                        ViewBag.SecurityWrong = true;

                        //Die View für einen Fehler anzeigen
                        return View("ChangePasswordError");
                    }

                    //Erstellen eines Tokens zum Ändern des Passworts
                    string Token = await _userOperations.GeneratePasswordToken(user.Id);

                    //Zusammenstellen der Callback-Url
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = Token }, protocol: HttpContext.Request.Scheme);

                    //Die Mail für den Anwender erstellen
                    SendMailModel mailData = await _mailGenerator.GenerateChangePasswordMail(user, Token, callbackUrl);

                    //Die Mail über den entsprechenden Service versenden
                    await GetMailSenderService().SendEmailAsync(mailData);

                    //Auf die entsprechende Erfolgsseite wechseln
                    return View("ChangePasswordSuccess");
                }
                catch
                {
                    return View("ChangePasswordError");
                }
            }

            //Die Security-Questions zum View-Bag hinzufügen
            AddListDataQuestionToViewbag(ViewBag);

            //Die View wird nur dann angezeigt wenn bei der Registrierung ein Fehler aufgetreten ist
            return View(model);
        }
        #endregion

        //#region Forgot Password Confirmation (Show)
        ////
        //// GET: /Account/ForgotPasswordConfirmation
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ForgotPasswordConfirmation()
        //{
        //    return View();
        //}
        //#endregion

        //#region Reset Password (Show / Postback)
        ////
        //// GET: /Account/ResetPassword
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ResetPassword(string code = null)
        //{
        //    return code == null ? View("Error") : View();
        //}

        ////
        //// POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await _userManager.FindByNameAsync(model.Email);
        //    if (user == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
        //    }
        //    var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
        //    }
        //    AddErrors(result);
        //    return View();
        //}
        //#endregion

        //#region Reset Password Confirmation (Show)
        ////
        //// GET: /Account/ResetPasswordConfirmation
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ResetPasswordConfirmation()
        //{
        //    return View();
        //}
        //#endregion

        //#region Send Code (Show / Postback)
        ////
        //// GET: /Account/SendCode
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        //{
        //    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
        //    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        ////
        //// POST: /Account/SendCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SendCode(SendCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }

        //    // Generate the token and send it
        //    var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
        //    if (string.IsNullOrWhiteSpace(code))
        //    {
        //        return View("Error");
        //    }

        //    var message = "Your security code is: " + code;
        //    if (model.SelectedProvider == "Email")
        //    {
        //        await _emailSender.SendEmailAsync(await _userManager.GetEmailAsync(user), "Security Code", message);
        //    }
        //    else if (model.SelectedProvider == "Phone")
        //    {
        //        await _smsSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
        //    }

        //    return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        //}
        //#endregion

        //#region Verify Code (Show / Postback)
        ////
        //// GET: /Account/VerifyCode
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        //{
        //    // Require that the user has already logged in via username/password or external login
        //    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        ////
        //// POST: /Account/VerifyCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // The following code protects for brute force attacks against the two factor codes.
        //    // If a user enters incorrect codes for a specified amount of time then the user account
        //    // will be locked out for a specified amount of time.
        //    var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToLocal(model.ReturnUrl);
        //    }
        //    if (result.IsLockedOut)
        //    {
        //        _logger.LogWarning(7, "User account locked out.");
        //        return View("Lockout");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid code.");
        //        return View(model);
        //    }
        //}
        //#endregion

        #region Helpers
        private void AddListDataGenderToViewbag(dynamic viewbag)
        {
            //Erstellen der Gender-Liste
            GenderSelectList GenderList = new GenderSelectList(LocalizerGender);

            //Das View-Bag bestücken
            ViewBag.GenderList = GenderList;
        }

        private void AddListDataCountryToViewbag(dynamic viewbag)
        {
            //Erstellen der Country-Liste
            CountrySelectList CountryList = new CountrySelectList(LocalizerCountry);

            //Das View-Bag bestücken
            ViewBag.CountryList = CountryList;
        }

        private void AddListDataQuestionToViewbag(dynamic viewbag)
        {
            //Erstellen der Question-Liste
            SecurityQuestionList QuestionList = new SecurityQuestionList(LocalizerQuestion);

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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        //private Task<ApplicationUser> GetCurrentUserAsync()
        //{
        //    return _userManager.GetUserAsync(HttpContext.User);
        //}

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

        //Ermittelt den benötigten Mail-Sender-Service aus dem IOC-Container
        //je nach dem ob sich die Anwendung im Debug-Mode oder in der Produktion befindet
        private IMailSender GetMailSenderService()
        {
            if (_hostingEnv.IsDevelopment())
            {
                return _mailSenderServiceAccessor("Dev");
            }
            else
            {
                return _mailSenderServiceAccessor("Prod");
            }
        }
        #endregion
    }
}