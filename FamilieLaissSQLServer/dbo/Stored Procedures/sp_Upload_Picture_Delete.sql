-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Löscht ein Upload-Picture
-- =============================================
CREATE PROCEDURE sp_Upload_Picture_Delete (@p_ID bigint)
AS
BEGIN
    -- Löschen der Image-Property
	exec sp_UploadPictureImageProperty_Delete @p_ID;

	-- Löschen der Exif-Daten
	exec sp_Delete_Exif_Data @p_ID;

    -- Löschen des Items aus der Datenbank
    DELETE FROM Upload_Picture_Item WHERE ID = @p_ID;
END