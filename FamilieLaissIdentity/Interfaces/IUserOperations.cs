using FamilieLaissIdentity.Data.Models;
using FamilieLaissIdentity.Models.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Interfaces
{
    public interface IUserOperations
    {
        //Bestätigung der eMail-Adresse für den User
        Task<bool> ConfirmMail(string userID, string token);

        //Setzt den Count für eine Fehlanmeldung (falsches Passwort um eines nach oben)
        Task<IdentityResult> AccessFailed(string userName);

        //Überprüfen ob der Benutzer wegen zu vieler fehlgeschlagener Anmeldeversuche gesperrt ist
        Task<bool> IsLockedOut(string userName);

        //Ermitteln der aktuellen Anzahl Fehlversuche
        Task<int> GetAccessFailedCount(string userName);

        //Zurücksetzen des Counters für die fehlerhaften Anmeldeversuche
        Task<IdentityResult> ResetAccesFailedCount(string userID);

        //Ermitteln aller Rollen für den User
        Task<IList<string>> GetRolesForUser(string userID);

        //Mail an den User über den Mail-Service von Identity senden
        Task SendMailToUser(string userName, string subject, string body);

        //Ermitteln eines Users anhand des Namens
        Task<FamilieLaissIdentityUser> FindUser(string userName);

        //Ermitteln eines Users anhand der eMail
        Task<FamilieLaissIdentityUser> FindUserByMail(string eMail);

        //Sperren / Entsperren des Accounts
        Task<IdentityResult> LockUnlockAccount(string userName, bool allowed);

        //Löschen eines Benutzerkontos
        Task<IdentityResult> DeleteAccount(string userName);

        //Generiert ein Passwort Reset-Token
        Task<string> GeneratePasswordToken(string userID);

        //Setzt ein neues Passwort für den Benutzer
        Task<IdentityResult> SetNewPassword(NewPasswordDTO model);
    }
}
