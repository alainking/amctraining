/*DB---------------------------*/

if not exists (select 1 from sys.databases where name = 'amctraining')
begin
  create database amctraining
end
go

/*Tables---------------------------*/

use amctraining
go
if not exists (select 1 from sys.objects where name = 'Products' and type = 'U')
begin
  create table Products
       ( ID            int           identity(1,1),
         Code          nvarchar(20)  not null,
         [Description] nvarchar(200) not null,
         CreateUser    nvarchar(100) not null,
         CreateDate    datetime2     not null,
         EditUser      nvarchar(100) not null,
         EditDate      datetime2     not null 
         constraint pk_ProductID primary key (ID),
         constraint uk_ProductCode unique (Code))
end
if not exists (select 1 from sys.objects where name = 'Suppliers' and type = 'U')
begin
  create table Suppliers
       ( ID            int           identity(1,1),
         Code          nvarchar(20)  not null,
         [Description] nvarchar(200) not null,
         ContactNo     nvarchar(20)      null,
         CreateUser    nvarchar(100) not null,
         CreateDate    datetime2     not null,
         EditUser      nvarchar(100) not null,
         EditDate      datetime2     not null 
         constraint pk_SupplierID primary key (ID),
         constraint uk_SupplierCode unique (Code))
end

/*Products---------------------------*/

if exists (select 1 from sys.objects where name = 'rsp_Products_Get' and type = 'P')
  drop procedure rsp_Products_Get
go

create procedure rsp_Products_Get

as

select ID,
       Code,
       Description,
       CreateUser,
       CreateDate,
       EditUser,
       EditDate
  from Products
go

grant execute on rsp_Products_Get to public
go

if exists (select 1 from sys.objects where name = 'rsp_Products_Find' and type = 'P')
  drop procedure rsp_Products_Find
go

create procedure rsp_Products_Find
     ( @Id     int)
as

select ID,
       Code,
       Description,
       CreateUser,
       CreateDate,
       EditUser,
       EditDate
  from Products
 where ID = @Id
go

grant execute on rsp_Products_Find to public
go

if exists (select 1 from sys.objects where name = 'rsp_Products_FindByCode' and type = 'P')
  drop procedure rsp_Products_FindByCode
go

create procedure rsp_Products_FindByCode
     ( @Code     nvarchar(20))
as

select ID,
       Code,
       Description,
       CreateUser,
       CreateDate,
       EditUser,
       EditDate
  from Products
 where Code = @Code
go

grant execute on rsp_Products_FindByCode to public
go

if exists (select 1 from sys.objects where name = 'rsp_Products_Add' and type = 'P')
  drop procedure rsp_Products_Add
go

create procedure rsp_Products_Add
     ( @Code        nvarchar(20),
       @Description nvarchar(200),
       @CreateUser  nvarchar(100),
	   @ID          int output)
as

insert Products 
     ( Code, 
       Description, 
       CreateUser, 
       CreateDate, 
       EditUser, 
       EditDate ) 
values 
     ( @Code,
       @Description,
       @CreateUser,
       getdate(),
       @CreateUser, 
       getdate())

select @ID = SCOPE_IDENTITY()

go

grant execute on rsp_Products_Add to public
go

if exists (select 1 from sys.objects where name = 'rsp_Products_Edit' and type = 'P')
  drop procedure rsp_Products_Edit
go

create procedure rsp_Products_Edit
     ( @ID          int,
       @Code        nvarchar(20),
       @Description nvarchar(200),
       @EditUser    nvarchar(100))
as

update Products 
   set Code        = @Code,
       Description = @Description,
       EditUser    = @EditUser,
       EditDate    = getdate()
 where ID = @ID

go

grant execute on rsp_Products_Edit to public
go

if exists (select 1 from sys.objects where name = 'rsp_Products_Del' and type = 'P')
  drop procedure rsp_Products_Del
go

create procedure rsp_Products_Del
     ( @ID          int )
as

delete
  from Products 
 where ID = @ID

go

grant execute on rsp_Products_Del to public
go

/*Suppliers---------------------------*/

if exists (select 1 from sys.objects where name = 'rsp_Suppliers_Get' and type = 'P')
  drop procedure rsp_Suppliers_Get
go

create procedure rsp_Suppliers_Get

as

select ID,
       Code,
       Description,
	     ContactNo,
       CreateUser,
       CreateDate,
       EditUser,
       EditDate
  from Suppliers
go

grant execute on rsp_Suppliers_Get to public
go

