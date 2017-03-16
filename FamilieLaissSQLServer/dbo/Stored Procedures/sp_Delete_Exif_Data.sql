-- =============================================
-- Author:		Michael Laiß
-- Create date: 15.03.2017
-- Description:	Löscht die Exif-Daten für ein Upload-Picture
-- =============================================
CREATE PROCEDURE sp_Delete_Exif_Data (@p_ID bigint)
AS
BEGIN
   -- Löschen der Daten
   DELETE FROM Upload_Picture_Item_Exif WHERE ID = @p_ID;
END