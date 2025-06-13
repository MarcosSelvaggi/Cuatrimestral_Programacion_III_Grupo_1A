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
	Descripcion varchar(100) not null,
	Activo bit not null default 1
)

create table Marcas(
	IdMarca tinyint primary key identity(1,1),
	Descripcion varchar(100) not null,
	Activo bit not null default 1
)

create table Productos(
	IdProducto int primary key identity(1,1), 
	Nombre nvarchar(100) not null,
	Precio money not null, 
	Activo bit not null default 1, 
	IdCategoria tinyint foreign key references Categorias(IdCategoria), 
	IdMarca tinyint foreign key references Marcas(IdMarca)
)

create table ImagenesDeProductos(
	IdImagen int primary key identity(1,1), 
	IdProducto int foreign key references Productos(IdProducto),
	UrlImagen nvarchar(1000) not null
)

create table Rol(
	IdRol tinyint primary key identity(1,1),
	Descripcion VARCHAR(50) not null unique
	)

create table Usuarios(
	IdUsuario int primary key identity(1,1), 
	Email VARCHAR(100) not null unique, 
	Contraseña VARCHAR(100) not null,
	IdRol tinyint foreign key references Rol(IdRol),
	Activo bit not null default 1, 
	Documento VARCHAR(20) not null unique, 
	Nombre VARCHAR(50) not null, 
	Apellido VARCHAR(50) not null, 
	Provincia VARCHAR(50) not null, 
	Localidad VARCHAR(50) not null, 
	CodigoPostal VARCHAR(10) not null, 
	Direccion VARCHAR(100) not null, 
	Telefono VARCHAR(20) not null
)

create table Favoritos(
	IdFavorito int primary key identity(1,1),
	IdProducto int foreign key references Productos(IdProducto),
	IdUsuario int foreign key references Usuarios(IdUsuario)
)
