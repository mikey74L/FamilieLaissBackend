-- =============================================
-- Author:		Michael Laiß
-- Create date: 23.02.2017
-- Description:	Aktualisiert ein Medien-Element 
-- =============================================
CREATE PROCEDURE sp_Media_Item_Update (@p_ID bigint, 
                                       @p_Name_German nvarchar(max), 
                                       @p_Name_English nvarchar(max), 
									   @p_Description_German nvarchar(max), 
									   @p_Description_English nvarchar(max), 
									   @p_Only_Family bit)
AS
BEGIN
    -- Aktualisieren des Medien-Elements
    UPDATE Media_Item SET Name_German = @p_Name_German, 
	                      Name_English = @p_Name_English,
	                      Description_German = @p_Description_German,
						  Description_English = @p_Description_English,
						  Only_Family = @p_Only_Family
                    WHERE ID = @p_ID;
END