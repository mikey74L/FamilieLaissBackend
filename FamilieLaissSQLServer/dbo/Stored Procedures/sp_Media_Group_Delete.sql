-- =============================================
-- Author:		Michael Laiß
-- Create date: 20.02.2011
-- Description:	Löscht ein Album (wird vom EF aufgerufen)
-- =============================================
CREATE PROCEDURE sp_Media_Group_Delete (@p_ID bigint)
AS
BEGIN
   -- Deklaration
	DECLARE @v_ID_Item bigint;
	DECLARE @Media_Item_IDs TABLE ( 
        ID BIGINT 
    ); 
	DECLARE v_Cursor_Media_Items CURSOR FOR
	    SELECT ID 
		  FROM @Media_Item_IDs; 

    -- Befüllen der Memory-Tabelle mit den IDs der Medien-Elemente
	INSERT INTO @Media_Item_IDs
	    SELECT ID
		  FROM Media_Item
		 WHERE Group_ID = @p_ID;

    -- Löschen aller Kategoriewerte über einen Cursor
	OPEN v_Cursor_Media_Items;
	FETCH NEXT INTO @v_ID_Item;
	WHILE @@FETCH_STATUS = 0
	BEGIN
	    -- Löschen des Datensatzes
		exec sp_Media_Item_Delete @v_ID_Item;

	    -- Nächsten Datensatz aus dem Cursor ermitteln
  	    FETCH NEXT INTO @v_ID_Item;
	END
	CLOSE v_Cursor_Media_Items;

   -- Löschen des Albums in der Datenbank
   DELETE Media_Group WHERE ID = @p_ID;
END