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

