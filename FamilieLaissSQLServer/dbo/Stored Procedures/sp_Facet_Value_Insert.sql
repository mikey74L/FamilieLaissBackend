-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Erstellt einen Kategorie-Wert 
-- =============================================
CREATE PROCEDURE sp_Facet_Value_Insert (@p_ID_Group bigint, @p_Name_German nvarchar(max), @p_Name_English nvarchar(max))
AS
BEGIN
    -- Deklaration
    DECLARE @v_ID            bigint;
	DECLARE @v_Create_Date   datetimeoffset;

	-- Ermitteln der ID aus der Sequence
    SELECT @v_ID = NEXT VALUE FOR SEQ_Facet_Value; 

	-- Festlegen des Datums
	SET @v_Create_Date = SYSDATETIMEOFFSET();

    -- Einfuegen des Kategorie-Wertes
	INSERT INTO Facet_Value (ID, ID_Group, Name_German, Name_English, DDL_Create)
                     VALUES (@v_ID, @p_ID_Group, @p_Name_German, @p_Name_English, @v_Create_Date)
    
	-- Rückgabewert für das EF
    SELECT @v_ID AS ID_Out, @v_Create_Date as Create_Date_Out;
END