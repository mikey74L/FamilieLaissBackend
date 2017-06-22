using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Model.FacetGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using FamilieLaissBackend.Data.Model;
using System.Threading.Tasks;
using FamilieLaissBackend.Model.Validation;

namespace FamilieLaissBackend.Controllers
{
    public class FacetGroupController : ApiController
    {
        #region Private Members
        private iUnitOfWorkData _UnitOfWork;
        #endregion

        #region C'tor
        public FacetGroupController(iUnitOfWorkData unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        #endregion

        #region REST-API Methods
        [HttpGet]
        public IQueryable<FacetGroup> Get()
        {
            return _UnitOfWork.FacetGroupRepository.All();
        }

        [HttpGet]
        public FacetGroup Get(int id)
        {
            return _UnitOfWork.FacetGroupRepository.All().Single(x => x.ID == id);
        }

        [HttpPost]
        //Entspricht einem Insert
        public async Task<FacetGroup> Post([FromBody]FacetGroupInsertDTO insertDTO)
        {
            //Neue Entity erzeugen und mappen
            FacetGroup InsertEntity = AutoMapper.Mapper.Map<FacetGroup>(insertDTO);

            //Hinzufügen der neuen Entity zum Store
            _UnitOfWork.FacetGroupRepository.Add(InsertEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();

            //Zurückmelden der neuen Entity damit der Client die ID erhält
            return InsertEntity;
        }

        [HttpPut]
        //Entspricht einem Update
        public async Task Put(int id, [FromBody]FacetGroupUpdateDTO updateDTO)
        {
            //Ermitteln der Entity
            FacetGroup UpdateEntity = _UnitOfWork.FacetGroupRepository.All().Single(x => x.ID == id);

            //Übernehmen der Properties mit Automapper
            AutoMapper.Mapper.Map<FacetGroupUpdateDTO, FacetGroup>(updateDTO, UpdateEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }

        [HttpDelete]
        //Entspricht einem Delete
        public async Task Delete(int id)
        {
            //Ermitteln der Entity
            FacetGroup DeleteEntity = _UnitOfWork.FacetGroupRepository.All().Single(x => x.ID == id);

            //Löschen des Items
            _UnitOfWork.FacetGroupRepository.Delete(DeleteEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
