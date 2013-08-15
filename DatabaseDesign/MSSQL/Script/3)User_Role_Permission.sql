DELETE FROM [Account]
DELETE FROM [Role]
GO

INSERT [Account]([LoginId],[Password],[Name],[Sex],[Email],[IsEnabled],[IsDeleted],[CreatedOn]) 
VALUES ('admin','21232f297a57a5a743894a0e4a801fc3', 'Onebin',1,'onebin.net@gmail.com',1,0, GETDATE());


INSERT [Role]([Name],[HomePage],[Memo],[IsEnabled],[IsDeleted],[Ordinal],[CreatedOn]) 
VALUES ('系统管理员','','系统管理员',1,0,0, GETDATE());
GO

DECLARE @accountId BIGINT
SELECT TOP 1 @accountId = [Id] FROM [Account] WHERE [LoginId]='admin'

DECLARE @roleId BIGINT
SELECT TOP 1 @roleId = [Id] FROM [Role] WHERE [Name]='系统管理员'

INSERT [Account_Role]([Account_Id],[Role_Id]) VALUES (@accountId,@roleId)

INSERT [Role_Permission]([Role_Id], [Permission_Id]) SELECT @roleId, Id FROM [Permission]