CREATE TABLE [dbo].[Product] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Code]        NVARCHAR (20)   NOT NULL,
    [Name]        NVARCHAR (100)  NOT NULL,
    [Description] NVARCHAR (MAX)  NOT NULL,
    [Price]       REAL            NOT NULL,
    [Photo]       VARBINARY (MAX) NULL,
    [CategoryId]  INT             NOT NULL,
    [UserId]      INT             NOT NULL,
    [CreatedAt]   DATE            NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Product_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]),
    CONSTRAINT [FK_Product_Product] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [IX_Product] UNIQUE NONCLUSTERED ([Code] ASC)
);












GO
CREATE NONCLUSTERED INDEX [IX_Product_1]
    ON [dbo].[Product]([Id] ASC);

