-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Löscht ein Upload-Video
-- =============================================
CREATE PROCEDURE sp_Upload_Video_Delete (@p_ID bigint)
AS
BEGIN
	-- Löschen des Items aus der Datenbank
    DELETE FROM Upload_Video_Item WHERE ID = @p_ID;
END