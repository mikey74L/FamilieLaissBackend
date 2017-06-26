using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Data.Model;
using FamilieLaissBackend.Model.MediaItemFacet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FamilieLaissBackend.Controllers
{
    public class MediaItemFacetController : ApiController
    {
        #region Private Members
        private iUnitOfWorkData _UnitOfWork;
        #endregion

        #region C'tor
        public MediaItemFacetController(iUnitOfWorkData unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        #endregion

        #region REST-API Methods
        [HttpGet]
        public IQueryable<MediaItemFacet> Get()
        {
            return _UnitOfWork.MediaItemFacetRepository.All();
        }

        [HttpGet]
        public MediaItemFacet Get(int id)
        {
            return _UnitOfWork.MediaItemFacetRepository.All().Single(x => x.ID == id);
        }

        [HttpPost]
        //Entspricht einem Insert
        public async Task<MediaItemFacet> Post([FromBody]MediaItemFacetInsertDTO insertDTO)
        {
            //Neue Entity erzeugen und mappen
            MediaItemFacet InsertEntity = AutoMapper.Mapper.Map<MediaItemFacet>(insertDTO);

            //Hinzufügen der neuen Entity zum Store
            _UnitOfWork.MediaItemFacetRepository.Add(InsertEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();

            //Zurückmelden der neuen Entity damit der Client die ID erhält
            return InsertEntity;
        }

        [HttpDelete]
        //Entspricht einem Delete
        public async Task Delete(int id)
        {
            //Ermitteln der Entity
            MediaItemFacet DeleteEntity = _UnitOfWork.MediaItemFacetRepository.All().Single(x => x.ID == id);

            //Löschen des Items
            _UnitOfWork.MediaItemFacetRepository.Delete(DeleteEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
