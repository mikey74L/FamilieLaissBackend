using AutoMapper;
using AutoMapper.QueryableExtensions;
using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Data.Model;
using FamilieLaissBackend.Model.MediaGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FamilieLaissBackend.Controllers
{
    public class MediaGroupController : ApiController
    {
        #region Private Members
        private iUnitOfWorkData _UnitOfWork;
        #endregion

        #region C'tor
        public MediaGroupController(iUnitOfWorkData unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        #endregion

        #region REST-API Methods
        [HttpGet]
        public IQueryable<MediaGroup> Get()
        {
            return _UnitOfWork.MediaGroupRepository.All();
        }

        [HttpGet]
        public MediaGroup Get(int id)
        {
            return _UnitOfWork.MediaGroupRepository.All().Single(x => x.ID == id);
        }

        [HttpPost]
        //Entspricht einem Insert
        public async Task<MediaGroup> Post([FromBody]MediaGroupInsertDTO insertDTO)
        {
            //Neue Entity erzeugen und mappen
            MediaGroup InsertEntity = AutoMapper.Mapper.Map<MediaGroup>(insertDTO);

            //Hinzufügen der neuen Entity zum Store
            _UnitOfWork.MediaGroupRepository.Add(InsertEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();

            //Zurückmelden der neuen Entity damit der Client die ID erhält
            return InsertEntity;
        }

        [HttpPut]
        //Entspricht einem Update
        public async Task Put(int id, [FromBody]MediaGroupUpdateDTO updateDTO)
        {
            //Ermitteln der Entity
            MediaGroup UpdateEntity = _UnitOfWork.MediaGroupRepository.All().Single(x => x.ID == id);

            //Übernehmen der Properties mit Automapper
            AutoMapper.Mapper.Map<MediaGroupUpdateDTO, MediaGroup>(updateDTO, UpdateEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }

        [HttpDelete]
        //Entspricht einem Delete
        public async Task Delete(int id)
        {
            //Ermitteln der Entity
            MediaGroup DeleteEntity = _UnitOfWork.MediaGroupRepository.All().Single(x => x.ID == id);

            //Löschen des Items
            _UnitOfWork.MediaGroupRepository.Delete(DeleteEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
