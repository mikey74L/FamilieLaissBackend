-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Löscht einen Kategorie-Wert
-- =============================================
CREATE PROCEDURE dbo.sp_Facet_Value_Delete (@p_ID bigint)
AS
BEGIN
    -- Deklaration
	DECLARE @v_ID_Assignment bigint;
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
		 WHERE Facet_Value_ID = @p_ID;

    -- Löschen aller Assignments über einen Cursor
	OPEN v_Cursor_Assignments;
	FETCH NEXT FROM v_Cursor_Assignments INTO @v_ID_Assignment;
	WHILE @@FETCH_STATUS = 0
	BEGIN
	    -- Löschen des Datensatzes
		exec sp_MediaFacet_Delete @v_ID_Assignment;

	    -- Nächsten Datensatz aus dem Cursor ermitteln
  	    FETCH NEXT FROM v_Cursor_Assignments INTO @v_ID_Assignment;
	END
	CLOSE v_Cursor_Assignments;

   -- Löschen des Kategorie-Wertes
   DELETE FROM Facet_Value WHERE ID = @p_ID;
END