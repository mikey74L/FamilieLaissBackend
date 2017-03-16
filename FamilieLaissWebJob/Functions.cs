using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using FamilieLaissSharedTypes.Model;
using System.Drawing;
using FamilieLaissBackend.Data.Model;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Azure;

namespace FamilieLaissWebJob
{
    public class Functions
    {
        //Wird aufgerufen wenn eine neue Nachricht für ein Upload-Picture eingestellt wird
        public static void NewUploadPicture([QueueTrigger("upload-picture")] NewUploadPictureModel newMessage,
                                            [Blob("upload-picture/{BlobName}", FileAccess.Read)] Stream uploadPicture)
        {
            //Log schreiben
            Console.WriteLine("New upload picture with following parameter uploaded");
            Console.WriteLine("ID: " + newMessage.ID.ToString());
            Console.WriteLine("Original-Name: " + newMessage.OriginalName);
            Console.WriteLine("Blob-Name: " + newMessage.BlobName);

            //Ermitteln der Breite und Höhe des Bildes
            Console.WriteLine("Extracting picture size from blob");
            Rectangle PictureSize = GetWidthAndHeightForImage(uploadPicture);
            Console.WriteLine("Picture size (width / height): " + PictureSize.Width + " / " + PictureSize.Height);

            //Die Position des Streams wieder auf 0 setzen, da es sonst zu Problemen
            //mit weiteren Operationen gibt die mit dem Stream arbeiten
            uploadPicture.Position = 0;

            //Erstellen des Datenbankeintrags
            Console.WriteLine("Save upload picture in database");
            SaveUploadPictureInDatabase(newMessage, PictureSize);
            Console.WriteLine("Upload picture successfully saved in database");

            //Ermitteln der Exif-Daten für das Bild
            Console.WriteLine("Read EXIF-Info from blob");
            UploadPictureExifInfo ExifInfo = GetExifInfoForImage(uploadPicture);

            //Speichern der Exif-Informationen in der Datenbank
            Console.WriteLine("Write EXIF-Info to database");
            SaveExifInfoInDatabase(newMessage.ID, ExifInfo);
            Console.WriteLine("Upload picture job successfully done");
        }

        //Ermittelt die Höhe und die Breite eines Bildes für einen Azure-Stream (Blob)
        private static Rectangle GetWidthAndHeightForImage(Stream pictureStream)
        {
            //Das Bitmap aus dem Stream erzeugen
            Bitmap bmp = new Bitmap(pictureStream);

            //Ermitteln der Höhe und Breite des Bildes
            Rectangle RetVal = new Rectangle();
            RetVal.Height = bmp.Height;
            RetVal.Width = bmp.Width;

            //Funktionsergebnis
            return RetVal;
        }

