/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2013/8/15 22:14:23                           */
/*==============================================================*/

drop table if exists Account;

drop table if exists Account_Role;

drop table if exists Menu;

drop table if exists Permission;

drop table if exists Role;

drop table if exists Role_Permission;

/*==============================================================*/
/* Table: Account                                               */
/*==============================================================*/
create table Account
(
   Id                   bigint not null auto_increment,
   LoginId              national varchar(256) not null,
   Password             national varchar(256) not null,
   Name                 national varchar(128),
   Sex                  int,
   Email                varchar(128),
   Phone                char(32),
   Memo                 national varchar(256),
   IsEnabled            bool,
   LoginTimes           int,
   LastLoginIP          char(32),
   IsDeleted            bool,
   LastLoginTime        datetime,
   CreatedOn            datetime,
   UpdatedOn            datetime,
   primary key (Id),
   key AK_Key_2 (LoginId),
   key AK_Key_3 (Email),
   key AK_Key_4 (Phone)
);

/*==============================================================*/
/* Table: Account_Role                                          */
/*==============================================================*/
create table Account_Role
(
   Account_Id           bigint not null,
   Role_Id              bigint not null,
   primary key (Account_Id, Role_Id)
);

/*==============================================================*/
/* Table: Menu                                                  */
/*==============================================================*/
create table Menu
(
   Id                   char(128) not null,
   Name                 national varchar(128) not null,
   Url                  varchar(255) not null,
   ParentId             char(128),
   Category             char(32) not null,
   Level                int,
   primary key (Id)
);

/*==============================================================*/
/* Table: Permission                                            */
/*==============================================================*/
create table Permission
(
   Id                   char(128) not null,
   MenuId               char(128),
   Name                 national varchar(256),
   Action               varchar(256),
   primary key (Id)
);

/*==============================================================*/
/* Table: Role                                                  */
/*==============================================================*/
create table Role
(
   Id                   bigint not null auto_increment,
   Name                 national char(128),
   HomePage             varchar(256),
   Memo                 national varchar(256),
   IsEnabled            bool,
   IsDeleted            bool,
   Ordinal              int,
   CreatedOn            datetime,
   UpdatedOn            datetime,
   primary key (Id)
);

/*==============================================================*/
/* Table: Role_Permission                                       */
/*==============================================================*/
create table Role_Permission
(
   Role_Id              bigint not null,
   Permission_Id        char(128) not null,
   primary key (Role_Id, Permission_Id)
);

alter table Account_Role add constraint FK_Reference_1 foreign key (Account_Id)
      references Account (Id) on delete cascade;

alter table Account_Role add constraint FK_Reference_2 foreign key (Role_Id)
      references Role (Id) on delete cascade;

alter table Permission add constraint FK_Reference_5 foreign key (MenuId)
      references Menu (Id);

alter table Role_Permission add constraint FK_Reference_3 foreign key (Role_Id)
      references Role (Id) on delete cascade;

alter table Role_Permission add constraint FK_Reference_4 foreign key (Permission_Id)
      references Permission (Id) on delete cascade;

