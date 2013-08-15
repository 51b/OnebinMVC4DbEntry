IF EXISTS (SELECT 1 FROM [Menu] WHERE [Id] = 'M0201') 
BEGIN 
UPDATE [Menu] SET [ParentId] = 'M02', [Name]='帐户管理', [Url]='/SystemSettings/Account/Index', [Category]='M', [Level]='2' WHERE [Id] = 'M0201' 
END 
ELSE BEGIN 
INSERT [Menu] ([Id],[ParentId],[Name],[Url],[Category],[Level]) VALUES ('M0201','M02','帐户管理','/SystemSettings/Account/Index','M','2') 
END 
GO 

IF EXISTS (SELECT 1 FROM [Menu] WHERE [Id] = 'M0202') 
BEGIN 
UPDATE [Menu] SET [ParentId] = 'M02', [Name]='角色管理', [Url]='/SystemSettings/Role/Index', [Category]='M', [Level]='2' WHERE [Id] = 'M0202' 
END 
ELSE BEGIN 
INSERT [Menu] ([Id],[ParentId],[Name],[Url],[Category],[Level]) VALUES ('M0202','M02','角色管理','/SystemSettings/Role/Index','M','2') 
END 
GO 

IF EXISTS (SELECT 1 FROM [Menu] WHERE [Id] = 'M02') 
BEGIN 
UPDATE [Menu] SET [ParentId] = '', [Name]='系统设置', [Url]='', [Category]='M', [Level]='1' WHERE [Id] = 'M02' 
END 
ELSE BEGIN 
INSERT [Menu] ([Id],[ParentId],[Name],[Url],[Category],[Level]) VALUES ('M02','','系统设置','','M','1') 
END 
GO 

IF EXISTS (SELECT 1 FROM [Menu] WHERE [Id] = 'M01') 
BEGIN 
UPDATE [Menu] SET [ParentId] = '', [Name]='首页', [Url]='/Home/Index', [Category]='M', [Level]='1' WHERE [Id] = 'M01' 
END 
ELSE BEGIN 
INSERT [Menu] ([Id],[ParentId],[Name],[Url],[Category],[Level]) VALUES ('M01','','首页','/Home/Index','M','1') 
END 
GO 

DELETE FROM [Menu] WHERE [Id] NOT IN ('M0201','M0202','M02','M01') 
GO 



IF EXISTS (SELECT 1 FROM [Permission] WHERE [Id] = 'M0201P01') 
BEGIN 
UPDATE [Permission] SET [MenuId] = 'M0201', [Name]='页面访问', [Action]='' WHERE [Id] = 'M0201P01' 
END 
ELSE BEGIN 
INSERT [Permission] ([Id],[MenuId],[Name],[Action]) VALUES ('M0201P01','M0201','页面访问','') 
END 
GO 

IF EXISTS (SELECT 1 FROM [Permission] WHERE [Id] = 'M0202P01') 
BEGIN 
UPDATE [Permission] SET [MenuId] = 'M0202', [Name]='页面访问', [Action]='' WHERE [Id] = 'M0202P01' 
END 
ELSE BEGIN 
INSERT [Permission] ([Id],[MenuId],[Name],[Action]) VALUES ('M0202P01','M0202','页面访问','') 
END 
GO 

DELETE FROM [Permission] WHERE [Id] NOT IN ('M0201P01','M0202P01') 
GO 



