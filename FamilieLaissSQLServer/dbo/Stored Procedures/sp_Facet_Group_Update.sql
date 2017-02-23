-- =============================================
-- Author:		Michael Laiß
-- Create date: 20.02.2017
-- Description:	Ändern der Daten einer Kategorie (Wird vom EF aufgerufen)
-- =============================================
CREATE PROCEDURE sp_Facet_Group_Update (@p_ID bigint, @p_Name_German nvarchar(max), @p_Name_English nvarchar(max))
AS
BEGIN
    -- Aktualisieren der Kategorie
    UPDATE Facet_Group SET Name_German = @p_Name_German, 
	                       Name_English = @p_Name_English
                     WHERE ID = @p_ID;
END