using FamilieLaissIdentity.Data.Models;
using FamilieLaissIdentity.Interfaces;
using FamilieLaissIdentity.Models.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Service
{
    public class UserOperationsService : IDisposable, IUserOperations
    {
        #region Private Members
        private UserManager<FamilieLaissIdentityUser> _userManager;
        #endregion

        #region C'tor
        public UserOperationsService(UserManager<FamilieLaissIdentityUser> userManager)
        {
            //From IOC
            _userManager = userManager;
        }
        #endregion

        #region User-Operations
        //Hinzufügen einer Rolle zum User
        public async Task<IdentityResult> AddToRole(string userName, string roleName)
        {
            //Ermitteln des Users
            FamilieLaissIdentityUser user = await _userManager.FindByNameAsync(userName);

            //Hinzufügen der Rolle
            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, roleName);

            //Return-Value
            return roleResult;
        }

        //Ermitteln des eMail Confirmation-Token
        public async Task<string> GetMailConfirmationToken(string UserName)
        {
            //Ermitteln des Users
            FamilieLaissIdentityUser user = await _userManager.FindByNameAsync(UserName);

            //Ermitteln des Tokens
            string Code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //Return-Value
            return Code;
        }

        //Bestätigung der eMail
        public async Task<bool> ConfirmMail(string userId, string token)
        {
            //Ermitteln des Users
            FamilieLaissIdentityUser user = await _userManager.FindByIdAsync(userId);

            //Bestätigen der eMail
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);

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
            FamilieLaissIdentityUser user = await _userManager.FindByNameAsync(userName);

            //Wenn kein User gefunden wurde, dann ist der Username nicht richtig
            //und nicht das Passwort. Dann muss auch nicnts gemacht werden.
            //Ansonsten wird der Count für die Fehlermeldungen um eines erhöht.
            if (user != null)
            {
                ReturnValue = await _userManager.AccessFailedAsync(user);
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
            FamilieLaissIdentityUser user = await _userManager.FindByNameAsync(userName);

            //Überprüfen ob der User gesperrt ist
            if (user != null)
            {
                return await _userManager.IsLockedOutAsync(user);
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
            FamilieLaissIdentityUser user = await _userManager.FindByNameAsync(userName);

            //Ermitteln des Counts
            if (user != null)
            {
                Count = await _userManager.GetAccessFailedCountAsync(user);
            }

            //Funktionsergebnis
            return Count;
        }

        //Zurücksetzen des Counters für die fehlerhaften Anmeldeversuche
        public async Task<IdentityResult> ResetAccesFailedCount(string userId)
        {
            //Ermitteln des Users
            FamilieLaissIdentityUser user = await _userManager.FindByIdAsync(userId);

            //Zurücksetzen aufrufen
            return await _userManager.ResetAccessFailedCountAsync(user);
        }

        //Ermitteln aller Rollen für den User
        public async Task<IList<string>> GetRolesForUser(string userId)
        {
            //Ermitteln des Users
            FamilieLaissIdentityUser user = await _userManager.FindByIdAsync(userId);

            //Ermitteln der Rollen
            return await _userManager.GetRolesAsync(user);
        }

        //Mail an den User über den Mail-Service von Identity senden
        public async Task SendMailToUser(string userName, string subject, string body)
        {
            //Ermitteln des Users
            FamilieLaissIdentityUser user = await _userManager.FindByNameAsync(userName);

            //Senden der Mail
          //  await _userManager.SendEmailAsync(user.Id, subject, body);
        }

        //Ermitteln eines Users anhand des Namens
        public async Task<FamilieLaissIdentityUser> FindUser(string userName)
        {
            //Ermitteln des Users anhand des Benutzernamens
            FamilieLaissIdentityUser user = await _userManager.FindByNameAsync(userName);

            //Funktionsergebnis
            return user;
        }

        //Ermitteln eines Users anhand der eMail
        public async Task<FamilieLaissIdentityUser> FindUserByMail(string eMail)
        {
            //Ermitteln des Users anhand der eMail
            FamilieLaissIdentityUser user = await _userManager.FindByEmailAsync(eMail);

            //Funktionsergebnis
            return user;
        }

        //Sperren / Entsperren des Accounts
        public async Task<IdentityResult> LockUnlockAccount(string userName, bool Allowed)
        {
            //Deklaration
            IdentityResult Result = null;
            List<IdentityError> Errors = new List<IdentityError>();

            //Ermitteln des Users
            FamilieLaissIdentityUser user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                //Setzen der Property
                user.IsAllowed = Allowed;

                //Aktualisieren des User-Accounts
                Result = await _userManager.UpdateAsync(user);
            }
            else
            {
                Errors.Add(new IdentityError() { Code = "UserNotFound", Description = "User not found" });
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
            List<IdentityError> Errors = new List<IdentityError>();

            //Ermitteln des Users
            FamilieLaissIdentityUser user = await _userManager.FindByNameAsync(userName);

            //Löschen des Accounts
            if (user != null)
            {
                Result = await _userManager.DeleteAsync(user);
            }
            else
            {
                Errors.Add(new IdentityError() { Code = "UserNotFound", Description = "User not found" });
                Result = IdentityResult.Failed(Errors.ToArray());
            }

            //TODO: Hier muss auch noch das Löschen Benutzerbezogener Daten mit rein

            //Funktionsergebnis
            return Result;
        }

        //Generiert ein Passwort Reset-Token
        public async Task<string> GeneratePasswordToken(string userID)
        {
            //Ermitteln des Users
            FamilieLaissIdentityUser user = await _userManager.FindByIdAsync(userID);

            //Passwort Reset Token erzeugen
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        //Setzt ein neues Passwort für den Benutzer
        public async Task<IdentityResult> SetNewPassword(NewPasswordDTO model)
        {
            //Deklaration
            List<IdentityError> Errors = new List<IdentityError>();

            //Ermitteln des Users
            FamilieLaissIdentityUser user = await _userManager.FindByNameAsync(model.UserName);

            //Wenn ein User gefunden wurde dann wird das Passwort zurückgesetzt
            if (user != null)
            {
                //Setzen des neuen Passworts
                IdentityResult Result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                //Funktionsergebnis
                return Result;
            }
            else
            {
                //Funktionsergebnis
                Errors.Add(new IdentityError() { Code = "UserNotFound", Description = "User not found" });
                return IdentityResult.Failed(Errors.ToArray());
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _userManager.Dispose();

        }
        #endregion
    }
}