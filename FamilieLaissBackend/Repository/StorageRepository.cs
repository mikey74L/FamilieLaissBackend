using FamilieLaissBackend.Interface;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using FamilieLaissSharedTypes.Model;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.WindowsAzure.Storage.Queue;

namespace FamilieLaissBackend.Repository
{
    public class StorageRepository : iStorageOperations
    {
        #region Private Methods
        /// <summary>
        /// Erstellen einer SAS-URL (Shared Access Signature URL) für einen Blob-Container
        /// </summary>
        /// <param name="blobContainer">Der Blob-Container für den die SAS erstellt werden soll</param>
        /// <param name="permission">Die Zugriffsrechte welche erteilt werden sollen</param>
        /// <param name="duration">Die Anzahl Stunden für welche die SAS-URL ihre Gültigkeit behält</param>
        /// <returns>Die formatierte SAS-URL</returns>
        private static string GetSaSForBlobContainer(CloudBlobContainer blobContainer, SharedAccessBlobPermissions permission, int duration)
        {
            //Erstellen der SAS für den angegebenen Container anhand der Parameter für permission und duration
            var sas = blobContainer.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = permission,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(duration),
            });

            //Funktionsergebnis ist die formatierte SAS-URL
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", blobContainer.Uri, sas);
        }
        #endregion

        #region Interface iStorageOperations
        /// <summary>
        /// Die SAS-URL (Shared Access Signature URL) für den Blob-Container erzeugen
        /// </summary>
        /// <param name="uploadType">Der Upload Typ (1 = Picture / 2 = Video)</param>
        /// <returns>Die formatierte SAS-URL</returns>
        public string GetSASForUploadItem(int uploadType)
        {
            //Den Storage-Account instanziieren
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionLibrary"));

            //Den Blob-Client instanziieren
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Den Blob-Container instanziieren
            CloudBlobContainer container;
                switch (uploadType)
            {
                case 1:
                    //Es handelt sich um ein Upload-Picture
                    container = blobClient.GetContainerReference("upload-picture");
                    break;
                case 2:
                    //Es handelt sich um ein Upload-Video
                    container = blobClient.GetContainerReference("upload-video");
                    break;
                default:
                    //Als Default wird der Picture-Container genommen
                    container = blobClient.GetContainerReference("upload-picture");
                    break;
            }
                
            //Ermitteln der SAS (Shared Access Signature) für den Blob-Container mit Hilfe der Hilfs-Methode
            return GetSaSForBlobContainer(container, SharedAccessBlobPermissions.Write, 12);
        }

        /// <summary>
        /// Ermittelt die nächste ID für das Upload-Item aus der Datenbank
        /// </summary>
        /// <param name="uploadType">Der Upload-Type des Items (1 = Picture / 2 = Video)</param>
        /// <returns>Die aus der Datenbank ermittelte ID</returns>
        public async Task<long> GetIDForUploadItem(int uploadType)
        {
            //Deklaration
            long IDUploadItem = 0;

            //Datenbankverbindung ermitteln
            string ConnString = System.Configuration.ConfigurationManager.ConnectionStrings["FamilieLaiss"].ConnectionString;

            //ID aus der Sequence in der Datenbank ermitteln
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                using (SqlCommand Command = Conn.CreateCommand())
                {
                    Command.CommandType = System.Data.CommandType.Text;
                    if (uploadType == 1)
                    {
                        Command.CommandText = "SELECT NEXT VALUE FOR SEQ_Upload_Picture";
                    }
                    else
                    {
                        Command.CommandText = "SELECT NEXT VALUE FOR SEQ_Upload_Video";
                    }
                    await Conn.OpenAsync();
                    IDUploadItem = Convert.ToInt64(await Command.ExecuteScalarAsync());
                    Conn.Close();
                }
            }

            //Funktionsergebnis
            return IDUploadItem;
        }

        /// <summary>
        /// Erstellt eine neue Nachricht in der Upload-Queue welche vom WebJob verarbeitet wird
        /// </summary>
        /// <param name="uploadInfo">Enthält die benötigten Informationen für die Message</param>
        public async Task CreateNewMessageInUploadQueue(NewUploadPictureModel uploadInfo)
        {
            //Herstellen einer Verbindung zum Azure-Storage
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionLibrary"));

            //Erstellen des Clients für die Warteschlange
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            //Erstellen des Zugriffobjektes für die Warteschlange
            CloudQueue queue;
            if (uploadInfo.UploadType == FamilieLaissSharedTypes.Enum.enUploadType.Picture)
            {
                queue = queueClient.GetQueueReference("upload-picture");
            }
            else
            {
                queue = queueClient.GetQueueReference("upload-video");
            }

            //Erstellen der Warteschlange falls diese noch nicht existiert
            await queue.CreateIfNotExistsAsync();

            //Erstellen einer neuen Nachricht
            CloudQueueMessage message = new CloudQueueMessage(Newtonsoft.Json.JsonConvert.SerializeObject(uploadInfo));

            //Hinzufügen der Nachricht zur Warteschlange
            await queue.AddMessageAsync(message);
        }
        #endregion
    }
}