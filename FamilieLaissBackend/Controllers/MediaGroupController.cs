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
            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }
    }
}
