DELIMITER //

CREATE PROCEDURE GenMenuPermission() 
BEGIN 

IF EXISTS (SELECT 1 FROM `Menu` WHERE `Id` = 'M0201') 
THEN 
UPDATE `Menu` SET `ParentId` = 'M02', `Name`='帐户管理', `Url`='/SystemSettings/Account/Index', `Category`='M', `Level`='2' WHERE `Id` = 'M0201'; 
ELSE 
INSERT `Menu` (`Id`,`ParentId`,`Name`,`Url`,`Category`,`Level`) VALUES ('M0201','M02','帐户管理','/SystemSettings/Account/Index','M','2'); 
END IF; 

IF EXISTS (SELECT 1 FROM `Menu` WHERE `Id` = 'M0202') 
THEN 
UPDATE `Menu` SET `ParentId` = 'M02', `Name`='角色管理', `Url`='/SystemSettings/Role/Index', `Category`='M', `Level`='2' WHERE `Id` = 'M0202'; 
ELSE 
INSERT `Menu` (`Id`,`ParentId`,`Name`,`Url`,`Category`,`Level`) VALUES ('M0202','M02','角色管理','/SystemSettings/Role/Index','M','2'); 
END IF; 

IF EXISTS (SELECT 1 FROM `Menu` WHERE `Id` = 'M02') 
THEN 
UPDATE `Menu` SET `ParentId` = '', `Name`='系统设置', `Url`='', `Category`='M', `Level`='1' WHERE `Id` = 'M02'; 
ELSE 
INSERT `Menu` (`Id`,`ParentId`,`Name`,`Url`,`Category`,`Level`) VALUES ('M02','','系统设置','','M','1'); 
END IF; 

IF EXISTS (SELECT 1 FROM `Menu` WHERE `Id` = 'M01') 
THEN 
UPDATE `Menu` SET `ParentId` = '', `Name`='首页', `Url`='/Home/Index', `Category`='M', `Level`='1' WHERE `Id` = 'M01'; 
ELSE 
INSERT `Menu` (`Id`,`ParentId`,`Name`,`Url`,`Category`,`Level`) VALUES ('M01','','首页','/Home/Index','M','1'); 
END IF; 

DELETE FROM `Menu` WHERE `Id` NOT IN ('M0201','M0202','M02','M01'); 



IF EXISTS (SELECT 1 FROM `Permission` WHERE `Id` = 'M0201P01') 
THEN 
UPDATE `Permission` SET `MenuId` = 'M0201', `Name`='页面访问', `Action`='' WHERE `Id` = 'M0201P01'; 
ELSE 
INSERT `Permission` (`Id`,`MenuId`,`Name`,`Action`) VALUES ('M0201P01','M0201','页面访问',''); 
END IF; 

IF EXISTS (SELECT 1 FROM `Permission` WHERE `Id` = 'M0201P0101') 
THEN 
UPDATE `Permission` SET `MenuId` = 'M0201', `Name`='查看用户信息', `Action`='/SystemSettings/Account/GetAccountInfo' WHERE `Id` = 'M0201P0101'; 
ELSE 
INSERT `Permission` (`Id`,`MenuId`,`Name`,`Action`) VALUES ('M0201P0101','M0201','查看用户信息','/SystemSettings/Account/GetAccountInfo'); 
END IF; 

IF EXISTS (SELECT 1 FROM `Permission` WHERE `Id` = 'M0201P010101') 
THEN 
UPDATE `Permission` SET `MenuId` = 'M0201', `Name`='编辑用户信息', `Action`='/SystemSettings/Account/EditAccountInfo' WHERE `Id` = 'M0201P010101'; 
ELSE 
INSERT `Permission` (`Id`,`MenuId`,`Name`,`Action`) VALUES ('M0201P010101','M0201','编辑用户信息','/SystemSettings/Account/EditAccountInfo'); 
END IF; 

IF EXISTS (SELECT 1 FROM `Permission` WHERE `Id` = 'M0202P01') 
THEN 
UPDATE `Permission` SET `MenuId` = 'M0202', `Name`='页面访问', `Action`='' WHERE `Id` = 'M0202P01'; 
ELSE 
INSERT `Permission` (`Id`,`MenuId`,`Name`,`Action`) VALUES ('M0202P01','M0202','页面访问',''); 
END IF; 

DELETE FROM `Permission` WHERE `Id` NOT IN ('M0201P01','M0201P0101','M0201P010101','M0202P01'); 



END;// 
DELIMITER ; 

CALL GenMenuPermission(); 

DROP PROCEDURE GenMenuPermission;