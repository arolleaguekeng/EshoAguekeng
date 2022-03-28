CREATE TABLE [dbo].[User] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR (50) NOT NULL,
    [Fullname] VARCHAR (50) NOT NULL,
    [Role]     VARCHAR (50) NOT NULL,
    [Password] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_User] UNIQUE NONCLUSTERED ([Id] ASC)
);



