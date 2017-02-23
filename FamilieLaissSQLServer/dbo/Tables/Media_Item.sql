CREATE TABLE [dbo].[Media_Item] (
    [ID]                  BIGINT             NOT NULL,
    [Group_ID]            BIGINT             NOT NULL,
    [Type]                TINYINT            NOT NULL,
    [Name_German]         NVARCHAR (200)     NOT NULL,
    [Name_English]        NVARCHAR (200)     NOT NULL,
    [Description_German]  NVARCHAR (2000)    NULL,
    [Description_English] NVARCHAR (2000)    NULL,
    [Only_Family]         BIT                NOT NULL,
    [Create_Date]         DATETIMEOFFSET (7) NOT NULL,
    [Upload_Picture_ID]   BIGINT             NULL,
    [Upload_Video_ID]     BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Media_Item_Group] FOREIGN KEY ([Group_ID]) REFERENCES [dbo].[Media_Group] ([ID]),
    CONSTRAINT [FK_Media_Item_Upload_Picture] FOREIGN KEY ([Upload_Picture_ID]) REFERENCES [dbo].[Upload_Picture_Item] ([ID]),
    CONSTRAINT [FK_Media_Item_Upload_Video] FOREIGN KEY ([Upload_Video_ID]) REFERENCES [dbo].[Upload_Video_Item] ([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_Media_Item_Name_German]
    ON [dbo].[Media_Item]([Group_ID] ASC, [Name_German] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_Media_Item_Name_English]
    ON [dbo].[Media_Item]([Group_ID] ASC, [Name_English] ASC);

