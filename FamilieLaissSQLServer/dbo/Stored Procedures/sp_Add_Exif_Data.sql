-- =============================================
-- Author:		Michael Laiß
-- Create date: 15.03.2017
-- Description:	Fügt die Exif-Daten für ein Upload-Item hinzu
-- =============================================
CREATE PROCEDURE sp_Add_Exif_Data (@p_ID bigint, @p_Make nvarchar(max), @p_Model nvarchar(max),
                                   @p_Resolution_X float, @p_Resolution_Y float, @p_Resolution_Unit nvarchar(max),
								   @p_Orientation smallint, @p_DDL_Recorded datetimeoffset,
								   @p_Exposure_Time float, @p_Exposure_Programm smallint, @p_Exposure_Mode smallint,
								   @p_F_Number float, @p_ISO int, @p_Shutter_Speed float,
								   @p_Metering_Mode smallint, @p_Flash_Mode smallint, @p_Focal_Length float,
								   @p_Sensing_Mode smallint, @p_White_Balance_Mode smallint, 
								   @p_Sharpness smallint, @p_Latitude float, @p_Longitude float)
AS
BEGIN
   -- Deklaration
   DECLARE @v_Geography GEOGRAPHY;

   -- Ermitteln der GPS-Position als Geography-Wert wenn vorhanden
   IF @p_Latitude IS NOT NULL AND @p_Longitude IS NOT NULL
   BEGIN
      SET @v_Geography = geography::Point(@p_Latitude, @p_Longitude, 4326);
   END
   ELSE
   BEGIN
      SET @v_Geography = null;
   END

   -- Einfügen der Daten in die Tabelle
   INSERT INTO Upload_Picture_Item_Exif (ID, Make, Model, Resolution_X, Resolution_Y, Resolution_Unit,
                                         Orientation, DDL_Recorded, Exposure_Time, Exposure_Programm,
										 Exposure_Mode, F_Number, ISO_Sensitivity, Shutter_Speed, Metering_Mode,
										 Flash_Mode, Focal_Length, Sensing_Mode, White_Balance_Mode,
										 Sharpness, GPS_Location)
								 VALUES (@p_ID, @p_Make, @p_Model, @p_Resolution_X, @p_Resolution_Y, 
								         @p_Resolution_Unit, @p_Orientation, @p_DDL_Recorded,
								         @p_Exposure_Time, @p_Exposure_Programm, @p_Exposure_Mode,
								         @p_F_Number, @p_ISO, @p_Shutter_Speed, @p_Metering_Mode, 
										 @p_Flash_Mode, @p_Focal_Length, @p_Sensing_Mode, 
										 @p_White_Balance_Mode, @p_Sharpness, @v_Geography);
END