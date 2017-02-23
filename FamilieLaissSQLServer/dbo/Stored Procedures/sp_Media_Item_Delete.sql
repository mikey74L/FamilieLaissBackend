-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Löscht ein Medien-Element 
-- =============================================
CREATE PROCEDURE sp_Media_Item_Delete (@p_ID bigint)
AS
BEGIN
    -- Deklaration
	DECLARE @v_Upload_Picture_ID bigint;
	DECLARE @v_Upload_Video_ID   bigint;
	DECLARE @v_ID_Assignment     bigint;
	DECLARE @Facet_Value_Assignment_IDs TABLE ( 
        ID BIGINT 
    ); 
	DECLARE v_Cursor_Assignments CURSOR FOR
	    SELECT ID 
		  FROM @Facet_Value_Assignment_IDs; 

    -- Befüllen der Memory-Tabelle mit den IDs der Assignments
	INSERT INTO @Facet_Value_Assignment_IDs
	    SELECT ID
		  FROM Media_Item_Facet
		 WHERE Media_Item_ID = @p_ID;

    -- Löschen aller Assignments über einen Cursor
	OPEN v_Cursor_Assignments;
	FETCH NEXT INTO @v_ID_Assignment;
	WHILE @@FETCH_STATUS = 0
	BEGIN
	    -- Löschen des Datensatzes
		exec sp_MediaFacet_Delete @v_ID_Assignment;

	    -- Nächsten Datensatz aus dem Cursor ermitteln
  	    FETCH NEXT INTO @v_ID_Assignment;
	END
	CLOSE v_Cursor_Assignments;

	-- Ermitteln der IDs für das Upload-Picture bzw. das Upload_Video
	SELECT @v_Upload_Picture_ID = Upload_Picture_ID, @v_Upload_Video_ID = Upload_Video_ID FROM Media_Item WHERE ID = @p_ID; 

    -- Löschen des Upload-Pictures
	IF @v_Upload_Picture_ID IS NOT NULL
	BEGIN
	    exec sp_Upload_Picture_Delete @v_Upload_Picture_ID;
	END

	-- Löschen des Video-Items
	IF @v_Upload_Video_ID IS NOT NULL
	BEGIN
	    exec sp_Upload_Video_Delete @v_Upload_Video_ID;
	END

    -- Löschen des Medien-Elements aus der Datenbank
    DELETE FROM Media_Item WHERE ID = @p_ID;
END