-- =============================================
-- Author:		Michael Laiss
-- Create date: 23.02.2017
-- Description:	Löscht eine Image-Property
-- =============================================
CREATE PROCEDURE [sp_UploadPictureImageProperty_Delete] (@p_ID bigint) 
AS
BEGIN
    -- Löschen des Datensatzes
	DELETE FROM Upload_Picture_Image_Property WHERE ID = @p_ID;
END