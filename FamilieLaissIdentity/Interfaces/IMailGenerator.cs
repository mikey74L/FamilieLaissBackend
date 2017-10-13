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
        SendMailModel GenerateRegisterMail(FamilieLaissIdentityUser user, string tokenMailConfirm, string callBackURL);
    }
}
