using FamilieLaissBackend.Interface;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Repository
{
    public class StorageRepository : iStorageOperations
    {
        /// <summary>
        /// Die SAS-URL (Shared Access Signature URL) für den Blob-Container der "Upload-Picture" erzeugen
        /// </summary>
        /// <returns>Die formatierte SAS-URL</returns>
        public string GetSASForUploadPicture()
        {
            //Den Storage-Account instanziieren
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionLibrary"));

            //Den Blob-Client instanziieren
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Den Blob-Container für die "Upload-Picture" instanziieren
            CloudBlobContainer container = blobClient.GetContainerReference("upload-picture");

            //Ermitteln der SAS (Shared Access Signature) für den Blob-Container mit Hilfe der Hilfs-Methode
            return GetSaSForBlobContainer(container, SharedAccessBlobPermissions.Write, 15);
        }

        /// <summary>
        /// Erstellen einer SAS-URL (Shared Access Signature URL) für einen Blob-Container
        /// </summary>
        /// <param name="blobContainer">Der Blob-Container für den die SAS erstellt werden soll</param>
        /// <param name="permission">Die Zugriffsrechte welche erteilt werden sollen</param>
        /// <param name="duration">Die Anzahl Minuten für welche die SAS-URL ihre Gültigkeit behält</param>
        /// <returns>Die formatierte SAS-URL</returns>
        private static string GetSaSForBlobContainer(CloudBlobContainer blobContainer, SharedAccessBlobPermissions permission, int duration)
        {
            //Erstellen der SAS für den angegebenen Container anhand der Parameter für permission und duration
            var sas = blobContainer.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = permission,
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(duration),
            });

            //Funktionsergebnis ist die formatierte SAS-URL
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", blobContainer.Uri, sas);
        }
    }
}