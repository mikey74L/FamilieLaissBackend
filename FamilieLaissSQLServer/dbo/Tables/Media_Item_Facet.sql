CREATE TABLE [dbo].[Media_Item_Facet] (
    [ID]             BIGINT NOT NULL,
    [Media_Item_ID]  BIGINT NOT NULL,
    [Facet_Value_ID] BIGINT NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Media_Item_Facet_Facet_Value] FOREIGN KEY ([Facet_Value_ID]) REFERENCES [dbo].[Facet_Value] ([ID]),
    CONSTRAINT [FK_Media_Item_Facet_Media_Item] FOREIGN KEY ([Media_Item_ID]) REFERENCES [dbo].[Media_Item] ([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_Media_Item_Facet_Unique]
    ON [dbo].[Media_Item_Facet]([Media_Item_ID] ASC, [Facet_Value_ID] ASC);

