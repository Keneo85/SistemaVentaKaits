

declare @DatabaseName nvarchar(50)
set @DatabaseName = N'Kaits'

declare @SQL varchar(max)

select @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';'
from master..SysProcesses
where DBId = DB_ID(@DatabaseName) AND SPId <> @@SPId

--select @SQL 
exec(@SQL)


use master
go

if exists(select * from sysdatabases where name = 'Kaits')
DROP DATABASE Kaits
Go

create database Kaits
go

use Kaits
go

drop table if exists Cliente
go
create table Cliente
(
Id_Cliente int primary key identity(1,1) not null,
Nombre varchar(100) not null,
ApellidoPaterno varchar(100) null,
ApellidoMaterno varchar(100) null,
DNI char(8) not null
)
go

drop table if exists Producto
go
create table Producto
(
Id_Producto int primary key identity(1,1) not null,
Descripcion varchar(200) not null,
Precio float not null
)
go

drop table if exists Pedido
go
create table Pedido
(
Id_Pedido int primary key not null,
Fecha_Pedido datetime not null,
Id_Cliente int not null,
Total_Pedido float not null
)
go

drop table if exists Detalle_Pedido
go
create table Detalle_Pedido
(
Id_Pedido_Detalle int primary key identity(1,1) not null,
Id_Pedido int not null,
Id_Producto int not null,
Cantidad int not null,
Precio_Unitario float not null,
Subtotal float not null,
IGV float not null
)
go

ALTER TABLE Pedido
ADD FOREIGN KEY (Id_Cliente) REFERENCES Cliente(Id_Cliente);
go

alter table Detalle_Pedido  with check add  constraint fk_Detalle_Pedido_Pedido foreign key(Id_Pedido)
references Pedido (Id_Pedido)
go

alter table Detalle_Pedido check constraint fk_Detalle_Pedido_Pedido
go

alter table Detalle_Pedido  with check add  constraint fk_Detalle_Pedido_Producto foreign key(Id_Producto)
references Producto(Id_Producto)
go

alter table Detalle_Pedido check constraint fk_Detalle_Pedido_Producto
go

-------------------------------------------------------------------------------------------------cliente
if exists(select 1 from sys.procedures where name = 'Grabar_Cliente')
	drop procedure Grabar_Cliente
go
create procedure Grabar_Cliente 
@Nombre varchar(100),
@ApellidoPaterno varchar(100),
@ApellidoMaterno varchar(100),
@DNI char(8)
as
insert into Cliente
           (Nombre,
            ApellidoPaterno,
            ApellidoMaterno,
            DNI)
     values
           (@Nombre,
            @ApellidoPaterno,
            @ApellidoMaterno,
            @DNI)
go

if exists(select 1 from sys.procedures where name = 'Actualizar_Cliente')
	drop procedure Actualizar_Cliente
go
create procedure Actualizar_Cliente 
@Id_Cliente int,
@Nombre varchar(100),
@ApellidoPaterno varchar(100),
@ApellidoMaterno varchar(100),
@DNI char(8)
as
update Cliente
   set Nombre = @Nombre, 
       ApellidoPaterno = @ApellidoPaterno,
       ApellidoMaterno = @ApellidoMaterno, 
       DNI = @DNI
 where Id_Cliente=@Id_Cliente
go

if exists(select 1 from sys.procedures where name = 'Listar_Cliente')
	drop procedure Listar_Cliente
go
create procedure Listar_Cliente 
as
select Id_Cliente,Nombre,ApellidoPaterno,ApellidoMaterno,DNI from Cliente 
go

if exists(select 1 from sys.procedures where name = 'Buscar_Cliente')
	drop procedure Buscar_Cliente
go
create procedure Buscar_Cliente
@Cadena varchar(300)
as
select Id_Cliente,Nombre,ApellidoPaterno,ApellidoMaterno,DNI from Cliente 
where 
((Nombre=@Cadena) or (Nombre +''+ ApellidoPaterno +''+ ApellidoMaterno like '%' + @Cadena + '%'))
and
((Nombre=@Cadena) or (DNI like '%' + @Cadena + '%' ))
go

if exists(select 1 from sys.procedures where name = 'Obtener_Cliente')
	drop procedure Obtener_Cliente
go
create procedure Obtener_Cliente 
@Id_Cliente int
as
select Id_Cliente,Nombre,ApellidoPaterno,ApellidoMaterno,DNI from Cliente where Id_Cliente=@Id_Cliente
go

if exists(select 1 from sys.procedures where name = 'Eliminar_Cliente')
	drop procedure Eliminar_Cliente
