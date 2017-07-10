using FamilieLaissBackend.Model;
using FamilieLaissBackend.Model.Account;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissBackend.Interfaces
{
    public interface IUserOperations
    {
        //Einen User Registrieren
        Task<IdentityResult> RegisterUser(RegisterUserDTO model);

        //Einen User zu einer bestimmten Rolle hinzufügen
        Task<IdentityResult> AddToRole(string userName, string roleName);

        //Ermitteln des eMail Confirmation-Token
        Task<string> GetMailConfirmationToken(string UserName);

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

        //Erstellen der Identity für Owin
        Task<ClaimsIdentity> CreateIdentity(IdentityUserExtended user, string authType);

        //Mail an den User über den Mail-Service von Identity senden
        Task SendMailToUser(string userName, string subject, string body);

        //Ermitteln eines Users anhand des Namens
        Task<IdentityUserExtended> FindUser(string userName);

        //Ermitteln eines Users anhand des Namens und des Passworts
        Task<IdentityUserExtended> FindUser(string userName, string password);

        //Ermitteln eines Users anhand der eMail
        Task<IdentityUserExtended> FindUserByMail(string eMail);

        //Ermitteln aller User
        IEnumerable<IdentityUserExtended> GetAllUsers();

        //Ermitteln aller Administratoren
        Task<IEnumerable<IdentityUserExtended>> GetAllAdministrators();

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
