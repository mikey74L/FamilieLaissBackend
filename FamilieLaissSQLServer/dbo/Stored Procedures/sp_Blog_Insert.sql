-- =============================================
-- Author:		Michael Laiß
-- Create date: 11.06.2017
-- Description:	Fügt einen neuen Blog-Eintrag hinzu (Wird vom EF aufgerufen)
-- =============================================
CREATE PROCEDURE [dbo].[sp_Blog_Insert] (@p_Title nvarchar(max), @p_Text nvarchar(max))
AS
BEGIN
    -- Deklaration
    DECLARE @v_ID            bigint;
	DECLARE @v_Create_Date   datetimeoffset;

	-- Ermitteln der ID für den Blog-Eintrag
    SELECT @v_ID = NEXT VALUE FOR SEQ_Blog; 

	-- Festlegen des Datums
	SET @v_Create_Date = SYSDATETIMEOFFSET();
   
   -- Einfügen des Blog-Eintrages
    INSERT INTO Blog (ID, Title, Text, DDL_Create)
              VALUES (@v_ID, @p_Title, @p_Text, @v_Create_Date);
    
	-- Werte für EF zurückliefern
    SELECT @v_ID AS ID_Out, @v_Create_Date as Create_Date_Out;
END