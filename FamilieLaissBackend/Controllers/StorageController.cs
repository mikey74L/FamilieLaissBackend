using FamilieLaissBackend.Interface;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FamilieLaissBackend.Controllers
{
    public class StorageController : ApiController
    {
        #region Private Members
        private iStorageOperations _StorageOperations;
        #endregion

        #region C'tor
        public StorageController(iStorageOperations storageOperations)
        {
            _StorageOperations = storageOperations;
        }
        #endregion

        #region SAS
        [HttpGet]
        public IHttpActionResult GetSASForUploadPicture()
        {
            try
            {
                //SAS-URL ermitteln
                string SAS_URL = _StorageOperations.GetSASForUploadPicture();

                //Result ist die SAS-URL
                return Ok(SAS_URL);
            }
            catch
            {
                return InternalServerError();
            }
        }
        #endregion

    }
}
