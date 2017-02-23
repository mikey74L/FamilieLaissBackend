-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Aktualisiert ein Upload-Picture
-- =============================================
CREATE PROCEDURE sp_Upload_Picture_Update (@p_ID bigint)
AS
BEGIN
    -- Dummy-Select da niemals ein Update auf ein Upload-Picture stattfindet
	SELECT 1 AS Dummy_Out;
END