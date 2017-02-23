-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Mit dieser Prozedur wird ein bestehender MediaFacet-Eintrag aus der Datenbank gelöscht.
--              Diese Prozedur wird vom EF aufgerufen.
-- =============================================
CREATE PROCEDURE [dbo].[sp_MediaFacet_Delete](@p_ID bigint)
AS
BEGIN
   -- Löschen des Datensatzes
   DELETE FROM Media_Item_Facet WHERE ID = @p_ID;
END