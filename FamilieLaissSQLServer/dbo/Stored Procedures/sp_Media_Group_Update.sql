-- =============================================
-- Author:		Michael Laiß
-- Create date: 20.02.2017
-- Description:	Aktualisiert die Daten eines Albums (wird vom EF aufgerufen)
-- =============================================
CREATE PROCEDURE sp_Media_Group_Update (@p_ID bigint, 
                                        @p_Name_German nvarchar(max), 
                                        @p_Name_English nvarchar(max), 
										@p_Description_German nvarchar(max),
										@p_Description_English nvarchar(max))
AS
BEGIN
   -- Aktualisieren der Daten
   UPDATE Media_Group SET Name_German = @p_Name_German,
                          Name_English = @p_Name_English,
                          Description_German = @p_Description_German,
						  Description_English = @p_Description_English
                    WHERE ID = @p_ID;
END