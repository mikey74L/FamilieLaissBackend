-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Fügt ein neues Upload-Video in die Datenbank ein
-- =============================================
CREATE PROCEDURE sp_Upload_Video_Insert (@p_Name nvarchar(max))
AS
BEGIN
    -- Deklaration
	DECLARE @v_ID          bigint;
	DECLARE @v_Upload_Date datetimeoffset(7);

	-- Setzen des Datums
	SET @v_Upload_Date = SYSDATETIMEOFFSET();

    -- Ermitteln der ID aus der Sequence
    SELECT @v_ID = NEXT VALUE FOR SEQ_Upload_Video; 

    -- Einfügen der Daten in die Datenbank
    INSERT INTO Upload_Video_Item (ID, Original_Name, Upload_Date, Status, Original_Width, Original_Height, Duration_Hour, Duration_Minute, Duration_Second)
                           VALUES (@v_ID, @p_Name, @v_Upload_Date, 0, 0, 0, 0, 0, 0);

	-- Ergebnis für das EF
	SELECT @v_ID AS ID_Out, @v_Upload_Date AS Upload_Date_Out, 0 AS Status_Out, 0 AS Width_Out, 0 AS Height_Out, 0 AS Hour_Out, 0 AS Minute_Out, 0 AS Second_Out;    
END