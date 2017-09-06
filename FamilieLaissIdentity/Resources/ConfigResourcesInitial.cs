using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Resources
{
    public class ConfigResourcesInitial
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource("FamilieLaissAPI", "Familie Laiss API V1.0")
                {
                    Enabled = true,
                    Description = "Die REST-API für die Familie Laiss Web-Site"
                }
            };
        }
    }
}