if exists (select 1 from sys.objects where name = 'rsp_Suppliers_Find' and type = 'P')
  drop procedure rsp_Suppliers_Find
go

create procedure rsp_Suppliers_Find
     ( @Id     int)
as

select ID,
       Code,
       Description,
	     ContactNo,
       CreateUser,
       CreateDate,
       EditUser,
       EditDate
  from Suppliers
 where ID = @Id
go

grant execute on rsp_Suppliers_Find to public
go

if exists (select 1 from sys.objects where name = 'rsp_Suppliers_FindByCode' and type = 'P')
  drop procedure rsp_Suppliers_FindByCode
go

create procedure rsp_Suppliers_FindByCode
     ( @Code     nvarchar(20))
as

select ID,
       Code,
       Description,
	     ContactNo,
       CreateUser,
       CreateDate,
       EditUser,
       EditDate
  from Suppliers
 where Code = @Code
go

grant execute on rsp_Suppliers_FindByCode to public
go

if exists (select 1 from sys.objects where name = 'rsp_Suppliers_Add' and type = 'P')
  drop procedure rsp_Suppliers_Add
go

create procedure rsp_Suppliers_Add
     ( @Code        nvarchar(20),
       @Description nvarchar(200),
  	   @ContactNo   nvarchar(20),
       @CreateUser  nvarchar(100),
	     @ID          int output)
as

insert Suppliers 
     ( Code, 
       Description, 
	     ContactNo,
       CreateUser, 
       CreateDate, 
       EditUser, 
       EditDate ) 
values 
     ( @Code,
       @Description,
       @ContactNo,
       @CreateUser,
       getdate(),
       @CreateUser, 
       getdate())

select @ID = SCOPE_IDENTITY()

go

grant execute on rsp_Suppliers_Add to public
go

if exists (select 1 from sys.objects where name = 'rsp_Suppliers_Edit' and type = 'P')
  drop procedure rsp_Suppliers_Edit
go

create procedure rsp_Suppliers_Edit
     ( @ID          int,
       @Code        nvarchar(20),
       @Description nvarchar(200),
	     @ContactNo   nvarchar(20),
       @EditUser    nvarchar(100))
as

update Suppliers
   set Code        = @Code,
       Description = @Description,
	     ContactNo   = @ContactNo,
       EditUser    = @EditUser,
       EditDate    = getdate()
 where ID = @ID

go

grant execute on rsp_Suppliers_Edit to public
go

if exists (select 1 from sys.objects where name = 'rsp_Suppliers_Del' and type = 'P')
  drop procedure rsp_Suppliers_Del
go

create procedure rsp_Suppliers_Del
     ( @ID          int )
as

delete
  from Suppliers 
 where ID = @ID

go

grant execute on rsp_Suppliers_Del to public
go

/*Data---------------------------*/

insert Products
     ( Code,
	   Description,
	   CreateUser,
	   CreateDate,
	   EditUser,
	   EditDate )
values 
     ( 'key', 'Keyboard', 'sample', getdate(), 'sample', getdate()),
	 ( 'mse', 'Mouse', 'sample', getdate(), 'sample', getdate()),
	 ( 'hdd', 'Hard Drive', 'sample', getdate(), 'sample', getdate()),
	 ( 'ssd', 'Solid State Drive', 'sample', getdate(), 'sample', getdate()),
	 ( 'cpui7', 'CPU - i7', 'sample', getdate(), 'sample', getdate()),
	 ( 'cpui5', 'CPU - i5', 'sample', getdate(), 'sample', getdate()),
	 ( 'tv40', '40 inch TV', 'sample', getdate(), 'sample', getdate()),
	 ( 'tv65', '65 inch TV', 'sample', getdate(), 'sample', getdate()),
	 ( 'raspi', 'Raspberry PI', 'sample', getdate(), 'sample', getdate()),
	 ( 'lap', 'Laptop', 'sample', getdate(), 'sample', getdate())
	 
insert Suppliers
     ( Code,
	   Description,
	   ContactNo,
	   CreateUser,
	   CreateDate,
	   EditUser,
	   EditDate )
values 
	 ( 'Dell', 'Delll', '0123456', 'sample', getdate(), 'sample', getdate()),
	 ( 'HP', 'HP', '', 'sample', getdate(), 'sample', getdate()),
	 ( 'FTSA', 'Frontosa', '', 'sample', getdate(), 'sample', getdate()),
	 ( 'Mstk', 'Mustek', '', 'sample', getdate(), 'sample', getdate()),
	 ( 'Chaos', 'Chaos', '', 'sample', getdate(), 'sample', getdate())
