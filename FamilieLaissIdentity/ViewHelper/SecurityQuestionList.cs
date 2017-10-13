using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FamilieLaissIdentity.ViewHelper
{
    public class SecurityQuestionList
    {
        #region Private Field for Localizer
        private readonly IStringLocalizer<SecurityQuestionList> Localizer;
        #endregion
      
        #region C'tor
        public SecurityQuestionList(IStringLocalizer<SecurityQuestionList> localizer)
        {
            Localizer = localizer;
        }
        #endregion
    
        #region Question-Liste 
        public List<SelectListItem> QuestionList
        {
            get
            {
                //Deklaration
                List<SelectListItem> ReturnValue = new List<SelectListItem>();

                //Hinzufügen der Einträge
                ReturnValue.Add(new SelectListItem() { Value = "1", Text = Localizer["1"] });
                ReturnValue.Add(new SelectListItem() { Value = "2", Text = Localizer["2"] });
                ReturnValue.Add(new SelectListItem() { Value = "3", Text = Localizer["3"] });
                ReturnValue.Add(new SelectListItem() { Value = "4", Text = Localizer["4"] });
                ReturnValue.Add(new SelectListItem() { Value = "5", Text = Localizer["5"] });
                ReturnValue.Add(new SelectListItem() { Value = "6", Text = Localizer["6"] });
                ReturnValue.Add(new SelectListItem() { Value = "7", Text = Localizer["7"] });
                ReturnValue.Add(new SelectListItem() { Value = "8", Text = Localizer["8"] });
                ReturnValue.Add(new SelectListItem() { Value = "9", Text = Localizer["9"] });
                ReturnValue.Add(new SelectListItem() { Value = "10", Text = Localizer["10"] });

                //Funktionsergebnis zurückliefern
                return ReturnValue;
            }
        }
        #endregion
    }
}
