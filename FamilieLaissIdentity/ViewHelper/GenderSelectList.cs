using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.ViewHelper
{
    public class GenderSelectList
    {
        public List<SelectListItem> GenderList { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "Mann" },
            new SelectListItem { Value = "1", Text = "Frau" },
         };
    }
}
