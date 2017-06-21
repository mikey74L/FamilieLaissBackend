using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Model.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FamilieLaissBackend.Controllers
{
    [RoutePrefix("api/Validation")]
    public class ValidationController : ApiController
    {
        #region Private Members
        private iUnitOfWorkData _UnitOfWork;
        #endregion

        #region C'tor
        public ValidationController(iUnitOfWorkData unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        #endregion
  
        /// <summary>
        /// Überprüft ob der deutsche Name für eine Kategorie schon existiert
        /// </summary>
        /// <param name="valueObject">Das Object mit den zu überprüfenden Werten</param>
        /// <returns>Task wird zurückgeliefert</returns>
        [HttpPost]
        [Route("CheckFacetGroupNameGerman")]
        public IHttpActionResult CheckFacetGroupNameGerman([FromBody]CheckValueDTO valueObject)
        {
            //Deklaration
            int Anzahl = 0;

            if (valueObject.ID == -1)
            {
                //Es muss für eine neue Entität überprüft werden
                Anzahl = _UnitOfWork.FacetGroupRepository.All().Count(x => x.NameGerman == valueObject.Value && x.Type == valueObject.AdditionalType);
            }
            else
            {
                //Es muss für eine bestehende Entität überprüft werden
                Anzahl = _UnitOfWork.FacetGroupRepository.All().Count(x => x.NameGerman == valueObject.Value && x.ID != valueObject.ID && x.Type == valueObject.AdditionalType);
            }

            //Wenn die Anzahl 0 ist, ist alles ok
            if (Anzahl == 0)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
  
        /// <summary>
        /// Überprüft ob der englische Name für eine Kategorie schon existiert
        /// </summary>
        /// <param name="valueObject">Das Object mit den zu überprüfenden Werten</param>
        /// <returns>Task wird zurückgeliefert</returns>
        [HttpPost]
        [Route("CheckFacetGroupNameEnglish")]
        public IHttpActionResult CheckFacetGroupNameEnglish([FromBody]CheckValueDTO valueObject)
        {
            //Deklaration
            int Anzahl = 0;

            if (valueObject.ID == -1)
            {
                //Es muss für eine neue Entität überprüft werden
                Anzahl = _UnitOfWork.FacetGroupRepository.All().Count(x => x.NameEnglish == valueObject.Value && x.Type == valueObject.AdditionalType);
            }
            else
            {
                //Es muss für eine bestehende Entität überprüft werden
                Anzahl = _UnitOfWork.FacetGroupRepository.All().Count(x => x.NameEnglish == valueObject.Value && x.ID != valueObject.ID && x.Type == valueObject.AdditionalType);
            }

            //Wenn die Anzahl 0 ist, ist alles ok
            if (Anzahl == 0)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
