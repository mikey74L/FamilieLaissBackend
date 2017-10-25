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
        //Einen neuen Benutzer-Account erstellen
        Task<IdentityResult> CreateUser(FamilieLaissIdentityUser user, string password);

        //Erstellt ein neues Token für Confirm Mail
        Task<string> CreateMailConfirmationToken(FamilieLaissIdentityUser user);

        //Bestätigung der eMail-Adresse für den User
        Task<bool> ConfirmMail(FamilieLaissIdentityUser user, string token);

        //Setzt den Count für eine Fehlanmeldung (falsches Passwort um eines nach oben)
        Task<IdentityResult> AccessFailed(string userName);

        //Überprüfen ob der Benutzer wegen zu vieler fehlgeschlagener Anmeldeversuche gesperrt ist
        Task<bool> IsLockedOut(string userName);

        //Ermittelt ob ein User schon seine EMail-Adresse bestätigt hat
        Task<bool> IsEMailConfirmed(FamilieLaissIdentityUser user);

        //Ermitteln der aktuellen Anzahl Fehlversuche
        Task<int> GetAccessFailedCount(string userName);

        //Zurücksetzen des Counters für die fehlerhaften Anmeldeversuche
        Task<IdentityResult> ResetAccesFailedCount(string userID);

        //Ermitteln aller Rollen für den User
        Task<IList<string>> GetRolesForUser(string userID);

        //Mail an den User über den Mail-Service von Identity senden
        Task SendMailToUser(string userName, string subject, string body);

        //Ermitteln eines Users anhand der ID
        Task<FamilieLaissIdentityUser> FindUserById(string Id);

        //Ermitteln eines Users anhand des Namens
        Task<FamilieLaissIdentityUser> FindUserByName(string userName);

        //Ermitteln eines Users anhand der eMail
        Task<FamilieLaissIdentityUser> FindUserByMail(string eMail);

        //Ermittelt alle Admin-User
        Task<IEnumerable<FamilieLaissIdentityUser>> GetAdminUsers();

        //Sperren / Entsperren des Accounts
        Task<IdentityResult> LockUnlockAccount(string userName, bool allowed);

        //Löschen eines Benutzerkontos
        Task<IdentityResult> DeleteAccount(string userName);

        //Generiert ein Passwort Reset-Token
        Task<string> GeneratePasswordToken(string userID);

        //Setzt das Password anhand eines Tokens auf ein neues Passwort
        Task<IdentityResult> ResetPassword(FamilieLaissIdentityUser user, string token, string newPassword);
    }
}
