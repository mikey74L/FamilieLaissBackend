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

        #region API Methods
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
        public async void Post([FromBody]string value)
        {
            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }

        [HttpPut]
        public async void Put(int id, [FromBody]string value)
        {
            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }

        [HttpDelete]
        public async void Delete(int id)
        {
            //Löschen des Items
            _UnitOfWork.FacetGroupRepository.Delete(null);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
