-- =============================================
-- Author:		Michael Laiß
-- Create date: 20.02.2017
-- Description:	Fügt ein neues Album hinzu (wird vom EF aufgerufen)
-- =============================================
CREATE PROCEDURE sp_Media_Group_Insert (@p_Typ tinyint, 
                                        @p_Name_German nvarchar(max), 
										@p_Name_English nvarchar(max), 
										@p_Description_German nvarchar(max),
										@p_Description_English nvarchar(max))
AS
BEGIN
   -- Deklarationen
   DECLARE @v_ID bigint;
   DECLARE @v_Create_Date datetimeoffset(7);

   -- Festlegen des Datums
   SET @v_Create_Date = SYSDATETIMEOFFSET();

   -- Ermitteln der ID aus der Sequence
   SELECT @v_ID = NEXT VALUE FOR SEQ_Media_Group; 

   -- Einfügen der Daten
   INSERT Media_Group(ID, Type, Name_German, Name_English, Description_German, Description_English, DDL_Create)
              VALUES (@v_ID, @p_Typ, @p_Name_German, @p_Name_English, @p_Description_German, @p_Description_English, @v_Create_Date);
    
   -- Ergebnis für EF zurückliefern
   SELECT @v_ID AS ID_Out, @v_Create_Date AS Create_Date_Out;
END