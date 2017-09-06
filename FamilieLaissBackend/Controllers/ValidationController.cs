using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Model.Validation;
using System.Linq;
using System.Threading.Tasks;
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

        #region Validation for Facet-Group
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
                switch (valueObject.AdditionalType)
                {
                    //Both
                    case 0:
                        Anzahl = _UnitOfWork.FacetGroupRepository.All().Count(x => x.NameGerman == valueObject.Value);
                        break;
                    default:
                        Anzahl = _UnitOfWork.FacetGroupRepository.All().Count(x => x.NameGerman == valueObject.Value && (x.Type == valueObject.AdditionalType || x.Type == 0));
                        break;
                }
            }
            else
            {
                //Es muss für eine bestehende Entität überprüft werden
                switch (valueObject.AdditionalType)
                {
                    //Both
                    case 0:
                        Anzahl = _UnitOfWork.FacetGroupRepository.All().Count(x => x.NameGerman == valueObject.Value && x.ID != valueObject.ID);
                        break;
                    default:
                        Anzahl = _UnitOfWork.FacetGroupRepository.All().Count(x => x.NameGerman == valueObject.Value && x.ID != valueObject.ID && (x.Type == valueObject.AdditionalType || x.Type == 0));
                        break;
                }
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
                switch (valueObject.AdditionalType)
                {
                    //Both
                    case 0:
                        Anzahl = _UnitOfWork.FacetGroupRepository.All().Count(x => x.NameEnglish == valueObject.Value);
                        break;
                    default:
                        Anzahl = _UnitOfWork.FacetGroupRepository.All().Count(x => x.NameEnglish == valueObject.Value && (x.Type == valueObject.AdditionalType || x.Type == 0));
                        break;
                }
            }
            else
            {
                //Es muss für eine bestehende Entität überprüft werden
                switch (valueObject.AdditionalType)
                {
                    //Both
                    case 0:
                        Anzahl = _UnitOfWork.FacetGroupRepository.All().Count(x => x.NameEnglish == valueObject.Value && x.ID != valueObject.ID);
                        break;
                    default:
                        Anzahl = _UnitOfWork.FacetGroupRepository.All().Count(x => x.NameEnglish == valueObject.Value && x.ID != valueObject.ID && (x.Type == valueObject.AdditionalType || x.Type == 0));
                        break;
                }
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
        #endregion

        #region Validation for Facet-Values
        /// <summary>
        /// Überprüft ob der deutsche Name für einen Kategoriewert schon existiert
        /// </summary>
        /// <param name="valueObject">Das Object mit den zu überprüfenden Werten</param>
        /// <returns>Task wird zurückgeliefert</returns>
        [HttpPost]
        [Route("CheckFacetValueNameGerman")]
        public IHttpActionResult CheckFacetValueNameGerman([FromBody]CheckValueDTO valueObject)
        {
            //Deklaration
            int Anzahl = 0;

            if (valueObject.ID == -1)
            {
                //Es muss für eine neue Entität überprüft werden
                Anzahl = _UnitOfWork.FacetValueRepository.All().Count(x => x.NameGerman == valueObject.Value && x.ID_Group == valueObject.AdditionalType);
            }
            else
            {
                //Es muss für eine bestehende Entität überprüft werden
                Anzahl = _UnitOfWork.FacetValueRepository.All().Count(x => x.NameGerman == valueObject.Value && x.ID != valueObject.ID && x.ID_Group == valueObject.AdditionalType);
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
        /// Überprüft ob der englische Name für einen Kategoriewert schon existiert
        /// </summary>
        /// <param name="valueObject">Das Object mit den zu überprüfenden Werten</param>
        /// <returns>Task wird zurückgeliefert</returns>
        [HttpPost]
        [Route("CheckFacetValueNameEnglish")]
        public IHttpActionResult CheckFacetValueNameEnglish([FromBody]CheckValueDTO valueObject)
        {
            //Deklaration
            int Anzahl = 0;

            if (valueObject.ID == -1)
            {
                //Es muss für eine neue Entität überprüft werden
                Anzahl = _UnitOfWork.FacetValueRepository.All().Count(x => x.NameEnglish == valueObject.Value && x.ID_Group == valueObject.AdditionalType);
            }
            else
            {
                //Es muss für eine bestehende Entität überprüft werden
                Anzahl = _UnitOfWork.FacetValueRepository.All().Count(x => x.NameEnglish == valueObject.Value && x.ID != valueObject.ID && x.ID_Group == valueObject.AdditionalType);
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
        #endregion

        #region Validation for Media-Groups
        /// <summary>
        /// Überprüft ob der deutsche Name für ein Album schon existiert
        /// </summary>
        /// <param name="valueObject">Das Object mit den zu überprüfenden Werten</param>
        /// <returns>Task wird zurückgeliefert</returns>
        [HttpPost]
        [Route("CheckMediaGroupNameGerman")]
        public IHttpActionResult CheckMediaGroupNameGerman([FromBody]CheckValueDTO valueObject)
        {
            //Deklaration
            int Anzahl = 0;

            if (valueObject.ID == -1)
            {
                //Es muss für eine neue Entität überprüft werden
                Anzahl = _UnitOfWork.MediaGroupRepository.All().Count(x => x.NameGerman == valueObject.Value && x.Type == valueObject.AdditionalType);
            }
            else
            {
                //Es muss für eine bestehende Entität überprüft werden
                Anzahl = _UnitOfWork.MediaGroupRepository.All().Count(x => x.NameGerman == valueObject.Value && x.ID != valueObject.ID && x.Type == valueObject.AdditionalType);
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
        /// Überprüft ob der englische Name für ein Album schon existiert
        /// </summary>
        /// <param name="valueObject">Das Object mit den zu überprüfenden Werten</param>
        /// <returns>Task wird zurückgeliefert</returns>
        [HttpPost]
        [Route("CheckMediaGroupNameEnglish")]
        public IHttpActionResult CheckMediaGroupNameEnglish([FromBody]CheckValueDTO valueObject)
        {
            //Deklaration
            int Anzahl = 0;

            if (valueObject.ID == -1)
            {
                //Es muss für eine neue Entität überprüft werden
                Anzahl = _UnitOfWork.MediaGroupRepository.All().Count(x => x.NameEnglish == valueObject.Value && x.Type == valueObject.AdditionalType);
            }
            else
            {
                //Es muss für eine bestehende Entität überprüft werden
                Anzahl = _UnitOfWork.MediaGroupRepository.All().Count(x => x.NameEnglish == valueObject.Value && x.ID != valueObject.ID && x.Type == valueObject.AdditionalType);
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
        #endregion

        #region Validation for Media-Items
        /// <summary>
        /// Überprüft ob der deutsche Name für ein Medien-Element schon existiert
        /// </summary>
        /// <param name="valueObject">Das Object mit den zu überprüfenden Werten</param>
        /// <returns>Task wird zurückgeliefert</returns>
        [HttpPost]
        [Route("CheckMediaItemNameGerman")]
        public IHttpActionResult CheckMediaItemNameGerman([FromBody]CheckValueDTO valueObject)
        {
            //Deklaration
            int Anzahl = 0;

            if (valueObject.ID == -1)
            {
                //Es muss für eine neue Entität überprüft werden
                Anzahl = _UnitOfWork.MediaItemRepository.All().Count(x => x.NameGerman == valueObject.Value && x.ID_Group == valueObject.AdditionalType);
            }
            else
            {
                //Es muss für eine bestehende Entität überprüft werden
                Anzahl = _UnitOfWork.MediaItemRepository.All().Count(x => x.NameGerman == valueObject.Value && x.ID != valueObject.ID && x.ID_Group == valueObject.AdditionalType);
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
        /// Überprüft ob der englische Name für ein Medien-Element schon existiert
        /// </summary>
        /// <param name="valueObject">Das Object mit den zu überprüfenden Werten</param>
        /// <returns>Task wird zurückgeliefert</returns>
        [HttpPost]
        [Route("CheckMediaItemNameEnglish")]
        public IHttpActionResult CheckMediaItemNameEnglish([FromBody]CheckValueDTO valueObject)
        {
            //Deklaration
            int Anzahl = 0;

            if (valueObject.ID == -1)
            {
                //Es muss für eine neue Entität überprüft werden
                Anzahl = _UnitOfWork.MediaItemRepository.All().Count(x => x.NameEnglish == valueObject.Value && x.ID_Group == valueObject.AdditionalType);
            }
            else
            {
                //Es muss für eine bestehende Entität überprüft werden
                Anzahl = _UnitOfWork.MediaItemRepository.All().Count(x => x.NameEnglish == valueObject.Value && x.ID != valueObject.ID && x.ID_Group == valueObject.AdditionalType);
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
        #endregion
    }
}