        //Ermittelt die Exif-Informationen aus dem Bild für einen Azure-Stream (Blob)
        private static UploadPictureExifInfo GetExifInfoForImage(Stream pictureStream)
        {
            //Deklaration
            UploadPictureExifInfo info = new UploadPictureExifInfo();

            //Auslesen der Metadaten zum Blob über den Stream
            IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(pictureStream);

            //Erstellen des Directory für die IFD0 Werte
            var Ifd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();

            //Auslesen des Tags für "Make"
            try
            {
                info.Make = Ifd0Directory?.GetString(ExifDirectoryBase.TagMake);
            }
            catch
            {
                info.Make = "";
            }

            //Auslesen des Tags für "Model"
            try
            {
                info.Model = Ifd0Directory?.GetString(ExifDirectoryBase.TagModel);
            }
            catch
            {
                info.Model = "";
            }

            //Auslesen des Tags für "Resolution X"
            try
            {
                info.Resolution_X = Ifd0Directory?.GetDouble(ExifDirectoryBase.TagXResolution);
            }
            catch
            {
                info.Resolution_X = null;
            }
            
            //Auslesen des Tags für "Resolution Y"
            try
            {
                info.Resolution_Y = Ifd0Directory?.GetDouble(ExifDirectoryBase.TagYResolution);
            }
            catch
            {
                info.Resolution_Y = null;
            }
            
            //Auslesen des Tags für "Resolution Unit"
            try
            {
                info.Resolution_Unit = Ifd0Directory?.GetString(ExifDirectoryBase.TagResolutionUnit);
            }
            catch
            {
                info.Resolution_Unit = "";
            }

            //Auslesen des Tags für "Orientation"
            try
            {
                info.Orientation = Ifd0Directory?.GetInt32(ExifDirectoryBase.TagOrientation);
            }
            catch
            {
                info.Orientation = null;
            }

            //Auslesen des Tags für "Date / Time"
            try
            {
                info.DDL_Recorded = Ifd0Directory?.GetDateTime(ExifDirectoryBase.TagDateTime);
            }
            catch
            {
                info.DDL_Recorded = null;
            }

            //Erstellen des Directory für die SUBIFD Werte
            var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

            //Auslesen des Tags für "Exposure Time"
            try
            {
                info.Exposure_Time = subIfdDirectory?.GetDouble(ExifDirectoryBase.TagExposureTime);
            }
            catch
            {
                info.Exposure_Time = null;
            }

            //Auslesen des Tags für "F_Number"
            try
            {
                info.F_Number = subIfdDirectory?.GetDouble(ExifDirectoryBase.TagFNumber);
            }
            catch
            {
                info.F_Number = null;
            }

            //Auslesen des Tags für "Exposure Programm"
            try
            {
                info.Exposure_Programm = subIfdDirectory?.GetInt32(ExifDirectoryBase.TagExposureProgram);
            }
            catch
            {
                info.Exposure_Programm = null;
            }

            //Auslesen des Tags für "ISO"
            try
            {
                info.ISO_Sensitivity = subIfdDirectory?.GetInt32(ExifDirectoryBase.TagIsoEquivalent);
            }
            catch
            {
                info.ISO_Sensitivity = null;
            }

            //Auslesen des Tags für "Shutter Speed"
            try
            {
                info.Shutter_Speed = subIfdDirectory?.GetDouble(ExifDirectoryBase.TagShutterSpeed);
            }
            catch
            {
                info.Shutter_Speed = null;
            }

            //Auslesen des Tags für "Metering Mode"
            try
            {
                info.Metering_Mode = subIfdDirectory?.GetInt32(ExifDirectoryBase.TagMeteringMode);
            }
            catch
            {
                info.Metering_Mode = null;
            }

            //Auslesen des Tags für "Flash Mode"
            try
            {
                info.Flash_Mode = subIfdDirectory?.GetInt32(ExifDirectoryBase.TagFlash);
            }
            catch
            {
                info.Flash_Mode = null;
            }

            //Auslesen des Tags für "Focal Length"
            try
            {
                info.Focal_Length = subIfdDirectory?.GetDouble(ExifDirectoryBase.TagFocalLength);
            }
            catch
            {
                info.Focal_Length = null;
            }

            //Auslesen des Tags für "Sensing Mode"
            try
            {
                info.Sensing_Mode = subIfdDirectory?.GetInt32(ExifDirectoryBase.TagSensingMethod);
            }
            catch
            {
                info.Sensing_Mode = null;
            }

            //Auslesen des Tags für "Exposure Mode"
            try
            {
                info.Exposure_Mode = subIfdDirectory?.GetInt32(ExifDirectoryBase.TagExposureMode);
            }
            catch
            {
                info.Exposure_Mode = null;
            }

            //Auslesen des Tags für "White Balance Mode"
            try
            {
                info.White_Balance_Mode = subIfdDirectory?.GetInt32(ExifDirectoryBase.TagWhiteBalanceMode);
            }
            catch
            {
                info.White_Balance_Mode = null;
            }
        
            //Auslesen des Tags für "Sharpness"
            try
            {
                info.Sharpness = subIfdDirectory?.GetInt32(ExifDirectoryBase.TagSharpness);
            }
            catch
            {
                info.Sharpness = null;
            }

            //Erstellen des Directory für die GPS Werte
            var GPSDirectory = directories.OfType<GpsDirectory>().FirstOrDefault();

            //Emitteln der Geo-Location
            try
            {
                GeoLocation GPSLocation = GPSDirectory.GetGeoLocation();
                info.Longitute = GPSLocation.Longitude;
                info.Latitude = GPSLocation.Latitude;
            }
            catch
            {
                info.Longitute = null;
                info.Latitude = null;
            }

            //Funktionsergebnis zurückliefern
            return info;
        }

        //Erstellt einen neuen Eintrag für ein Upload-Picture in der Datenbank
        private static void SaveUploadPictureInDatabase(NewUploadPictureModel messageInfo, Rectangle pictureSize)
        {
            //Erstellen und initialisieren des neuen Objektes
            UploadPictureItem NewItem = new UploadPictureItem();
            NewItem.ID = messageInfo.ID;
            NewItem.HeightOriginal = pictureSize.Height;
            NewItem.WidthOriginal = pictureSize.Width;
            NewItem.NameOriginal = messageInfo.OriginalName;

            //Einen Datenbankkontext für das Entity Framework erstellen
            FamilieLaissEntities Context = new FamilieLaissEntities();

            //Hinzufügen des neuen Items zu den Upload-Pictures
            Context.UploadPictureItems.Add(NewItem);

            //Speichern der Änderungen in der Datenbank
            Context.SaveChanges();
        }

