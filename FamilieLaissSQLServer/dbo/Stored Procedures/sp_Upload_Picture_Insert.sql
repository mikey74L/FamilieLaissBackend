-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Fügt ein neues Upload-Picture in die Datenbank ein
-- =============================================
CREATE PROCEDURE sp_Upload_Picture_Insert (@p_Name nvarchar(max), @p_Height int, @p_Width int)
AS
BEGIN
    -- Deklaration
	DECLARE @v_ID          bigint;
	DECLARE @v_Upload_Date datetimeoffset(7);

	-- Setzen des Upload-Datums
	SET @v_Upload_Date = SYSDATETIMEOFFSET();

    -- Ermitteln der ID aus der Sequence
    SELECT @v_ID = NEXT VALUE FOR SEQ_Upload_Picture; 

	-- Einfügen des Upload-Items
    INSERT INTO Upload_Picture_Item (ID, Name_Original, Upload_Date, Height_Original, Width_Original, Status)
                             VALUES (@v_ID, @p_Name, @v_Upload_Date, @p_Height, @p_Width, 0);
    
	-- Ergebnis für das EF
	SELECT @v_ID AS ID_Out, @v_Upload_Date AS Upload_Date_Out, 0 as Status_Out;
END