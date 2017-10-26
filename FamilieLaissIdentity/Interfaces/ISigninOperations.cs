using FamilieLaissIdentity.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Interfaces
{
    public interface ISigninOperations
    {
        Task<FamilieLaissSigninResultModel> SigninWithPassword(string userName, string password);
    }
}
