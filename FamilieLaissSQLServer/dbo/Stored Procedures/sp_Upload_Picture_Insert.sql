-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Fügt ein neues Upload-Picture in die Datenbank ein
--
-- 09.03.2017  Michael Laiss  Die ID wird jetzt mit übergeben, da das Erzeugen neuer
--                            Einträge jetzt der Azure Web-Job übernimmt und dieser über
--                            die Azure Queue entsprechend die ID schon hat, weil diese
--                            beim Upload des Photos schon über die entsprechende Sequence ermittelt wird
-- =============================================
CREATE PROCEDURE sp_Upload_Picture_Insert (@p_ID bigint, @p_Name nvarchar(max), @p_Height int, @p_Width int)
AS
BEGIN
    -- Deklaration
	DECLARE @v_Upload_Date datetimeoffset(7);

	-- Setzen des Upload-Datums
	SET @v_Upload_Date = SYSDATETIMEOFFSET();

	-- Einfügen des Upload-Items
    INSERT INTO Upload_Picture_Item (ID, Name_Original, Upload_Date, Height_Original, Width_Original, Status)
                             VALUES (@p_ID, @p_Name, @v_Upload_Date, @p_Height, @p_Width, 0);
    
	-- Ergebnis für das EF
	SELECT @v_Upload_Date AS Upload_Date_Out, 0 as Status_Out;
END