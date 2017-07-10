using FamilieLaissBackend.Context;
using FamilieLaissBackend.Interfaces;
using FamilieLaissBackend.Manager;
using FamilieLaissBackend.Model.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace FamilieLaissBackend.Repository
{
    public class AuthRepository : IDisposable, IUserOperations
    {
        #region Private Members
        private FamilieLaissIdentityContext _ctx;
        private UserManager<IdentityUserExtended> _userManager;
        #endregion

        #region C'tor
        public AuthRepository()
        {
            //Ermitteln des Identity-DB-Kontext aus dem aktuellen OWIN-Kontext
            _ctx = HttpContext.Current.GetOwinContext().Get<FamilieLaissIdentityContext>();

            //Ermitteln des User-Managers aus dem aktuellen OWIN-Kontext
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<FamilieLaissUserManager>();
        }
        #endregion

        #region User-Operations
        //Registrieren eines Users
        public async Task<IdentityResult> RegisterUser(RegisterUserDTO model)
        {
            //Übernehmen der Properties aus dem Registrier-Model mit Automapper
            IdentityUserExtended user = AutoMapper.Mapper.Map<IdentityUserExtended>(model);
            user.IsAllowed = false;

            //Erstellen des Users
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            //Funktionsergebnis
            return result;
        }

        //Hinzufügen einer Rolle zum User
        public async Task<IdentityResult> AddToRole(string userName, string roleName)
        {
            //Ermitteln des Users
            IdentityUserExtended user = await _userManager.FindByNameAsync(userName);

            //Hinzufügen der Rolle
            IdentityResult roleResult = await _userManager.AddToRoleAsync(user.Id, roleName);

            //Return-Value
            return roleResult;
        }

        //Ermitteln des eMail Confirmation-Token
        public async Task<string> GetMailConfirmationToken(string UserName)
        {
            //Ermitteln des Users
            IdentityUserExtended user = await _userManager.FindByNameAsync(UserName);

            //Ermitteln des Tokens
            string Code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);

            //Return-Value
            return Code;
        }

        //Bestätigung der eMail
        public async Task<bool> ConfirmMail(string userId, string token)
        {
            //Bestätigen der eMail
            IdentityResult result = await _userManager.ConfirmEmailAsync(userId, token);

            //Funktionsergebnis
            if (result.Errors.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Setzt den Count für eine Fehlanmeldung (falsches Passwort um eines nach oben)
        public async Task<IdentityResult> AccessFailed(string userName)
        {
            //Deklaration
            IdentityResult ReturnValue = null;

            //Ermitteln des Users
            IdentityUserExtended user = await _userManager.FindByNameAsync(userName);

            //Wenn kein User gefunden wurde, dann ist der Username nicht richtig
            //und nicht das Passwort. Dann muss auch nicnts gemacht werden.
            //Ansonsten wird der Count für die Fehlermeldungen um eines erhöht.
            if (user != null)
            {
                ReturnValue = await _userManager.AccessFailedAsync(user.Id);
            }

            //Funktionsergebnis
            return ReturnValue;
        }

        //Überprüfen ob der Benutzer wegen zu vieler fehlgeschlagener Anmeldeversuche gesperrt ist
        public async Task<bool> IsLockedOut(string userName)
        {
            //Deklaration
            bool ReturnValue = false;

            //Ermitteln des Users
            IdentityUserExtended user = await _userManager.FindByNameAsync(userName);

            //Überprüfen ob der User gesperrt ist
            if (user != null)
            {
                return await _userManager.IsLockedOutAsync(user.Id);
            }

            //Funktionsergebnis
            return ReturnValue;
        }

        //Ermitteln der aktuellen Anzahl Fehlversuche
        public async Task<int> GetAccessFailedCount(string userName)
        {
            //Deklaration
            int Count = 0;

            //Ermitteln des Users
            IdentityUserExtended user = await _userManager.FindByNameAsync(userName);

            //Ermitteln des Counts
            if (user != null)
            {
                Count = await _userManager.GetAccessFailedCountAsync(user.Id);
            }

            //Funktionsergebnis
            return Count;
        }

        //Zurücksetzen des Counters für die fehlerhaften Anmeldeversuche
        public async Task<IdentityResult> ResetAccesFailedCount(string userId)
        {
            return await _userManager.ResetAccessFailedCountAsync(userId);
        }

        //Ermitteln aller Rollen für den User
        public async Task<IList<string>> GetRolesForUser(string userId)
        {
            return await _userManager.GetRolesAsync(userId);
        }

        //Erstellen der Identity für Owin
        public async Task<ClaimsIdentity> CreateIdentity(IdentityUserExtended user, string authType)
        {
            return await _userManager.CreateIdentityAsync(user, authType);
        }

        //Mail an den User über den Mail-Service von Identity senden
        public async Task SendMailToUser(string userName, string subject, string body)
        {
            //Ermitteln des Users
            IdentityUserExtended user = await _userManager.FindByNameAsync(userName);

            //Senden der Mail
            await _userManager.SendEmailAsync(user.Id, subject, body);
        }

        //Ermitteln eines Users anhand des Namens
        public async Task<IdentityUserExtended> FindUser(string userName)
        {
            //Ermitteln des Users anhand des Benutzernamens
            IdentityUserExtended user = await _userManager.FindByNameAsync(userName);

            //Funktionsergebnis
            return user;
        }

        //Ermitteln eines Users anhand des Namens und des Passworts
        public async Task<IdentityUserExtended> FindUser(string userName, string password)
        {
            //Ermitteln des Users anhand des Benutzernamens und des Passwortes
            IdentityUserExtended user = await _userManager.FindAsync(userName, password);

            //Funktionsergebnis
            return user;
        }

        //Ermitteln eines Users anhand der eMail
        public async Task<IdentityUserExtended> FindUserByMail(string eMail)
        {
            //Ermitteln des Users anhand der eMail
            IdentityUserExtended user = await _userManager.FindByEmailAsync(eMail);

            //Funktionsergebnis
            return user;
        }

        //Ermitteln aller User
        public List<IdentityUserExtended> GetAllUsers()
        {
            //Deklaration
            List<IdentityUserExtended> Users = new List<IdentityUserExtended>();

            //Ermitteln der User aus der Datenbank
            foreach (var User in _userManager.Users)
            {
                Users.Add(User);
            }

            //Funktionsergebnis
            return Users;
        }

        //Ermitteln aller Administratoren
        public async Task<List<IdentityUserExtended>> GetAllAdministrators()
        {
            //Deklaration
            List<IdentityUserExtended> Users = new List<IdentityUserExtended>();

            //Ermitteln der User aus der Datenbank
            foreach (var User in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(User.Id, "Admin"))
                {
                    Users.Add(User);
                }
            }

            //Funktionsergebnis
            return Users;
        }

        //Sperren / Entsperren des Accounts
        public async Task<IdentityResult> LockUnlockAccount(string userName, bool Allowed)
        {
            //Deklaration
            IdentityResult Result = null;
            List<string> Errors = new List<string>();

            //Ermitteln des Users
            IdentityUserExtended user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                //Setzen der Property
                user.IsAllowed = Allowed;

                //Aktualisieren des User-Accounts
                Result = await _userManager.UpdateAsync(user);
            }
            else
            {
                Errors.Add("User not found");
                Result = IdentityResult.Failed(Errors.ToArray());
            }

            //Funktionsergebnis
            return Result;
        }

        //Löschen eines Benutzerkontos
        public async Task<IdentityResult> DeleteAccount(string userName)
        {
            //Deklaration
            IdentityResult Result;
            List<string> Errors = new List<string>();

            //Ermitteln des Users
            IdentityUserExtended user = await _userManager.FindByNameAsync(userName);

            //Löschen des Accounts
            if (user != null)
            {
                Result = await _userManager.DeleteAsync(user);
            }
            else
            {
                Errors.Add("User not found");
                Result = IdentityResult.Failed(Errors.ToArray());
            }

            //TODO: Hier muss auch noch das Löschen Benutzerbezogener Daten mit rein

            //TODO: Bei Refresh-Tokens muss hier auch der entsprechende Eintrag in der Datenbank gelöscht werden

            //Funktionsergebnis
            return Result;
        }

        //Generiert ein Passwort Reset-Token
        public async Task<string> GeneratePasswordToken(string userID)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(userID);
        }

        //Setzt ein neues Passwort für den Benutzer
        public async Task<IdentityResult> SetNewPassword(NewPasswordDTO model)
        {
            //Deklaration
            List<string> Errors = new List<string>();

            //Ermitteln des Users
            IdentityUserExtended user = await _userManager.FindByNameAsync(model.UserName);

            //Wenn ein User gefunden wurde dann wird das Passwort zurückgesetzt
            if (user != null)
            {
                //Setzen des neuen Passworts
                IdentityResult Result = await _userManager.ResetPasswordAsync(user.Id, model.Token, model.Password);

                //Funktionsergebnis
                return Result;
            }
            else
            {
                //Funktionsergebnis
                Errors.Add("User not found");
                return IdentityResult.Failed(Errors.ToArray());
            }
        }

        IEnumerable<IdentityUserExtended> IUserOperations.GetAllUsers()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<IdentityUserExtended>> IUserOperations.GetAllAdministrators()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
        #endregion
    }
}