CREATE TABLE [dbo].[Upload_Picture_Image_Property] (
    [ID]     BIGINT  NOT NULL,
    [Rotate] TINYINT NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Upload_Picture_Image_Property] FOREIGN KEY ([ID]) REFERENCES [dbo].[Upload_Picture_Item] ([ID])
);

