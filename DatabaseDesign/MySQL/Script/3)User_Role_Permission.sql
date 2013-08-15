SET SQL_SAFE_UPDATES=0;

DELETE FROM `Role_Permission`;
DELETE FROM `Account`;
DELETE FROM `Role`;

SET SQL_SAFE_UPDATES=1;

INSERT `Account`(`LoginId`,`Password`,`Name`,`Sex`,`Email`,`IsEnabled`,`IsDeleted`,`CreatedOn`) 
VALUES ('admin','21232f297a57a5a743894a0e4a801fc3', 'Onebin',1,'onebin.net@gmail.com',1,0, NOW());

INSERT `Role`(`Name`,`HomePage`,`Memo`,`IsEnabled`,`IsDeleted`,`Ordinal`,`CreatedOn`) 
VALUES ('系统管理员','','系统管理员',1,0,0, NOW());

select * from Role;


﻿DELIMITER //

CREATE PROCEDURE GrantPermission() 
BEGIN 

SELECT @AccountId := Id FROM `Account` WHERE `LoginId` = 'admin';
SELECT @RoleId := Id FROM `Role` WHERE `Name` = '系统管理员';
INSERT INTO `Account_Role`(`Account_Id`,`Role_Id`) VALUES (@AccountId, @RoleId);
INSERT INTO `Role_Permission`(`Role_Id`, `Permission_Id`) SELECT @RoleId, Id FROM `Permission`;

END;// 
DELIMITER ; 

CALL GrantPermission(); 

DROP PROCEDURE GrantPermission;


