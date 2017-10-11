using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.ViewHelper
{
    public class GenderSelectList
    {
        #region Private Field for Localizer
        private readonly IStringLocalizer<GenderSelectList> Localizer;
        #endregion

        #region C'tor
        public GenderSelectList(IStringLocalizer<GenderSelectList> localizer)
        {
            Localizer = localizer;
        }
        #endregion

        #region Gender-Liste 
        public List<SelectListItem> GenderList
        {
            get
            {
                //Deklaration
                List<SelectListItem> ReturnValue = new List<SelectListItem>();

                //Hinzufügen der Einträge
                ReturnValue.Add(new SelectListItem() { Value = "1", Text = Localizer["Male"] });
                ReturnValue.Add(new SelectListItem() { Value = "2", Text = Localizer["Female"] });

                //Funktionsergebnis zurückliefern
                return ReturnValue;
            }
        }
        #endregion
    }
}
