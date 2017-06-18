-- =============================================
-- Author:		Michael Laiß
-- Create date: 11.06.2017
-- Description:	Löscht eine bestendende Kategorie (Wird vom EF aufgerufen)
-- =============================================
CREATE PROCEDURE [dbo].[sp_Blog_Delete] (@p_ID bigint)
AS
BEGIN
	-- Löschen des Blogs
    DELETE FROM Blog WHERE ID = @p_ID;
END