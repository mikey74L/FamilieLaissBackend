CREATE TABLE [dbo].[Blog] (
    [ID]         BIGINT             NOT NULL,
    [Title]      NVARCHAR (70)      NOT NULL,
    [Text]       NVARCHAR (MAX)     NOT NULL,
    [DDL_Create] DATETIMEOFFSET (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_Blog_Title]
    ON [dbo].[Blog]([Title] ASC);

