using FamilieLaissIdentity.Attributes;
using FamilieLaissIdentity.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Controllers
{
    [SecurityHeaders]
    public class HomeController : Controller
    {
        #region Private Members
        private readonly IIdentityServerInteractionService _interaction;
        #endregion

        #region C'tor
        public HomeController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            //Rendern der View
            return View();
        }
        #endregion

        #region About
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            //Rendern der View
            return View();
        }
        #endregion

        #region Contact
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            //Rendern der View
            return View();
        }
        #endregion

        #region Error
        public async Task<IActionResult> Error(string errorId)
        {
            //Das Viewmodel Erstellen
            var vm = new ErrorViewModel();

            //Fehlerdetails von IdentityServer ermitteln
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            //Rendern der View
            return View("Error", vm);
        }
        #endregion
    }
}