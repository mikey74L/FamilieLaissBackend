-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Fügt ein neues Medien-Element in die Datenbank ein
-- =============================================
CREATE PROCEDURE sp_Media_Item_Insert (@p_ID_Group bigint, 
                                       @p_Typ int, 
									   @p_Name_German nvarchar(max), 
									   @p_Name_English nvarchar(max), 
									   @p_Description_German nvarchar(max), 
									   @p_Description_English nvarchar(max), 
									   @p_Only_Family bit,
                                       @p_Upload_Picture bigint, 
									   @p_Upload_Video bigint)
AS
BEGIN
    -- Deklaration
	DECLARE @v_ID bigint;
	DECLARE @v_Create_Date datetimeoffset(7);

	-- Ermitteln der ID aus der Sequence
    SELECT @v_ID = NEXT VALUE FOR SEQ_Media_Item; 

    -- Festlegen des Datums
    SET @v_Create_Date = SYSDATETIMEOFFSET();

	-- Einfuegen des neuen Medien-Elements
    INSERT Media_Item (ID, Group_ID, Type, Create_Date, Name_German, Name_English, Description_German, Description_English, Only_Family, Upload_Picture_ID, Upload_Video_ID)
               VALUES (@v_ID, @p_ID_Group, @p_Typ, @v_Create_Date, @p_Name_German, @p_Name_English, @p_Description_German, @p_Description_English, @p_Only_Family, @p_Upload_Picture, @p_Upload_Video);
    
	-- Aktualisieren des Status für das Upload-Photo wenn es sich um ein
	-- Photo handelt
	IF @p_Upload_Picture IS NOT NULL
	BEGIN
	   UPDATE Upload_Picture_Item SET Status = 1 WHERE ID = @p_Upload_Picture;
    END

	-- Aktualisieren des Status für das Upload-Video wenn es sich um ein Video
	-- handelt
	IF @p_Upload_Video IS NOT NULL
	BEGIN
	   UPDATE Upload_Video_Item SET Status = 7 WHERE ID = @p_Upload_Video;
	END

	-- Rückgabewerte für das EF
	SELECT @v_ID AS ID_Out, @v_Create_Date AS Create_Date_Out;
END