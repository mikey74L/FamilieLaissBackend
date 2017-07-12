using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FamilieLaissBackend
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //Show custom error page
            filters.Add(new HandleErrorAttribute());

            //Require Https always
            if (!HttpContext.Current.IsDebuggingEnabled)
            {
                filters.Add(new FamilieLaissBackend.Filter.RequireHttpsAttribute());
            }
        }
    }
}
