using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.Account
{
    public class AccountNotificationModel
    {
        #region Private Members
        private int _Geschlecht;
        private int _SecurityQuestion;
        private string _Land;
        #endregion

        #region C'tor
        public AccountNotificationModel(int geschlecht, string land, int securityQuestion)
        {
            _Geschlecht = geschlecht;
            _Land = land;
            _SecurityQuestion = securityQuestion;
        }
        #endregion

        #region Public Methods
        public string UserID { get; set; }
        public string VerificationToken { get; set; }
        public string URL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Geschlecht
        {
            get
            {
                if (_Geschlecht == 1)
                {
                    return FamilieLaissBackend.Resources.Gender_Resources.Men;
                }
                else
                {
                    return FamilieLaissBackend.Resources.Gender_Resources.Women;
                }
            }
        }
        public string Vorname { get; set; }
        public string Familienname { get; set; }
        public string Strasse { get; set; }
        public string HNR { get; set; }
        public string PLZ { get; set; }
        public string Stadt { get; set; }
        public string Land
        {
            get
            {
                string RetVal = "";
                switch (_Land)
                {
                    case "AF":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AF;
                        break;
                    case "AL":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AL;
                        break;
                    case "DZ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.DZ;
                        break;
                    case "AD":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AD;
                        break;
                    case "AO":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AO;
                        break;
                    case "AI":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AI;
                        break;
                    case "AG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AG;
                        break;
                    case "AR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AR;
                        break;
                    case "AM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AM;
                        break;
                    case "AW":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AW;
                        break;
                    case "AU":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AU;
                        break;
                    case "AT":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AT;
                        break;
                    case "AZ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AZ;
                        break;
                    case "BS":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BS;
                        break;
                    case "BH":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BH;
                        break;
                    case "BD":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BD;
                        break;
                    case "BB":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BB;
                        break;
                    case "BY":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BY;
                        break;
                    case "BE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BE;
                        break;
                    case "BZ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BZ;
                        break;
                    case "BJ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BJ;
                        break;
                    case "BM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BM;
                        break;
                    case "BT":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BT;
                        break;
                    case "BO":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BO;
                        break;
                    case "BQ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BQ;
                        break;
                    case "BA":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BA;
                        break;
                    case "BW":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BW;
                        break;
                    case "BV":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BV;
                        break;
                    case "BR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BR;
                        break;
                    case "BN":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BN;
                        break;
                    case "BG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BG;
                        break;
                    case "BF":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BF;
                        break;
                    case "BI":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.BI;
                        break;
                    case "KH":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.KH;
                        break;
                    case "CM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CM;
                        break;
                    case "CA":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CA;
                        break;
                    case "CV":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CV;
                        break;
                    case "KY":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.KY;
                        break;
                    case "CF":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CF;
                        break;
                    case "TD":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TD;
                        break;
                    case "CL":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CL;
                        break;
                    case "CN":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CN;
                        break;
                    case "CO":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CO;
                        break;
                    case "KM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.KM;
                        break;
                    case "CG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CG;
                        break;
                    case "CK":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CK;
                        break;
                    case "CR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CR;
                        break;
                    case "CI":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CI;
                        break;
                    case "HR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.HR;
                        break;
                    case "CU":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CU;
                        break;
                    case "CW":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CW;
                        break;
                    case "CY":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CY;
                        break;
                    case "CZ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CZ;
                        break;
                    case "KP":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.KP;
                        break;
                    case "LA":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.LA;
                        break;
                    case "CD":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CD;
                        break;
                    case "DK":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.DK;
                        break;
                    case "DJ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.DJ;
                        break;
                    case "DM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.DM;
                        break;
                    case "DO":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.DO;
                        break;
                    case "EC":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.EC;
                        break;
                    case "EG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.EG;
                        break;
                    case "SV":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SV;
                        break;
                    case "GQ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GQ;
                        break;
                    case "ER":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.ER;
                        break;
                    case "EE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.EE;
                        break;
                    case "ET":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.ET;
                        break;
                    case "FK":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.FK;
                        break;
                    case "FO":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.FO;
                        break;
                    case "FJ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.FJ;
                        break;
                    case "FI":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.FI;
                        break;
                    case "FR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.FR;
                        break;
                    case "GA":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GA;
                        break;
                    case "GM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GM;
                        break;
                    case "GE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GE;
                        break;
                    case "DE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.DE;
                        break;
                    case "GH":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GH;
                        break;
                    case "GI":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GI;
                        break;
                    case "GR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GR;
                        break;
                    case "GL":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GL;
                        break;
                    case "GD":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GD;
                        break;
                    case "GT":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GT;
                        break;
                    case "GG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GG;
                        break;
                    case "GN":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GN;
                        break;
                    case "GW":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GW;
                        break;
                    case "GY":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GY;
                        break;
                    case "HT":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.HT;
                        break;
                    case "VA":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.VA;
                        break;
                    case "HN":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.HN;
                        break;
                    case "HU":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.HU;
                        break;
                    case "IS":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.IS;
                        break;
                    case "IN":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.IN;
                        break;
                    case "ID":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.ID;
                        break;
                    case "IR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.IR;
                        break;
                    case "IQ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.IQ;
                        break;
                    case "IE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.IE;
                        break;
                    case "IM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.IM;
                        break;
                    case "IL":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.IL;
                        break;
                    case "IT":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.IT;
                        break;
                    case "JM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.JM;
                        break;
                    case "JP":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.JP;
                        break;
                    case "JE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.JE;
                        break;
                    case "JO":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.JO;
                        break;
                    case "KZ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.KZ;
                        break;
                    case "KE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.KE;
                        break;
                    case "KI":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.KI;
                        break;
                    case "KW":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.KW;
                        break;
                    case "KG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.KG;
                        break;
                    case "LV":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.LV;
                        break;
                    case "LB":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.LB;
                        break;
                    case "LS":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.LS;
                        break;
                    case "LR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.LR;
                        break;
                    case "LY":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.LY;
                        break;
                    case "LI":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.LI;
                        break;
                    case "LT":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.LT;
                        break;
                    case "LU":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.LU;
                        break;
                    case "MO":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MO;
                        break;
                    case "MG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MG;
                        break;
                    case "MW":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MW;
                        break;
                    case "MY":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MY;
                        break;
                    case "MV":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MV;
                        break;
                    case "ML":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.ML;
                        break;
                    case "MT":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MT;
                        break;
                    case "MR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MR;
                        break;
                    case "MU":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MU;
                        break;
                    case "MX":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MX;
                        break;
                    case "MC":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MC;
                        break;
                    case "MN":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MN;
                        break;
                    case "ME":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.ME;
                        break;
                    case "MS":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MS;
                        break;
                    case "MA":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MA;
                        break;
                    case "MZ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MZ;
                        break;
                    case "MM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MM;
                        break;
                    case "NA":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.NA;
                        break;
                    case "NR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.NR;
                        break;
                    case "NP":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.NP;
                        break;
                    case "NL":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.NL;
                        break;
                    case "NZ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.NZ;
                        break;
                    case "NI":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.NI;
                        break;
                    case "NE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.NE;
                        break;
                    case "NG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.NG;
                        break;
                    case "MP":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MP;
                        break;
                    case "NO":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.NO;
                        break;
                    case "OM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.OM;
                        break;
                    case "PK":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.PK;
                        break;
                    case "PW":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.PW;
                        break;
                    case "PA":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.PA;
                        break;
                    case "PG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.PG;
                        break;
                    case "PY":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.PY;
                        break;
                    case "PE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.PE;
                        break;
                    case "PH":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.PH;
                        break;
                    case "PL":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.PL;
                        break;
                    case "PT":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.PT;
                        break;
                    case "QA":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.QA;
                        break;
                    case "KR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.KR;
                        break;
                    case "MD":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MD;
                        break;
                    case "RO":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.RO;
                        break;
                    case "RU":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.RU;
                        break;
                    case "RW":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.RW;
                        break;
                    case "SH":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SH;
                        break;
                    case "KN":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.KN;
                        break;
                    case "LC":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.LC;
                        break;
                    case "VC":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.VC;
                        break;
                    case "WS":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.WS;
                        break;
                    case "SM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SM;
                        break;
                    case "ST":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.ST;
                        break;
                    case "SA":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SA;
                        break;
                    case "SN":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SN;
                        break;
                    case "RS":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.RS;
                        break;
                    case "SC":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SC;
                        break;
                    case "SL":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SL;
                        break;
                    case "SG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SG;
                        break;
                    case "SX":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SX;
                        break;
                    case "SK":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SK;
                        break;
                    case "SI":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SI;
                        break;
                    case "SB":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SB;
                        break;
                    case "SO":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SO;
                        break;
                    case "ZA":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.ZA;
                        break;
                    case "GS":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GS;
                        break;
                    case "SS":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SS;
                        break;
                    case "ES":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.ES;
                        break;
                    case "LK":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.LK;
                        break;
                    case "SD":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SD;
                        break;
                    case "SR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SR;
                        break;
                    case "SZ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SZ;
                        break;
                    case "SE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SE;
                        break;
                    case "CH":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.CH;
                        break;
                    case "SY":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.SY;
                        break;
                    case "TW":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TW;
                        break;
                    case "TJ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TJ;
                        break;
                    case "TH":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TH;
                        break;
                    case "MK":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.MK;
                        break;
                    case "HK":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.HK;
                        break;
                    case "TL":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TL;
                        break;
                    case "TG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TG;
                        break;
                    case "TO":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TO;
                        break;
                    case "TT":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TT;
                        break;
                    case "TN":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TN;
                        break;
                    case "TR":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TR;
                        break;
                    case "TM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TM;
                        break;
                    case "TC":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TC;
                        break;
                    case "TV":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TV;
                        break;
                    case "UG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.UG;
                        break;
                    case "UA":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.UA;
                        break;
                    case "AE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.AE;
                        break;
                    case "GB":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.GB;
                        break;
                    case "TZ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.TZ;
                        break;
                    case "US":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.US;
                        break;
                    case "UY":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.UY;
                        break;
                    case "UZ":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.UZ;
                        break;
                    case "VU":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.VU;
                        break;
                    case "VE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.VE;
                        break;
                    case "VN":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.VN;
                        break;
                    case "VG":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.VG;
                        break;
                    case "EH":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.EH;
                        break;
                    case "YE":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.YE;
                        break;
                    case "ZM":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.ZM;
                        break;
                    case "ZW":
                        RetVal = FamilieLaissBackend.Resources.Country_Resources.ZW;
                        break;
                }
                return RetVal;
            }
        }
        public string SecurityQuestion
        {
            get
            {
                switch (_SecurityQuestion)
                {
                    case 1:
                        return FamilieLaissBackend.Resources.SecurityQuestion_Resources.Question_1;
                    case 2:
                        return FamilieLaissBackend.Resources.SecurityQuestion_Resources.Question_2;
                    case 3:
                        return FamilieLaissBackend.Resources.SecurityQuestion_Resources.Question_3;
                    case 4:
                        return FamilieLaissBackend.Resources.SecurityQuestion_Resources.Question_4;
                    case 5:
                        return FamilieLaissBackend.Resources.SecurityQuestion_Resources.Question_5;
                    case 6:
                        return FamilieLaissBackend.Resources.SecurityQuestion_Resources.Question_6;
                    case 7:
                        return FamilieLaissBackend.Resources.SecurityQuestion_Resources.Question_7;
                    case 8:
                        return FamilieLaissBackend.Resources.SecurityQuestion_Resources.Question_8;
                    case 9:
                        return FamilieLaissBackend.Resources.SecurityQuestion_Resources.Question_9;
                    case 10:
                        return FamilieLaissBackend.Resources.SecurityQuestion_Resources.Question_10;
                    default:
                        return "";
                }
            }
        }
        public string SecurityAnswer { get; set; }
        #endregion
    }
}