DELETE FROM [AccountDetail]
DELETE FROM [Account]
DELETE FROM [Role]
GO

INSERT [Account]([LoginId],[Password],[IsEnabled],[IsDeleted],[CreatedOn]) 
VALUES ('admin','21232f297a57a5a743894a0e4a801fc3',1,0, GETDATE());

INSERT [AccountDetail]([Account_Id],[Name],[Sex],[SavedOn]) 
VALUES (1,'管理员',1,GETDATE());

INSERT [Role]([Name],[HomePage],[Memo],[IsEnabled],[IsDeleted],[Ordinal],[CreatedOn]) 
VALUES ('系统管理员','','系统管理员',1,0,0, GETDATE());
GO

DECLARE @accountId BIGINT
SELECT @accountId = [Id] FROM [Account] WHERE [LoginId]='admin'

DECLARE @roleId BIGINT
SELECT @roleId = [Id] FROM [Role] WHERE [Name]='系统管理员'

INSERT [Account_Role]([Account_Id],[Role_Id]) VALUES (@accountId,@roleId)

DECLARE @permissionId nvarchar(255)

DECLARE cur CURSOR FOR
	SELECT [Id] FROM [Permission]
OPEN cur
FETCH NEXT From cur INTO @permissionId
WHILE(@@fetch_status=0)
BEGIN
	INSERT [Role_Permission]([Role_Id],[Permission_Id]) VALUES (@roleId,@permissionId)
FETCH NEXT From cur INTO @permissionId
END
CLOSE cur
DEALLOCATE cur
GO

