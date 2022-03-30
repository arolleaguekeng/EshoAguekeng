CREATE TABLE [dbo].[Category] (
    [Id]     INT          NOT NULL,
    [Name]   VARCHAR (50) NOT NULL,
    [UserId] INT          NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Category_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [IX_Category] UNIQUE NONCLUSTERED ([Name] ASC)
);