go
create procedure Eliminar_Cliente
@Id_Cliente int
as
delete from Cliente where Id_Cliente=@Id_Cliente
go




-------------------------------------------------------------------------------------------------producto
if exists(select 1 from sys.procedures where name = 'Grabar_Producto')
	drop procedure Grabar_Producto
go
create procedure Grabar_Producto 
@Descripcion varchar(200),
@Precio float
as
insert into Producto
           (
		    Descripcion,
			Precio
		   )
     values
           (
		    @Descripcion,
			@Precio
		   )
go

if exists(select 1 from sys.procedures where name = 'Actualizar_Producto')
	drop procedure Actualizar_Producto
go
create procedure Actualizar_Producto 
@Id_Producto int,
@Descripcion varchar(200),
@Precio float
as
update Producto
   set Descripcion = @Descripcion,
       Precio = @Precio
 where Id_Producto=@Id_Producto
go

if exists(select 1 from sys.procedures where name = 'Listar_Producto')
	drop procedure Listar_Producto
go
create procedure Listar_Producto 
as
select Id_Producto, Descripcion, Precio from Producto 
go

if exists(select 1 from sys.procedures where name = 'Buscar_Producto')
	drop procedure Buscar_Producto
go
create procedure Buscar_Producto
@Descripcion varchar(200)
as
select Id_Producto, Descripcion, Precio from Producto where Descripcion like '%' + @Descripcion + '%'
go

if exists(select 1 from sys.procedures where name = 'Obtener_Producto')
	drop procedure Obtener_Producto
go
create procedure Obtener_Producto
@Id_Producto int
as
select Id_Producto,Descripcion, Precio from Producto where Id_Producto=@Id_Producto
go

if exists(select 1 from sys.procedures where name = 'Eliminar_Producto')
	drop procedure Eliminar_Producto
go
create procedure Eliminar_Producto
@Id_Producto int
as
delete from Producto where Id_Producto=@Id_Producto
go



-------------------------------------------------------------------------------------------------Pedido
go

if exists(select 1 from sys.procedures where name = 'Grabar_Pedido')
	drop procedure Grabar_Pedido
go
create procedure Grabar_Pedido
@Id_Pedido int,
@Fecha_Pedido datetime,
@Id_Cliente int,
@Total_Pedido float,
@NroPedido int output
as
insert into Pedido
           (
		    Id_Pedido,
		    Fecha_Pedido,
			Id_Cliente,
			Total_Pedido
		   )
     values
           (
		    @Id_Pedido,
		    @Fecha_Pedido,
			@Id_Cliente,
			@Total_Pedido
		   )
set @NroPedido = (select max(Id_Pedido) as Id_Pedido from Pedido)
go

if exists(select 1 from sys.procedures where name = 'Actualizar_Pedido')
	drop procedure Actualizar_Pedido
go
create procedure Actualizar_Pedido 
@Id_Pedido int,
@Fecha_Pedido datetime,
@Id_Cliente int,
@Total_Pedido float
as
update Pedido
   set Fecha_Pedido = @Fecha_Pedido,
       Id_Cliente = @Id_Cliente,
       Total_Pedido = @Total_Pedido
 where Id_Pedido=@Id_Pedido
go

if exists(select 1 from sys.procedures where name = 'Listar_Pedido')
	drop procedure Listar_Pedido 
go
create procedure Listar_Pedido 
as
select 
Id_Pedido,
ci.Id_Cliente,
ci.Nombre +' '+ ci.ApellidoPaterno +' '+ ci.ApellidoMaterno as Cliente,
Fecha_Pedido,
Total_Pedido 
from Pedido pe 
inner join Cliente ci on pe.Id_Cliente=ci.Id_Cliente
go

if exists(select 1 from sys.procedures where name = 'Listar_Detalle_Pedido')
	drop procedure Listar_Detalle_Pedido 
go
create procedure Listar_Detalle_Pedido 
@Id_Pedido int
as
select  
Id_Pedido_Detalle,
Id_Pedido,
dp.Id_Producto,
pr.Descripcion as Producto,
Cantidad,
Precio_Unitario,
Subtotal,
IGV
from Detalle_Pedido dp
inner join Producto pr on dp.Id_Producto=pr.Id_Producto where Id_Pedido=@Id_Pedido
go

if exists(select 1 from sys.procedures where name = 'Obtener_Pedido')
	drop procedure Obtener_Pedido
