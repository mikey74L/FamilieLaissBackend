using AutoMapper;
using AutoMapper.QueryableExtensions;
using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Data.Model;
using FamilieLaissBackend.Model.FacetValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FamilieLaissBackend.Controllers
{
    public class FacetValueController : ApiController
    {
        #region Private Members
        private iUnitOfWorkData _UnitOfWork;
        #endregion

        #region C'tor
        public FacetValueController(iUnitOfWorkData unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        #endregion

        [HttpGet]
        public IQueryable<FacetValue> Get()
        {
            return _UnitOfWork.FacetValueRepository.All();
        }

        [HttpGet]
        public FacetValue Get(int id)
        {
            return _UnitOfWork.FacetValueRepository.All().Single(x => x.ID == id);
        }

        [HttpPost]
        //Entspricht einem Insert
        public async Task<FacetValue> Post([FromBody]FacetValueInsertDTO insertDTO)
        {
            //Neue Entity erzeugen und mappen
            FacetValue InsertEntity = AutoMapper.Mapper.Map<FacetValue>(insertDTO);

            //Hinzufügen der neuen Entity zum Store
            _UnitOfWork.FacetValueRepository.Add(InsertEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();

            //Zurückmelden der neuen Entity damit der Client die ID erhält
            return InsertEntity;
        }

        [HttpPut]
        //Entspricht einem Update
        public async Task Put(int id, [FromBody]FacetValueUpdateDTO updateDTO)
        {
            //Ermitteln der Entity
            FacetValue UpdateEntity = _UnitOfWork.FacetValueRepository.All().Single(x => x.ID == id);

            //Übernehmen der Properties mit Automapper
            AutoMapper.Mapper.Map<FacetValueUpdateDTO, FacetValue>(updateDTO, UpdateEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }

        [HttpDelete]
        //Entspricht einem Delete
        public async Task Delete(int id)
        {
            //Ermitteln der Entity
            FacetValue DeleteEntity = _UnitOfWork.FacetValueRepository.All().Single(x => x.ID == id);

            //Löschen des Items
            _UnitOfWork.FacetValueRepository.Delete(DeleteEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }
    }
}
