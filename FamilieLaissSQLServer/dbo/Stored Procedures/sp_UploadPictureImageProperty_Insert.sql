-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Mit dieser Prozedur wird vom EF ein neuer
--              Eintrag fuer einen Image-Property gemacht
-- =============================================
CREATE PROCEDURE [sp_UploadPictureImageProperty_Insert] (@p_ID bigint, @p_Rotation int)
AS
BEGIN
   -- Daten in Datenbank einfuegen
   INSERT INTO Upload_Picture_Image_Property(ID, Rotate)
                                     VALUES (@p_ID, @p_Rotation);
END