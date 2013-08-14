/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2013/8/12 16:06:44                           */
/*==============================================================*/


drop table if exists Account;

drop table if exists Account_Role;

drop table if exists DictionaryCatalog;

drop table if exists DictionaryDetail;

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
   LoginId              national varchar(255) not null,
   Password             national varchar(255) not null,
   Name                 national varchar(128),
   Sex                  int,
   Email                national varchar(128),
   Phone                national varchar(128),
   Memo                 national char(1),
   IsEnabled            bool,
   LoginTimes           int,
   LastLoginIP          national varchar(128),
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
/* Table: DictionaryCatalog                                     */
/*==============================================================*/
create table DictionaryCatalog
(
   Id                   bigint not null auto_increment comment '目录Id',
   Val                  national varchar(128) not null comment '值',
   Name                 national varchar(128) not null comment '名称',
   Memo                 national char(1),
   Account_Id           varchar(36) comment '最后修改人',
   SavedOn              datetime comment '最后修改时间',
   IsDeleted            bool comment '是否删除',
   primary key (Id),
   key AK_Key_2 (Val)
);

/*==============================================================*/
/* Table: DictionaryDetail                                      */
/*==============================================================*/
create table DictionaryDetail
(
   Id                   bigint not null auto_increment comment '字典ID',
   DictionaryCatalog_Id bigint,
   Val                  national varchar(128) not null comment '字典值',
   Txt                  national varchar(128) not null comment '字典名称',
   Ordinal              int comment '排序',
   Account_Id           varchar(36) comment '最后修改人',
   SavedOn              datetime comment '最后修改时间',
   IsDeleted            bool comment '是否删除',
   primary key (Id)
);

/*==============================================================*/
/* Table: Menu                                                  */
/*==============================================================*/
create table Menu
(
   Id                   national varchar(255) not null,
   Name                 national varchar(255) not null,
   Url                  national varchar(255) not null,
   ParentId             national varchar(255),
   Category             national varchar(255) not null,
   Level                int,
   primary key (Id)
);

/*==============================================================*/
/* Table: Permission                                            */
/*==============================================================*/
create table Permission
(
   Id                   national varchar(255) not null,
   MenuId               national varchar(255),
   Name                 national varchar(255),
   Action               national varchar(255),
   primary key (Id)
);

/*==============================================================*/
/* Table: Role                                                  */
/*==============================================================*/
create table Role
(
   Id                   bigint not null auto_increment,
   Name                 national varchar(255),
   HomePage             national varchar(255),
   Memo                 national char(1),
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
   Permission_Id        national varchar(255) not null,
   primary key (Role_Id, Permission_Id)
);

alter table Account_Role add constraint FK_Reference_1 foreign key (Account_Id)
      references Account (Id) on delete cascade;

alter table Account_Role add constraint FK_Reference_2 foreign key (Role_Id)
      references Role (Id) on delete cascade;

alter table DictionaryDetail add constraint FK_Reference_6 foreign key (DictionaryCatalog_Id)
      references DictionaryCatalog (Id) on delete cascade;

alter table Permission add constraint FK_Reference_5 foreign key (MenuId)
      references Menu (Id);

alter table Role_Permission add constraint FK_Reference_3 foreign key (Role_Id)
      references Role (Id) on delete cascade;

alter table Role_Permission add constraint FK_Reference_4 foreign key (Permission_Id)
      references Permission (Id) on delete cascade;

