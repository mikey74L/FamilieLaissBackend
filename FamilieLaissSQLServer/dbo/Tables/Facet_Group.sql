CREATE TABLE [dbo].[Facet_Group] (
    [ID]               BIGINT             NOT NULL,
    [Type]             TINYINT            NOT NULL,
    [Name_German]      NVARCHAR (70)      NOT NULL,
    [Name_English]     NVARCHAR (70)      NOT NULL,
    [Can_Delete]       BIT                NOT NULL,
    [Facet_Value_Type] TINYINT            NOT NULL,
    [DDL_Create]       DATETIMEOFFSET (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_Facet_Group_Name_German]
    ON [dbo].[Facet_Group]([Type] ASC, [Name_German] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_Facet_Group_Name_English]
    ON [dbo].[Facet_Group]([Type] ASC, [Name_English] ASC);