        //Speichert die Exif-Informationen für das Bild in der Datenbank
        private static void SaveExifInfoInDatabase(long ID, UploadPictureExifInfo exif_Info)
        {
            //Ermitteln des Datenbank-Connection-Strings
            string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FamilieLaissDatabase"].ConnectionString;

            //Speichern der Daten in der Datenbank mit Hilfe der Stored-Procedure
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    //Setzen der Prozedure
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "sp_Add_Exif_Data";

                    //Definieren der Parameter
                    command.Parameters.Add("p_ID", SqlDbType.BigInt).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Make", SqlDbType.NVarChar).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Model", SqlDbType.NVarChar).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Resolution_X", SqlDbType.Float).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Resolution_Y", SqlDbType.Float).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Resolution_Unit", SqlDbType.NVarChar).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Orientation", SqlDbType.SmallInt).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_DDL_Recorded", SqlDbType.DateTimeOffset).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Exposure_Time", SqlDbType.Float).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Exposure_Programm", SqlDbType.SmallInt).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Exposure_Mode", SqlDbType.SmallInt).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_F_Number", SqlDbType.Float).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_ISO", SqlDbType.Int).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Shutter_Speed", SqlDbType.Float).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Metering_Mode", SqlDbType.SmallInt).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Flash_Mode", SqlDbType.SmallInt).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Focal_Length", SqlDbType.Float).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Sensing_Mode", SqlDbType.SmallInt).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_White_Balance_Mode", SqlDbType.SmallInt).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Sharpness", SqlDbType.SmallInt).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Latitude", SqlDbType.Float).Direction = ParameterDirection.Input;
                    command.Parameters.Add("p_Longitude", SqlDbType.Float).Direction = ParameterDirection.Input;

                    //Setzen der Parameter Values
                    command.Parameters["p_ID"].Value = ID;
                    if (string.IsNullOrEmpty(exif_Info.Make))
                    {
                        command.Parameters["p_Make"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Make"].Value = exif_Info.Make;
                    }
                    if (string.IsNullOrEmpty(exif_Info.Model))
                    {
                        command.Parameters["p_Model"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Model"].Value = exif_Info.Model;
                    }
                    if (!exif_Info.Resolution_X.HasValue)
                    {
                        command.Parameters["p_Resolution_X"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Resolution_X"].Value = exif_Info.Resolution_X;
                    }
                    if (!exif_Info.Resolution_Y.HasValue)
                    {
                        command.Parameters["p_Resolution_Y"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Resolution_Y"].Value = exif_Info.Resolution_Y;
                    }
                    if (string.IsNullOrEmpty(exif_Info.Resolution_Unit))
                    {
                        command.Parameters["p_Resolution_Unit"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Resolution_Unit"].Value = exif_Info.Resolution_Unit;
                    }
                    if (!exif_Info.Orientation.HasValue)
                    {
                        command.Parameters["p_Orientation"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Orientation"].Value = exif_Info.Orientation;
                    }
                    if (!exif_Info.DDL_Recorded.HasValue)
                    {
                        command.Parameters["p_DDL_Recorded"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_DDL_Recorded"].Value = exif_Info.DDL_Recorded;
                    }
                    if (!exif_Info.Exposure_Time.HasValue)
                    {
                        command.Parameters["p_Exposure_Time"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Exposure_Time"].Value = exif_Info.Exposure_Time;
                    }
                    if (!exif_Info.Exposure_Programm.HasValue)
                    {
                        command.Parameters["p_Exposure_Programm"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Exposure_Programm"].Value = exif_Info.Exposure_Programm;
                    }
                    if (!exif_Info.Exposure_Mode.HasValue)
                    {
                        command.Parameters["p_Exposure_Mode"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Exposure_Mode"].Value = exif_Info.Exposure_Mode;
                    }
                    if (!exif_Info.F_Number.HasValue)
                    {
                        command.Parameters["p_F_Number"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_F_Number"].Value = exif_Info.F_Number;
                    }
                    if (!exif_Info.ISO_Sensitivity.HasValue)
                    {
                        command.Parameters["p_ISO"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_ISO"].Value = exif_Info.ISO_Sensitivity;
                    }
                    if (!exif_Info.Shutter_Speed.HasValue)
                    {
                        command.Parameters["p_Shutter_Speed"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Shutter_Speed"].Value = exif_Info.Shutter_Speed;
                    }
                    if (!exif_Info.Metering_Mode.HasValue)
                    {
                        command.Parameters["p_Metering_Mode"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Metering_Mode"].Value = exif_Info.Metering_Mode;
                    }
                    if (!exif_Info.Flash_Mode.HasValue)
                    {
                        command.Parameters["p_Flash_Mode"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Flash_Mode"].Value = exif_Info.Flash_Mode;
                    }
                    if (!exif_Info.Focal_Length.HasValue)
                    {
                        command.Parameters["p_Focal_Length"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Focal_Length"].Value = exif_Info.Focal_Length;
                    }
                    if (!exif_Info.Sensing_Mode.HasValue)
                    {
                        command.Parameters["p_Sensing_Mode"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Sensing_Mode"].Value = exif_Info.Sensing_Mode;
                    }
                    if (!exif_Info.White_Balance_Mode.HasValue)
                    {
                        command.Parameters["p_White_Balance_Mode"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_White_Balance_Mode"].Value = exif_Info.White_Balance_Mode;
                    }
                    if (!exif_Info.Sharpness.HasValue)
                    {
                        command.Parameters["p_Sharpness"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Sharpness"].Value = exif_Info.Sharpness;
                    }
                    if (!exif_Info.Latitude.HasValue)
                    {
                        command.Parameters["p_Latitude"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Latitude"].Value = exif_Info.Latitude;
                    }
                    if (!exif_Info.Longitute.HasValue)
                    {
                        command.Parameters["p_Longitude"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["p_Longitude"].Value = exif_Info.Longitute;
                    }

                    //Ausführen
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
