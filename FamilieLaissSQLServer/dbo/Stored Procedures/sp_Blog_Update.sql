-- =============================================
-- Author:		Michael Laiß
-- Create date: 11.06.2017
-- Description:	Aktualisiert einen bestehenden Blog-Eintrag (Wird vom EF aufgerufen)
-- =============================================
CREATE PROCEDURE [dbo].[sp_Blog_Update] (@p_ID bigint, @p_Title nvarchar(max), @p_Text nvarchar(max))
AS
BEGIN
    -- Aktualisieren der Kategorie
    UPDATE Blog SET Title = @p_Title, 
	                Text = @p_Text
              WHERE ID = @p_ID;
END