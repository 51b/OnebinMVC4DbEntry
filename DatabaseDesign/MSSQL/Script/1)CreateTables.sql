/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2013/8/15 22:02:09                           */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Account_Role') and o.name = 'FK_ACCOUNT__REFERENCE_ACCOUNT')
alter table Account_Role
   drop constraint FK_ACCOUNT__REFERENCE_ACCOUNT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Account_Role') and o.name = 'FK_ACCOUNT__REFERENCE_ROLE')
alter table Account_Role
   drop constraint FK_ACCOUNT__REFERENCE_ROLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Permission') and o.name = 'FK_PERMISSI_REFERENCE_MENU')
alter table Permission
   drop constraint FK_PERMISSI_REFERENCE_MENU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Role_Permission') and o.name = 'FK_ROLE_PER_REFERENCE_ROLE')
alter table Role_Permission
   drop constraint FK_ROLE_PER_REFERENCE_ROLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Role_Permission') and o.name = 'FK_ROLE_PER_REFERENCE_PERMISSI')
alter table Role_Permission
   drop constraint FK_ROLE_PER_REFERENCE_PERMISSI
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Account')
            and   type = 'U')
   drop table Account
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Account_Role')
            and   type = 'U')
   drop table Account_Role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Menu')
            and   type = 'U')
   drop table Menu
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Permission')
            and   type = 'U')
   drop table Permission
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Role')
            and   type = 'U')
   drop table Role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Role_Permission')
            and   type = 'U')
   drop table Role_Permission
go

/*==============================================================*/
/* Table: Account                                               */
/*==============================================================*/
create table Account (
   Id                   bigint               identity,
   LoginId              nvarchar(256)        not null,
   Password             nvarchar(256)        not null,
   Name                 nvarchar(128)        null,
   Sex                  int                  null,
   Email                varchar(128)         null,
   Phone                char(32)             null,
   Memo                 nvarchar(256)        null,
   IsEnabled            bit                  null,
   LoginTimes           int                  null,
   LastLoginIP          char(32)             null,
   IsDeleted            bit                  null,
   LastLoginTime        datetime             null,
   CreatedOn            datetime             null,
   UpdatedOn            datetime             null,
   constraint PK_ACCOUNT primary key nonclustered (Id),
   constraint AK_KEY_2_ACCOUNT unique (LoginId),
   constraint AK_KEY_3_ACCOUNT unique (Email),
   constraint AK_KEY_4_ACCOUNT unique (Phone)
)
go

/*==============================================================*/
/* Table: Account_Role                                          */
/*==============================================================*/
create table Account_Role (
   Account_Id           bigint               not null,
   Role_Id              bigint               not null,
   constraint PK_ACCOUNT_ROLE primary key nonclustered (Account_Id, Role_Id)
)
go

/*==============================================================*/
/* Table: Menu                                                  */
/*==============================================================*/
create table Menu (
   Id                   char(128)            not null,
   Name                 nvarchar(128)        not null,
   Url                  varchar(255)         not null,
   ParentId             char(128)            null,
   Category             char(32)             not null,
   Level                int                  null,
   constraint PK_MENU primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: Permission                                            */
/*==============================================================*/
create table Permission (
   Id                   char(128)            not null,
   MenuId               char(128)            null,
   Name                 nvarchar(256)        null,
   Action               varchar(256)         null,
   constraint PK_PERMISSION primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: Role                                                  */
/*==============================================================*/
create table Role (
   Id                   bigint               identity,
   Name                 nchar(128)           null,
   HomePage             varchar(256)         null,
   Memo                 nvarchar(256)        null,
   IsEnabled            bit                  null,
   IsDeleted            bit                  null,
   Ordinal              int                  null,
   CreatedOn            datetime             null,
   UpdatedOn            datetime             null,
   constraint PK_ROLE primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: Role_Permission                                       */
/*==============================================================*/
create table Role_Permission (
   Role_Id              bigint               not null,
   Permission_Id        char(128)            not null,
   constraint PK_ROLE_PERMISSION primary key nonclustered (Role_Id, Permission_Id)
)
go

alter table Account_Role
   add constraint FK_ACCOUNT__REFERENCE_ACCOUNT foreign key (Account_Id)
      references Account (Id)
         on delete cascade
go

alter table Account_Role
   add constraint FK_ACCOUNT__REFERENCE_ROLE foreign key (Role_Id)
      references Role (Id)
         on delete cascade
go

alter table Permission
   add constraint FK_PERMISSI_REFERENCE_MENU foreign key (MenuId)
      references Menu (Id)
go

alter table Role_Permission
   add constraint FK_ROLE_PER_REFERENCE_ROLE foreign key (Role_Id)
      references Role (Id)
         on delete cascade
go

alter table Role_Permission
   add constraint FK_ROLE_PER_REFERENCE_PERMISSI foreign key (Permission_Id)
      references Permission (Id)
         on delete cascade
go

