-- =============================================
-- Author:		Michael Laiß
-- Create date: 20.02.2017
-- Description:	Löscht eine Kategorie (Wird vom EF aufgerufen)
-- =============================================
CREATE PROCEDURE sp_Facet_Group_Delete (@p_ID bigint)
AS
BEGIN
    -- Deklaration
	DECLARE @v_ID_Value bigint;
	DECLARE @Facet_Value_IDs TABLE ( 
        ID BIGINT 
    ); 
	DECLARE v_Cursor_Values CURSOR FOR
	    SELECT ID 
		  FROM @Facet_Value_IDs; 

    -- Befüllen der Memory-Tabelle mit den IDs der Kategorie-Werte
	INSERT INTO @Facet_Value_IDs
	    SELECT ID
		  FROM Facet_Value
		 WHERE ID_Group = @p_ID;

    -- Löschen aller Kategoriewerte über einen Cursor
	OPEN v_Cursor_Values;
	FETCH NEXT FROM v_Cursor_Values INTO @v_ID_Value;
	WHILE @@FETCH_STATUS = 0
	BEGIN
	    -- Löschen des Datensatzes
		exec sp_Facet_Value_Delete @v_ID_Value;

	    -- Nächsten Datensatz aus dem Cursor ermitteln
 	    FETCH NEXT FROM v_Cursor_Values INTO @v_ID_Value;
	END
	CLOSE v_Cursor_Values;

	-- Löschen der Gruppe
    DELETE FROM Facet_Group WHERE ID = @p_ID;
END