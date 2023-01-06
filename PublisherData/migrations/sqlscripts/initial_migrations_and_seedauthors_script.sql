IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Authors] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Books] (
    [BookId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [PublishDate] datetime2 NOT NULL,
    [BasePrice] decimal(18,2) NOT NULL,
    [AuthorId] int NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([BookId]),
    CONSTRAINT [FK_Books_Authors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Authors] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Books_AuthorId] ON [Books] ([AuthorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230105142602_initial_migration', N'6.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Authors]'))
    SET IDENTITY_INSERT [Authors] ON;
INSERT INTO [Authors] ([Id], [FirstName], [LastName])
VALUES (1, N'Rodrigo', N'Cortés'),
(2, N'Brandon', N'Sanderson'),
(3, N'Javier', N'Negrete'),
(4, N'Joe', N'Abercrombie'),
(5, N'Stephen', N'King'),
(6, N'Arturo', N'Pérez Reverte'),
(7, N'BB', N'King'),
(8, N'JRR', N'Tolkien'),
(9, N'Christopher', N'Tolkien'),
(10, N'Dean', N'Koontz'),
(11, N'Juan', N'Gómez Jurado');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Authors]'))
    SET IDENTITY_INSERT [Authors] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230105181958_seedauthors', N'6.0.1');
GO

COMMIT;
GO

