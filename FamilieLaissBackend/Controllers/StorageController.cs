using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.SqlClient;
using FamilieLaissSharedTypes.Model;
using FamilieLaissAzureOperations.Interface;

namespace FamilieLaissBackend.Controllers
{
    [RoutePrefix("api/storage")]
    public class StorageController : ApiController
    {
        #region Private Members
        private iAzureStorageOperations _StorageOperations;
        #endregion

        #region C'tor
        public StorageController(iAzureStorageOperations storageOperations)
        {
            _StorageOperations = storageOperations;
        }
        #endregion

        #region SAS
        [HttpGet]
        [Route("GetSASForUploadItem")]
        public IHttpActionResult GetSASForUploadItem(int uploadType)
        {
            try
            {
                //SAS-URL ermitteln
                string SAS_URL = _StorageOperations.GetSASForUploadItem(uploadType);

                //Result ist die SAS-URL
                return Ok(SAS_URL);
            }
            catch
            {
                return InternalServerError();
            }
        }
        #endregion

        #region IDs ermitteln
        [HttpGet]
        [Route("GetIDForUploadItem")]
        public async Task<IHttpActionResult> GetIDForUploadItem(int uploadType)
        {
            try
            {
                //Deklaration
                long IDUploadItem;

                //Ermitteln der Upload-ID
                IDUploadItem = await _StorageOperations.GetIDForUploadItem(uploadType);

                //Result ist die ermittelte ID
                return Ok(IDUploadItem);
            }
            catch
            {
                return InternalServerError();
            }
        }
        #endregion

        #region Create Azure Queue Entry
        [HttpPost]
        [Route("WriteToUploadQueue")]
        public async Task<IHttpActionResult> WriteToUploadQueue(NewUploadPictureModel uploadInfo)
        {
            try
            {
                //Erstellen der neuen Nachricht in der Queue
                await _StorageOperations.CreateNewMessageInUploadQueue(uploadInfo);

                //Result zurückliefern
                return Ok();
            }
            catch
            {
                //Result zurückliefern
                return InternalServerError();
            }
        }
        #endregion
    }
}
