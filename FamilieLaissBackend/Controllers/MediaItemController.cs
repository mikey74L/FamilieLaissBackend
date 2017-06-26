using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Data.Model;
using FamilieLaissBackend.Model.MediaItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FamilieLaissBackend.Controllers
{
    public class MediaItemController : ApiController
    {
        #region Private Members
        private iUnitOfWorkData _UnitOfWork;
        #endregion

        #region C'tor
        public MediaItemController(iUnitOfWorkData unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        #endregion

        #region REST-API Methods
        [HttpGet]
        public IQueryable<MediaItem> Get()
        {
            return _UnitOfWork.MediaItemRepository.All();
        }

        [HttpGet]
        public MediaItem Get(int id)
        {
            return _UnitOfWork.MediaItemRepository.All().Single(x => x.ID == id);
        }

        [HttpPost]
        //Entspricht einem Insert
        public async Task<MediaItem> Post([FromBody]MediaItemInsertDTO insertDTO)
        {
            //Neue Entity erzeugen und mappen
            MediaItem InsertEntity = AutoMapper.Mapper.Map<MediaItem>(insertDTO);

            //Hinzufügen der neuen Entity zum Store
            _UnitOfWork.MediaItemRepository.Add(InsertEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();

            //Zurückmelden der neuen Entity damit der Client die ID erhält
            return InsertEntity;
        }

        [HttpPut]
        //Entspricht einem Update
        public async Task Put(int id, [FromBody]MediaItemUpdateDTO updateDTO)
        {
            //Ermitteln der Entity
            MediaItem UpdateEntity = _UnitOfWork.MediaItemRepository.All().Single(x => x.ID == id);

            //Übernehmen der Properties mit Automapper
            AutoMapper.Mapper.Map<MediaItemUpdateDTO, MediaItem>(updateDTO, UpdateEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }

        [HttpDelete]
        //Entspricht einem Delete
        public async Task Delete(int id)
        {
            //Ermitteln der Entity
            MediaItem DeleteEntity = _UnitOfWork.MediaItemRepository.All().Single(x => x.ID == id);

            //Löschen des Items
            _UnitOfWork.MediaItemRepository.Delete(DeleteEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
