-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Mit dieser Prozedur wird ein neues MediaFacet in der Datenbank erstellt.
--              Die Prozedur wird vom EF aufgerufen
-- =============================================
CREATE PROCEDURE [dbo].[sp_MediaFacet_Insert](@p_Media_ID bigint, @p_FacetValue bigint) 
AS
BEGIN
   -- Deklaration
   DECLARE @v_ID  bigint;

   -- Ermitteln der ID aus der Sequence
   SELECT @v_ID = NEXT VALUE FOR SEQ_Media_Item_Facet; 

   -- Einfügen des Datensatzes
   INSERT INTO Media_Item_Facet (ID, Media_Item_ID, Facet_Value_ID)
	                     VALUES (@v_ID, @p_Media_ID, @p_FacetValue);
	                           
   -- Result-Set ermitteln
   SELECT @v_ID AS ID_OUT;
END