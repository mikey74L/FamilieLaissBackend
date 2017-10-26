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
    public class SigninOperationsService : ISigninOperations
    {
        #region Private Members
        private readonly IUserOperations _UserOperations;
        private readonly SignInManager<FamilieLaissIdentityUser> _Manager;
        #endregion

        #region C'tor
        public SigninOperationsService(IUserOperations userOperations, SignInManager<FamilieLaissIdentityUser> manager)
        {
            _UserOperations = userOperations;
            _Manager = manager;
        }
        #endregion

        //Macht einen Signin über Benutzername und Passwort
        public async Task<FamilieLaissSigninResultModel> SigninWithPassword(string userName, string password)
        {
            //Deklaration
            FamilieLaissSigninResultModel ReturnValue = new FamilieLaissSigninResultModel();

            try
            {
                //Überprüfen ob der Benutzer existiert
                FamilieLaissIdentityUser user = await _UserOperations.FindUserByName(userName);

                if (user != null)
                {
                    //Überprüfen ob der Benutzer seine Mail-Adresse schon bestätigt hat
                    ReturnValue.EMailConfirmed = await _UserOperations.IsEMailConfirmed(user);

                    //Überprüfen ob der Benutzer schon freigeschalten wurde
                    ReturnValue.IsAllowed = _UserOperations.Users.Count(x => x.Id == user.Id && x.IsAllowed) > 0;

                    //Überprüfen ob der Benutzer gesperrt wurde
                    ReturnValue.IsLockedOut = await _UserOperations.IsLockedOut(userName);

                    //Nur wenn bisher keine Fehler aufgetaucht sind wird weitergemacht
                    if (ReturnValue.EMailConfirmed && ReturnValue.IsAllowed && !ReturnValue.IsLockedOut && !ReturnValue.UsernameOrPasswordWrong)
                    {
                        //Anmelden des Benutzers
                        SignInResult Result = await _Manager.PasswordSignInAsync(userName, password, false, false);

                        //Wenn Anmeldung fehlgeschlagen wird der Count für die Fehlermeldungen um eines nach oben gesetzt
                        if (!Result.Succeeded)
                        {
                            ReturnValue.UsernameOrPasswordWrong = true;
                        }
                    }
                }
                else
                {
                    ReturnValue.UsernameOrPasswordWrong = true;
                }
            }
            catch
            {
                ReturnValue.GeneralError = true;
            }

            //Funktionsergebnis
            return ReturnValue;
        }
    }
}
