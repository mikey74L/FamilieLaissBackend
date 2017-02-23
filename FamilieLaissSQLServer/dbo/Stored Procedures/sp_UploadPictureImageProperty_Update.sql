-- =============================================
-- Author:		Michael Laiss
-- Create date: 23.02.2017
-- Description:	Mit dieser Prozedur wird eine Image-Property vom
--              EF aktualisiert
-- =============================================
CREATE PROCEDURE [sp_UploadPictureImageProperty_Update] (@p_ID bigint, @p_Rotation int)
AS
BEGIN
   -- Aktualisieren des Datenbankrecords
   UPDATE Upload_Picture_Image_Property SET Rotate = @p_Rotation WHERE ID = @p_ID;
END