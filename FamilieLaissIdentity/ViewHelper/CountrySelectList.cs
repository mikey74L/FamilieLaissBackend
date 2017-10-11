using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.ViewHelper
{
    public class CountrySelectList
    {
        #region Private Field for Localizer
        private readonly IStringLocalizer<CountrySelectList> Localizer;
        #endregion

        #region C'tor
        public CountrySelectList(IStringLocalizer<CountrySelectList> localizer)
        {
            Localizer = localizer;
        }
        #endregion
     
        #region Country-Liste 
        public List<SelectListItem> CountryList
        {
            get
            {
                //Deklaration
                List<SelectListItem> ReturnValue = new List<SelectListItem>();

                //Hinzufügen der Einträge
                ReturnValue.Add(new SelectListItem() { Value = "AF", Text = Localizer["AF"] });
                ReturnValue.Add(new SelectListItem() { Value = "AL", Text = Localizer["AL"] });
                ReturnValue.Add(new SelectListItem() { Value = "DZ", Text = Localizer["DZ"] });
                ReturnValue.Add(new SelectListItem() { Value = "AD", Text = Localizer["AD"] });
                ReturnValue.Add(new SelectListItem() { Value = "AO", Text = Localizer["AO"] });
                ReturnValue.Add(new SelectListItem() { Value = "AI", Text = Localizer["AI"] });
                ReturnValue.Add(new SelectListItem() { Value = "AG", Text = Localizer["AG"] });
                ReturnValue.Add(new SelectListItem() { Value = "AR", Text = Localizer["AR"] });
                ReturnValue.Add(new SelectListItem() { Value = "AM", Text = Localizer["AM"] });
                ReturnValue.Add(new SelectListItem() { Value = "AW", Text = Localizer["AW"] });
                ReturnValue.Add(new SelectListItem() { Value = "AU", Text = Localizer["AU"] });
                ReturnValue.Add(new SelectListItem() { Value = "AT", Text = Localizer["AT"] });
                ReturnValue.Add(new SelectListItem() { Value = "AZ", Text = Localizer["AZ"] });
                ReturnValue.Add(new SelectListItem() { Value = "BS", Text = Localizer["BS"] });
                ReturnValue.Add(new SelectListItem() { Value = "BH", Text = Localizer["BH"] });
                ReturnValue.Add(new SelectListItem() { Value = "BD", Text = Localizer["BD"] });
                ReturnValue.Add(new SelectListItem() { Value = "BB", Text = Localizer["BB"] });
                ReturnValue.Add(new SelectListItem() { Value = "BY", Text = Localizer["BY"] });
                ReturnValue.Add(new SelectListItem() { Value = "BE", Text = Localizer["BE"] });
                ReturnValue.Add(new SelectListItem() { Value = "BZ", Text = Localizer["BZ"] });
                ReturnValue.Add(new SelectListItem() { Value = "BJ", Text = Localizer["BJ"] });
                ReturnValue.Add(new SelectListItem() { Value = "BM", Text = Localizer["BM"] });
                ReturnValue.Add(new SelectListItem() { Value = "BT", Text = Localizer["BT"] });
                ReturnValue.Add(new SelectListItem() { Value = "BO", Text = Localizer["BO"] });
                ReturnValue.Add(new SelectListItem() { Value = "BQ", Text = Localizer["BQ"] });
                ReturnValue.Add(new SelectListItem() { Value = "BA", Text = Localizer["BA"] });
                ReturnValue.Add(new SelectListItem() { Value = "BW", Text = Localizer["BW"] });
                ReturnValue.Add(new SelectListItem() { Value = "BV", Text = Localizer["BV"] });
                ReturnValue.Add(new SelectListItem() { Value = "BR", Text = Localizer["BR"] });
                ReturnValue.Add(new SelectListItem() { Value = "BN", Text = Localizer["BN"] });
                ReturnValue.Add(new SelectListItem() { Value = "BG", Text = Localizer["BG"] });
                ReturnValue.Add(new SelectListItem() { Value = "BF", Text = Localizer["BF"] });
                ReturnValue.Add(new SelectListItem() { Value = "BI", Text = Localizer["BI"] });
                ReturnValue.Add(new SelectListItem() { Value = "KH", Text = Localizer["KH"] });
                ReturnValue.Add(new SelectListItem() { Value = "CM", Text = Localizer["CM"] });
                ReturnValue.Add(new SelectListItem() { Value = "CA", Text = Localizer["CA"] });
                ReturnValue.Add(new SelectListItem() { Value = "CV", Text = Localizer["CV"] });
                ReturnValue.Add(new SelectListItem() { Value = "KY", Text = Localizer["KY"] });
                ReturnValue.Add(new SelectListItem() { Value = "CF", Text = Localizer["CF"] });
                ReturnValue.Add(new SelectListItem() { Value = "TD", Text = Localizer["TD"] });
                ReturnValue.Add(new SelectListItem() { Value = "CL", Text = Localizer["CL"] });
                ReturnValue.Add(new SelectListItem() { Value = "CN", Text = Localizer["CN"] });
                ReturnValue.Add(new SelectListItem() { Value = "CO", Text = Localizer["CO"] });
                ReturnValue.Add(new SelectListItem() { Value = "KM", Text = Localizer["KM"] });
                ReturnValue.Add(new SelectListItem() { Value = "CG", Text = Localizer["CG"] });
                ReturnValue.Add(new SelectListItem() { Value = "CK", Text = Localizer["CK"] });
                ReturnValue.Add(new SelectListItem() { Value = "CR", Text = Localizer["CR"] });
                ReturnValue.Add(new SelectListItem() { Value = "CI", Text = Localizer["CI"] });
                ReturnValue.Add(new SelectListItem() { Value = "HR", Text = Localizer["HR"] });
                ReturnValue.Add(new SelectListItem() { Value = "CU", Text = Localizer["CU"] });
                ReturnValue.Add(new SelectListItem() { Value = "CW", Text = Localizer["CW"] });
                ReturnValue.Add(new SelectListItem() { Value = "CY", Text = Localizer["CY"] });
                ReturnValue.Add(new SelectListItem() { Value = "CZ", Text = Localizer["CZ"] });
                ReturnValue.Add(new SelectListItem() { Value = "KP", Text = Localizer["KP"] });
                ReturnValue.Add(new SelectListItem() { Value = "LA", Text = Localizer["LA"] });
                ReturnValue.Add(new SelectListItem() { Value = "CD", Text = Localizer["CD"] });
                ReturnValue.Add(new SelectListItem() { Value = "DK", Text = Localizer["DK"] });
                ReturnValue.Add(new SelectListItem() { Value = "DJ", Text = Localizer["DJ"] });
                ReturnValue.Add(new SelectListItem() { Value = "DM", Text = Localizer["DM"] });
                ReturnValue.Add(new SelectListItem() { Value = "DO", Text = Localizer["DO"] });
                ReturnValue.Add(new SelectListItem() { Value = "EC", Text = Localizer["EC"] });
                ReturnValue.Add(new SelectListItem() { Value = "EG", Text = Localizer["EG"] });
                ReturnValue.Add(new SelectListItem() { Value = "SV", Text = Localizer["SV"] });
                ReturnValue.Add(new SelectListItem() { Value = "GQ", Text = Localizer["GQ"] });
                ReturnValue.Add(new SelectListItem() { Value = "ER", Text = Localizer["ER"] });
                ReturnValue.Add(new SelectListItem() { Value = "EE", Text = Localizer["EE"] });
                ReturnValue.Add(new SelectListItem() { Value = "ET", Text = Localizer["ET"] });
                ReturnValue.Add(new SelectListItem() { Value = "FK", Text = Localizer["FK"] });
                ReturnValue.Add(new SelectListItem() { Value = "FO", Text = Localizer["FO"] });
                ReturnValue.Add(new SelectListItem() { Value = "FJ", Text = Localizer["FJ"] });
                ReturnValue.Add(new SelectListItem() { Value = "FI", Text = Localizer["FI"] });
                ReturnValue.Add(new SelectListItem() { Value = "FR", Text = Localizer["FR"] });
                ReturnValue.Add(new SelectListItem() { Value = "GA", Text = Localizer["GA"] });
                ReturnValue.Add(new SelectListItem() { Value = "GM", Text = Localizer["GM"] });
                ReturnValue.Add(new SelectListItem() { Value = "GE", Text = Localizer["GE"] });
                ReturnValue.Add(new SelectListItem() { Value = "DE", Text = Localizer["DE"] });
                ReturnValue.Add(new SelectListItem() { Value = "GH", Text = Localizer["GH"] });
                ReturnValue.Add(new SelectListItem() { Value = "GI", Text = Localizer["GI"] });
                ReturnValue.Add(new SelectListItem() { Value = "GR", Text = Localizer["GR"] });
                ReturnValue.Add(new SelectListItem() { Value = "GL", Text = Localizer["GL"] });
                ReturnValue.Add(new SelectListItem() { Value = "GD", Text = Localizer["GD"] });
                ReturnValue.Add(new SelectListItem() { Value = "GT", Text = Localizer["GT"] });
                ReturnValue.Add(new SelectListItem() { Value = "GG", Text = Localizer["GG"] });
                ReturnValue.Add(new SelectListItem() { Value = "GN", Text = Localizer["GN"] });
                ReturnValue.Add(new SelectListItem() { Value = "GW", Text = Localizer["GW"] });
                ReturnValue.Add(new SelectListItem() { Value = "GY", Text = Localizer["GY"] });
                ReturnValue.Add(new SelectListItem() { Value = "HT", Text = Localizer["HT"] });
                ReturnValue.Add(new SelectListItem() { Value = "VA", Text = Localizer["VA"] });
                ReturnValue.Add(new SelectListItem() { Value = "HN", Text = Localizer["HN"] });
                ReturnValue.Add(new SelectListItem() { Value = "HU", Text = Localizer["HU"] });
                ReturnValue.Add(new SelectListItem() { Value = "IS", Text = Localizer["IS"] });
                ReturnValue.Add(new SelectListItem() { Value = "IN", Text = Localizer["IN"] });
                ReturnValue.Add(new SelectListItem() { Value = "ID", Text = Localizer["ID"] });
                ReturnValue.Add(new SelectListItem() { Value = "IR", Text = Localizer["IR"] });
                ReturnValue.Add(new SelectListItem() { Value = "IQ", Text = Localizer["IQ"] });
                ReturnValue.Add(new SelectListItem() { Value = "IE", Text = Localizer["IE"] });
                ReturnValue.Add(new SelectListItem() { Value = "IM", Text = Localizer["IM"] });
                ReturnValue.Add(new SelectListItem() { Value = "IL", Text = Localizer["IL"] });
                ReturnValue.Add(new SelectListItem() { Value = "IT", Text = Localizer["IT"] });
                ReturnValue.Add(new SelectListItem() { Value = "JM", Text = Localizer["JM"] });
                ReturnValue.Add(new SelectListItem() { Value = "JP", Text = Localizer["JP"] });
                ReturnValue.Add(new SelectListItem() { Value = "JE", Text = Localizer["JE"] });
                ReturnValue.Add(new SelectListItem() { Value = "JO", Text = Localizer["JO"] });
                ReturnValue.Add(new SelectListItem() { Value = "KZ", Text = Localizer["KZ"] });
                ReturnValue.Add(new SelectListItem() { Value = "KE", Text = Localizer["KE"] });
                ReturnValue.Add(new SelectListItem() { Value = "KI", Text = Localizer["KI"] });
                ReturnValue.Add(new SelectListItem() { Value = "KW", Text = Localizer["KW"] });
                ReturnValue.Add(new SelectListItem() { Value = "KG", Text = Localizer["KG"] });
                ReturnValue.Add(new SelectListItem() { Value = "LV", Text = Localizer["LV"] });
                ReturnValue.Add(new SelectListItem() { Value = "LB", Text = Localizer["LB"] });
                ReturnValue.Add(new SelectListItem() { Value = "LS", Text = Localizer["LS"] });
                ReturnValue.Add(new SelectListItem() { Value = "LR", Text = Localizer["LR"] });
                ReturnValue.Add(new SelectListItem() { Value = "LY", Text = Localizer["LY"] });
                ReturnValue.Add(new SelectListItem() { Value = "LI", Text = Localizer["LI"] });
                ReturnValue.Add(new SelectListItem() { Value = "LT", Text = Localizer["LT"] });
                ReturnValue.Add(new SelectListItem() { Value = "LU", Text = Localizer["LU"] });
                ReturnValue.Add(new SelectListItem() { Value = "MO", Text = Localizer["MO"] });
                ReturnValue.Add(new SelectListItem() { Value = "MG", Text = Localizer["MG"] });
                ReturnValue.Add(new SelectListItem() { Value = "MW", Text = Localizer["MW"] });
                ReturnValue.Add(new SelectListItem() { Value = "MY", Text = Localizer["MY"] });
                ReturnValue.Add(new SelectListItem() { Value = "MV", Text = Localizer["MV"] });
                ReturnValue.Add(new SelectListItem() { Value = "ML", Text = Localizer["ML"] });
                ReturnValue.Add(new SelectListItem() { Value = "MT", Text = Localizer["MT"] });
                ReturnValue.Add(new SelectListItem() { Value = "MR", Text = Localizer["MR"] });
                ReturnValue.Add(new SelectListItem() { Value = "MU", Text = Localizer["MU"] });
                ReturnValue.Add(new SelectListItem() { Value = "MX", Text = Localizer["MX"] });
                ReturnValue.Add(new SelectListItem() { Value = "MC", Text = Localizer["MC"] });
                ReturnValue.Add(new SelectListItem() { Value = "MN", Text = Localizer["MN"] });
                ReturnValue.Add(new SelectListItem() { Value = "ME", Text = Localizer["ME"] });
                ReturnValue.Add(new SelectListItem() { Value = "MS", Text = Localizer["MS"] });
                ReturnValue.Add(new SelectListItem() { Value = "MA", Text = Localizer["MA"] });
                ReturnValue.Add(new SelectListItem() { Value = "MZ", Text = Localizer["MZ"] });
                ReturnValue.Add(new SelectListItem() { Value = "MM", Text = Localizer["MM"] });
                ReturnValue.Add(new SelectListItem() { Value = "NA", Text = Localizer["NA"] });
                ReturnValue.Add(new SelectListItem() { Value = "NR", Text = Localizer["NR"] });
                ReturnValue.Add(new SelectListItem() { Value = "NP", Text = Localizer["NP"] });
                ReturnValue.Add(new SelectListItem() { Value = "NL", Text = Localizer["NL"] });
                ReturnValue.Add(new SelectListItem() { Value = "NZ", Text = Localizer["NZ"] });
                ReturnValue.Add(new SelectListItem() { Value = "NI", Text = Localizer["NI"] });
                ReturnValue.Add(new SelectListItem() { Value = "NE", Text = Localizer["NE"] });
                ReturnValue.Add(new SelectListItem() { Value = "NG", Text = Localizer["NG"] });
                ReturnValue.Add(new SelectListItem() { Value = "MP", Text = Localizer["MP"] });
                ReturnValue.Add(new SelectListItem() { Value = "NO", Text = Localizer["NO"] });
                ReturnValue.Add(new SelectListItem() { Value = "OM", Text = Localizer["OM"] });
                ReturnValue.Add(new SelectListItem() { Value = "PK", Text = Localizer["PK"] });
                ReturnValue.Add(new SelectListItem() { Value = "PW", Text = Localizer["PW"] });
                ReturnValue.Add(new SelectListItem() { Value = "PA", Text = Localizer["PA"] });
                ReturnValue.Add(new SelectListItem() { Value = "PG", Text = Localizer["PG"] });
                ReturnValue.Add(new SelectListItem() { Value = "PY", Text = Localizer["PY"] });
                ReturnValue.Add(new SelectListItem() { Value = "PE", Text = Localizer["PE"] });
                ReturnValue.Add(new SelectListItem() { Value = "PH", Text = Localizer["PH"] });
                ReturnValue.Add(new SelectListItem() { Value = "PL", Text = Localizer["PL"] });
                ReturnValue.Add(new SelectListItem() { Value = "PT", Text = Localizer["PT"] });
                ReturnValue.Add(new SelectListItem() { Value = "QA", Text = Localizer["QA"] });
                ReturnValue.Add(new SelectListItem() { Value = "KR", Text = Localizer["KR"] });
                ReturnValue.Add(new SelectListItem() { Value = "MD", Text = Localizer["MD"] });
                ReturnValue.Add(new SelectListItem() { Value = "RO", Text = Localizer["RO"] });
                ReturnValue.Add(new SelectListItem() { Value = "RU", Text = Localizer["RU"] });
                ReturnValue.Add(new SelectListItem() { Value = "RW", Text = Localizer["RW"] });
                ReturnValue.Add(new SelectListItem() { Value = "SH", Text = Localizer["SH"] });
                ReturnValue.Add(new SelectListItem() { Value = "KN", Text = Localizer["KN"] });
                ReturnValue.Add(new SelectListItem() { Value = "LC", Text = Localizer["LC"] });
                ReturnValue.Add(new SelectListItem() { Value = "VC", Text = Localizer["VC"] });
                ReturnValue.Add(new SelectListItem() { Value = "WS", Text = Localizer["WS"] });
                ReturnValue.Add(new SelectListItem() { Value = "SM", Text = Localizer["SM"] });
                ReturnValue.Add(new SelectListItem() { Value = "ST", Text = Localizer["ST"] });
                ReturnValue.Add(new SelectListItem() { Value = "SA", Text = Localizer["SA"] });
                ReturnValue.Add(new SelectListItem() { Value = "SN", Text = Localizer["SN"] });
                ReturnValue.Add(new SelectListItem() { Value = "RS", Text = Localizer["RS"] });
                ReturnValue.Add(new SelectListItem() { Value = "SC", Text = Localizer["SC"] });
                ReturnValue.Add(new SelectListItem() { Value = "SL", Text = Localizer["SL"] });
                ReturnValue.Add(new SelectListItem() { Value = "SG", Text = Localizer["SG"] });
                ReturnValue.Add(new SelectListItem() { Value = "SX", Text = Localizer["SX"] });
                ReturnValue.Add(new SelectListItem() { Value = "SK", Text = Localizer["SK"] });
                ReturnValue.Add(new SelectListItem() { Value = "SI", Text = Localizer["SI"] });
                ReturnValue.Add(new SelectListItem() { Value = "SB", Text = Localizer["SB"] });
                ReturnValue.Add(new SelectListItem() { Value = "SO", Text = Localizer["SO"] });
                ReturnValue.Add(new SelectListItem() { Value = "ZA", Text = Localizer["ZA"] });
                ReturnValue.Add(new SelectListItem() { Value = "GS", Text = Localizer["GS"] });
                ReturnValue.Add(new SelectListItem() { Value = "SS", Text = Localizer["SS"] });
                ReturnValue.Add(new SelectListItem() { Value = "ES", Text = Localizer["ES"] });
                ReturnValue.Add(new SelectListItem() { Value = "LK", Text = Localizer["LK"] });
                ReturnValue.Add(new SelectListItem() { Value = "SD", Text = Localizer["SD"] });
                ReturnValue.Add(new SelectListItem() { Value = "SR", Text = Localizer["SR"] });
                ReturnValue.Add(new SelectListItem() { Value = "SZ", Text = Localizer["SZ"] });
                ReturnValue.Add(new SelectListItem() { Value = "SE", Text = Localizer["SE"] });
                ReturnValue.Add(new SelectListItem() { Value = "CH", Text = Localizer["CH"] });
                ReturnValue.Add(new SelectListItem() { Value = "SY", Text = Localizer["SY"] });
                ReturnValue.Add(new SelectListItem() { Value = "TW", Text = Localizer["TW"] });
                ReturnValue.Add(new SelectListItem() { Value = "TJ", Text = Localizer["TJ"] });
                ReturnValue.Add(new SelectListItem() { Value = "TH", Text = Localizer["TH"] });
                ReturnValue.Add(new SelectListItem() { Value = "MK", Text = Localizer["MK"] });
                ReturnValue.Add(new SelectListItem() { Value = "HK", Text = Localizer["HK"] });
                ReturnValue.Add(new SelectListItem() { Value = "TL", Text = Localizer["TL"] });
                ReturnValue.Add(new SelectListItem() { Value = "TG", Text = Localizer["TG"] });
                ReturnValue.Add(new SelectListItem() { Value = "TO", Text = Localizer["TO"] });
                ReturnValue.Add(new SelectListItem() { Value = "TT", Text = Localizer["TT"] });
                ReturnValue.Add(new SelectListItem() { Value = "TN", Text = Localizer["TN"] });
                ReturnValue.Add(new SelectListItem() { Value = "TR", Text = Localizer["TR"] });
                ReturnValue.Add(new SelectListItem() { Value = "TM", Text = Localizer["TM"] });
                ReturnValue.Add(new SelectListItem() { Value = "TC", Text = Localizer["TC"] });
                ReturnValue.Add(new SelectListItem() { Value = "TV", Text = Localizer["TV"] });
                ReturnValue.Add(new SelectListItem() { Value = "UG", Text = Localizer["UG"] });
                ReturnValue.Add(new SelectListItem() { Value = "UA", Text = Localizer["UA"] });
                ReturnValue.Add(new SelectListItem() { Value = "AE", Text = Localizer["AE"] });
                ReturnValue.Add(new SelectListItem() { Value = "GB", Text = Localizer["GB"] });
                ReturnValue.Add(new SelectListItem() { Value = "TZ", Text = Localizer["TZ"] });
                ReturnValue.Add(new SelectListItem() { Value = "US", Text = Localizer["US"] });
                ReturnValue.Add(new SelectListItem() { Value = "UY", Text = Localizer["UY"] });
                ReturnValue.Add(new SelectListItem() { Value = "UZ", Text = Localizer["UZ"] });
                ReturnValue.Add(new SelectListItem() { Value = "VU", Text = Localizer["VU"] });
                ReturnValue.Add(new SelectListItem() { Value = "VE", Text = Localizer["VE"] });
                ReturnValue.Add(new SelectListItem() { Value = "VN", Text = Localizer["VN"] });
                ReturnValue.Add(new SelectListItem() { Value = "VG", Text = Localizer["VG"] });
                ReturnValue.Add(new SelectListItem() { Value = "EH", Text = Localizer["EH"] });
                ReturnValue.Add(new SelectListItem() { Value = "YE", Text = Localizer["YE"] });
                ReturnValue.Add(new SelectListItem() { Value = "ZM", Text = Localizer["ZM"] });
                ReturnValue.Add(new SelectListItem() { Value = "ZW", Text = Localizer["ZW"] });

                //Funktionsergebnis zurückliefern
                return ReturnValue;
            }
        }
        #endregion
    }
}
