-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Aktualisiert ein Upload-Video
-- =============================================
CREATE PROCEDURE sp_Upload_Video_Update (@p_ID bigint)
AS
BEGIN
    -- Dummy Select da dieses niemals durch das EF aufgerufen wird
	SELECT 0 AS DUMMY;
END