-- =============================================
-- Author:		Michael Laiß
-- Create date: 20.02.2017
-- Description:	Fügt eine neue Kategorie hinzu (Wird vom EF aufgerufen)
-- =============================================
CREATE PROCEDURE sp_Facet_Group_Insert (@p_Type tinyint, @p_Name_German nvarchar(max), @p_Name_English nvarchar(max))
AS
BEGIN
    -- Deklaration
    DECLARE @v_ID            bigint;
	DECLARE @v_Create_Date   datetimeoffset;

	-- Ermitteln der ID der eingefügten Kategorie
    SELECT @v_ID = NEXT VALUE FOR SEQ_Facet_Group; 

	-- Festlegen des Datums
	SET @v_Create_Date = SYSDATETIMEOFFSET();

    -- Einfügen der Kategorie
    INSERT INTO Facet_Group (ID, Type, Name_German, Name_English, Can_Delete, Facet_Value_Type, DDL_Create)
                     VALUES (@v_ID, @p_Type, @p_Name_German, @p_Name_English, 1, 0, @v_Create_Date);
    
	-- Werte für EF zurückliefern
    SELECT @v_ID AS ID_Out, 1 AS Can_Delete_Out, 0 Facet_Value_Type_Out, @v_Create_Date as Create_Date_Out;
END