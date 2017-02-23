CREATE TABLE [dbo].[Upload_Video_Item] (
    [ID]              BIGINT             NOT NULL,
    [Original_Name]   NVARCHAR (255)     NOT NULL,
    [Status]          TINYINT            NOT NULL,
    [Upload_Date]     DATETIMEOFFSET (7) NOT NULL,
    [Original_Height] INT                NOT NULL,
    [Original_Width]  INT                NOT NULL,
    [Duration_Hour]   INT                NOT NULL,
    [Duration_Minute] INT                NOT NULL,
    [Duration_Second] INT                NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

