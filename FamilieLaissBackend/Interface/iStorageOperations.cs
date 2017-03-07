using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissBackend.Interface
{
    public interface iStorageOperations
    {
        /// <summary>
        /// Die SAS-URL (Shared Access Signature URL) für den Blob-Container der "Upload-Picture" ermitteln
        /// </summary>
        /// <returns>Die formatierte SAS-URL</returns>
        string GetSASForUploadPicture();
    }
}
