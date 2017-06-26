using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Data.Model;
using FamilieLaissBackend.Model.UploadPictureImageProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FamilieLaissBackend.Controllers
{
    public class UploadPictureImagePropertyController : ApiController
    {
        #region Private Members
        private iUnitOfWorkData _UnitOfWork;
        #endregion

        #region C'tor
        public UploadPictureImagePropertyController(iUnitOfWorkData unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        #endregion

        #region REST-API Methods
        [HttpGet]
        public IQueryable<UploadPictureImageProperty> Get()
        {
            return _UnitOfWork.UploadPictureImagePropertyRepository.All();
        }

        [HttpGet]
        public UploadPictureImageProperty Get(int id)
        {
            return _UnitOfWork.UploadPictureImagePropertyRepository.All().Single(x => x.ID == id);
        }

        [HttpPost]
        //Entspricht einem Insert
        public async Task<UploadPictureImageProperty> Post([FromBody]UploadPictureImagePropertyInsertDTO insertDTO)
        {
            //Neue Entity erzeugen und mappen
            UploadPictureImageProperty InsertEntity = AutoMapper.Mapper.Map<UploadPictureImageProperty>(insertDTO);

            //Hinzufügen der neuen Entity zum Store
            _UnitOfWork.UploadPictureImagePropertyRepository.Add(InsertEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();

            //Zurückmelden der neuen Entity damit der Client die ID erhält
            return InsertEntity;
        }

        [HttpPut]
        //Entspricht einem Update
        public async Task Put(int id, [FromBody]UploadPictureImagePropertyUpdateDTO updateDTO)
        {
            //Ermitteln der Entity
            UploadPictureImageProperty UpdateEntity = _UnitOfWork.UploadPictureImagePropertyRepository.All().Single(x => x.ID == id);

            //Übernehmen der Properties mit Automapper
            AutoMapper.Mapper.Map<UploadPictureImagePropertyUpdateDTO, UploadPictureImageProperty>(updateDTO, UpdateEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
