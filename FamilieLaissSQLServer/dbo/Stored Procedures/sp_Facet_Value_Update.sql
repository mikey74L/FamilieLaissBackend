-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Aktualisiert einen Kategoriewert 
-- =============================================
CREATE PROCEDURE sp_Facet_Value_Update (@p_ID bigint, @p_Name_German nvarchar(max), @p_Name_English nvarchar(max))
AS
BEGIN
   -- Aktualisieren des Kategorie-Wertes
   UPDATE Facet_Value SET Name_German = @p_Name_German, 
                          Name_English = @p_Name_English
                   WHERE  ID = @p_ID;
END