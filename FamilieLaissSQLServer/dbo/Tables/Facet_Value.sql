CREATE TABLE [dbo].[Facet_Value] (
    [ID]           BIGINT             NOT NULL,
    [ID_Group]     BIGINT             NOT NULL,
    [Name_German]  NVARCHAR (50)      NOT NULL,
    [Name_English] NVARCHAR (50)      NOT NULL,
    [DDL_Create]   DATETIMEOFFSET (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Facet_Value_Facet_Group] FOREIGN KEY ([ID_Group]) REFERENCES [dbo].[Facet_Group] ([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_Facet_Value_Name_German]
    ON [dbo].[Facet_Value]([ID_Group] ASC, [Name_German] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_Facet_Value_Name_English]
    ON [dbo].[Facet_Value]([ID_Group] ASC, [Name_English] ASC);

