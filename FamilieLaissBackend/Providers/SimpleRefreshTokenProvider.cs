using FamilieLaissBackend.Helper;
using FamilieLaissBackend.Model.Account;
using FamilieLaissBackend.Repository;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FamilieLaissBackend.Providers
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            //Ermitteln der Client-ID aus dem Ticket
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            //Wenn keine ClientID gefunden wurde, dann sofort beenden
            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            //Erzeugen einer neuen ID für das Refresh-Token aus der GUID
            var refreshTokenId = Guid.NewGuid().ToString("n");

            using (AuthRepository _repo = new AuthRepository())
            {
                //Ermitteln der Life-Time für das Refresh-Token aus dem OWIN-Context
                var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

                //Erzeugen eines neuen Refresh-Tokens für den Datenbank-Eintrag
                var token = new RefreshToken()
                {
                    Id = GeneralHelper.GetHash(refreshTokenId),
                    ClientId = clientid,
                    Subject = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
                };

                //Setzen der Ablaufzeiten im Ticket
                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

                //Serialisieren des Tickets
                token.ProtectedTicket = context.SerializeTicket();

                //Hinzufügen des Refresh-Tokens zur Datenbank
                var result = await _repo.AddRefreshToken(token);

                //Das Refresh-Token im OWIN-Context erzeugen
                if (result)
                {
                    context.SetToken(refreshTokenId);
                }
            }
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}