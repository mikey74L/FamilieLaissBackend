CREATE TABLE [dbo].[Upload_Picture_Item] (
    [ID]              BIGINT             NOT NULL,
    [Name_Original]   NVARCHAR (255)     NOT NULL,
    [Upload_Date]     DATETIMEOFFSET (7) NOT NULL,
    [Height_Original] INT                NOT NULL,
    [Width_Original]  INT                NOT NULL,
    [Status]          TINYINT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

