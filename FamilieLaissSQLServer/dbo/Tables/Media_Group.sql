CREATE TABLE [dbo].[Media_Group] (
    [ID]                  BIGINT             NOT NULL,
    [Type]                TINYINT            NOT NULL,
    [Name_German]         NVARCHAR (70)      NOT NULL,
    [Name_English]        NVARCHAR (70)      NOT NULL,
    [Description_German]  NVARCHAR (300)     NULL,
    [Description_English] NVARCHAR (300)     NULL,
    [DDL_Create]          DATETIMEOFFSET (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_Media_Group_Name_German]
    ON [dbo].[Media_Group]([Type] ASC, [Name_German] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_Media_Group_Name_English]
    ON [dbo].[Media_Group]([Type] ASC, [Name_English] ASC);