go
create procedure Obtener_Pedido
@Id_Pedido int
as
select 
Id_Pedido,
ci.Id_Cliente,
ci.Nombre +' '+ ci.ApellidoPaterno +' '+ ci.ApellidoMaterno as Cliente,
Fecha_Pedido,
Total_Pedido 
from Pedido pe 
inner join Cliente ci on pe.Id_Cliente=ci.Id_Cliente where Id_Pedido=@Id_Pedido 
go

if exists(select 1 from sys.procedures where name = 'Obtener_Numero_Pedido')
	drop procedure Obtener_Numero_Pedido
go
create procedure Obtener_Numero_Pedido
as
select max(Id_Pedido) + 1 as NroPedido from Pedido
go

if exists(select 1 from sys.procedures where name = 'Grabar_Detalle_Pedido_xml')
	drop procedure Grabar_Detalle_Pedido_xml
go
create procedure Grabar_Detalle_Pedido_xml
@Detalle XML
as
declare @i int;
declare @tablaDetalle table(
	Id_Pedido int ,
	Id_Producto int,
	Cantidad int,
	Precio_Unitario float,
	Subtotal float,
	IGV float
)

exec sp_xml_preparedocument @i output, @detalle
insert into @tablaDetalle(
	Id_Pedido,
	Id_Producto,
	Cantidad,
	Precio_Unitario,
	Subtotal,
	IGV
)
select * from openxml(@i, N'/DocumentElement/Detalle')
with
(
	Id_Pedido int ,
	Id_Producto int,
	Cantidad int,
	Precio_Unitario float,
	Subtotal float,
	IGV float
)
exec
sp_xml_removedocument @i;
go

if exists(select 1 from sys.procedures where name = 'Grabar_Detalle_Pedido')
	drop procedure Grabar_Detalle_Pedido
go
create procedure Grabar_Detalle_Pedido
@Id_Pedido int,
@Id_Producto int,
@Cantidad int,
@Precio_Unitario float,
@Subtotal float,
@IGV float
as
insert into Detalle_Pedido
			(
				Id_Pedido,
				Id_Producto,
				Cantidad,
				Precio_Unitario,
				Subtotal,
				IGV
			)
			values 
			(
			    @Id_Pedido,
			    @Id_Producto,
				@Cantidad,
				@Precio_Unitario,
				@Subtotal,
				@IGV
			)
go

if exists(select 1 from sys.procedures where name = 'Eliminar_Detalle_Pedido')
	drop procedure Eliminar_Detalle_Pedido
go
create procedure Eliminar_Detalle_Pedido
@Id_Pedido int
as
delete from Detalle_Pedido where Id_Pedido=@Id_Pedido
go

if exists(select 1 from sys.procedures where name = 'Eliminar_Pedido')
	drop procedure Eliminar_Pedido
go
create procedure Eliminar_Pedido
@Id_Pedido int
as
delete from Pedido where Id_Pedido=@Id_Pedido
go




-------------------------------------------------------------------------------------------------insert
delete from Cliente
insert into Cliente values('Erick Daniel','Maldonado','Solis','11224455')
insert into Cliente values('Jose Felipe','Sanango','Cerna','46789533')
insert into Cliente values('Carlos Karin','Epinoza','Medrano','01784598')
insert into Cliente values('Pedro Manuel','Orderique','Vargas','10895634')
insert into Cliente values('Ivan Marcos','Quiroz','Otiniano','43568591')
insert into Cliente values('Gian Carlos','Quispe','Mayta','96857411')
insert into Cliente values('Rodolfo vidal','Avila','sota','37119573')

delete from Producto
insert into Producto values('Pintura Mate - Azul 5 lt', 60.00)
insert into Producto values('Sellador 5 lt', 70.00)
insert into Producto values('Yeso Ceramico x kilo', 2.00)
insert into Producto values('Carburo 1.5 kg', 15.80)
insert into Producto values('Cemente Sol 45 kg', 45.10)
insert into Producto values('Escalera 2 mt', 120.50)
insert into Producto values('Lija 180 mm', 1.50)
insert into Producto values('Soldadura - varilla suelta', 5.50)


--subtotal = (1*60) / 1.18 = 50.84745762711864 (consideraremos 2 decimales)
--igv = ((1*60) / 1.18) * 18%(0.18) =  9.1512 (consideraremos 2 decimales)
--60.00
--70.00
--130.00
delete from Detalle_Pedido
delete from Pedido
go
insert into Pedido values(1,'2023-03-16', 1, 130)
insert into Detalle_Pedido values(1, 1, 1, 60, 50.8474576271186, 9.15254237288135)
insert into Detalle_Pedido values(1, 2, 1, 70, 59.3220338983051, 10.6779661016949)
