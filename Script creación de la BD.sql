use master 
go
if NOT EXISTS (Select * From sys.databases WHERE name = 'DB_Ecommerce_DB_Programacion_III')
Begin 
Create Database DB_Ecommerce_DB_Programacion_III 
End 
go 
use DB_Ecommerce_DB_Programacion_III
go 

create table Categorias(
	IdCategoria tinyint primary key identity(1,1),
	Descripcion varchar(100) not null 
)

create table Marcas(
	IdMarca tinyint primary key identity(1,1),
	Descripcion varchar(100) not null 
)

create table Productos(
	IdProducto int primary key identity(1,1), 
	Nombre nvarchar(100) not null,
	Precio money not null, 
	Estado bit not null default 1, 
	IdCategoria tinyint foreign key references Categorias(IdCategoria), 
	IdMarca tinyint foreign key references Marcas(IdMarca)
)

create table ImagenesDeProductos(
	IdImagen int primary key identity(1,1), 
	IdProducto int foreign key references Productos(IdProducto),
	UrlImagen nvarchar(1000) not null
)
