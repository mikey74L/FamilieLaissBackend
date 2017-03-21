using FamilieLaissSharedTypes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissAzureOperations.Interface
{
    public interface iAzureStorageOperations
    {
        /// <summary>
        /// Die SAS-URL (Shared Access Signature URL) für den Blob-Container ermitteln
        /// </summary>
        /// <returns>Die formatierte SAS-URL</returns>
        /// <param name="uploadType">Der Upload Typ (1 = Picture / 2 = Video)</param>
        string GetSASForUploadItem(int uploadType);

        /// <summary>
        /// Ermittelt die nächste ID für das Upload-Item aus der Datenbank
        /// </summary>
        /// <param name="uploadType">Der Upload-Type des Items (1 = Picture / 2 = Video)</param>
        /// <returns>Die aus der Datenbank ermittelte ID</returns>
        Task<long> GetIDForUploadItem(int uploadType);

        /// <summary>
        /// Erstellt eine neue Nachricht in der Upload-Queue welche vom WebJob verarbeitet wird
        /// </summary>
        /// <param name="uploadInfo">Enthält die benötigten Informationen für die Message</param>
        Task CreateNewMessageInUploadQueue(NewUploadPictureModel uploadInfo);

        /// <summary>
        /// Löscht ein Upload-Picture aus dem Azure-Blob-Storage
        /// </summary>
        /// <param name="filename">Der Dateiname des Upload-Picture das gelöscht werden soll</param>
        Task DeleteUploadPicture(string filename);
    }
}
