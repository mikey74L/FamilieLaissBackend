using FamilieLaissBackend.Data.Interface;
using FamilieLaissBackend.Data.Model;
using FamilieLaissBackend.Model.UploadPictureItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FamilieLaissBackend.Controllers
{
    public class UploadPictureItemController : ApiController
    {
        #region Private Members
        private iUnitOfWorkData _UnitOfWork;
        #endregion

        #region C'tor
        public UploadPictureItemController(iUnitOfWorkData unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        #endregion

        #region REST-API Methods
        [HttpGet]
        public IQueryable<UploadPictureItem> Get()
        {
            return _UnitOfWork.UploadPictureItemRepository.All();
        }

        [HttpGet]
        public UploadPictureItem Get(int id)
        {
            return _UnitOfWork.UploadPictureItemRepository.All().Single(x => x.ID == id);
        }

        [HttpDelete]
        //Entspricht einem Delete
        public async Task Delete(int id)
        {
            //Ermitteln der Entity
            UploadPictureItem DeleteEntity = _UnitOfWork.UploadPictureItemRepository.All().Single(x => x.ID == id);

            //Löschen des Items
            _UnitOfWork.UploadPictureItemRepository.Delete(DeleteEntity);

            //Speichern der Änderungen
            await _UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
