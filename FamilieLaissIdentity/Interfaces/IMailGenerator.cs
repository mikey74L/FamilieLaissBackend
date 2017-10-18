using FamilieLaissIdentity.Data.Models;
using FamilieLaissIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Interfaces
{
    public interface IMailGenerator
    {
        //Erstellt eine Mail für den User nach dem die Registrierung erfolgreich abgeschlossen ist
        Task<SendMailModel> GenerateRegisterMail(FamilieLaissIdentityUser user, string tokenMailConfirm, string callBackURL);

        //Erstellt eine Mail für den Administrator mit der Aufforderung das Benutzerkonto freizuschalten
        Task<SendMailModel> GenerateAdminUnlockAccountMail(FamilieLaissIdentityUser user, FamilieLaissIdentityUser admin);

        //Erstellt eine Mail für den Anwender mit Anweisungen wie das Passwort geändert werden kann
        Task<SendMailModel> GenerateChangePasswordMail(FamilieLaissIdentityUser user, string tokenChangePassword, string callBackURL);

        //Erstellt eine Mail für den Anwender wenn das Benutzerkonto durch den Administrator freigegeben wurde
        Task<SendMailModel> GenerateGrantAccessMail(FamilieLaissIdentityUser user);

        //Erstellt eine Mail für den Anwender wenn das Benutzerkonto durch den Administrator gesperrt wurde
        Task<SendMailModel> GenerateRevokeAcessMail(FamilieLaissIdentityUser user);
    }
}
