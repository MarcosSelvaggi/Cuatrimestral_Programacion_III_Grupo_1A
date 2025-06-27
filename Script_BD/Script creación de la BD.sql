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
	Stock int not null check (Stock >= 0),
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

create table Carrito(
	IdCarrito int primary key identity(1,1),
	IdUsuario int foreign key references Usuarios(IdUsuario)
)

create table Detalles(
	IdDetalle int primary key identity(1,1),
	IdCarrito int foreign key references Carrito(IdCarrito),
	IdProducto int foreign key references Productos(IdProducto),
	Cantidad int not null check (Cantidad >= 1),
	PrecioUnitario money not null
)

create table EstadoDePedidos (
    IDEstadoPedido tinyint primary key identity(1,1),
    Descripcion varchar(100) not null unique
)

create table MetodosDePago (
    IDMetodoPago tinyint primary key identity(1,1),
    Descripcion varchar(100) not null unique
)

create table EstadoDeEnvios (
    IDEnvio int primary key identity(1,1),
    FechaDeEnvio datetime not null,
    Descripcion varchar(255) not null
)

create table EstadoDePagos (
    IdEstadoPago tinyint primary key identity(1,1),
    Descripcion varchar(50) not null unique
)

create table DetalleDePagos (
    IDPago int primary key identity(1,1),
    IDMetodoPago tinyint not null foreign key references MetodosDePago(IDMetodoPago),
    FechaDePago datetime not null,
    Detalles varchar(255) not null
) 

create table Pedidos (
    IDPedido int primary key identity(1,1),
    IDCliente int not null foreign key references Usuarios(IdUsuario),
    IDEnvio int not null foreign key references EstadoDeEnvios(IDEnvio),
    IDEstadoPedido tinyint not null foreign key references EstadoDePedidos(IDEstadoPedido),
	IdEstadoPago tinyint not null foreign key references EstadoDePagos(IdEstadoPago),
    FechaDePedido datetime not null default getdate(),
    PrecioTotal money null,
    IDPago int not null foreign key references DetalleDePagos(IDPago)
)

create table DetalleDePedidos (
    IDPedido int not null foreign key references Pedidos(IDPedido),
    IDProducto int not null foreign key references Productos(IDProducto),
    Cantidad int not null check (Cantidad >= 1),
    PrecioUnitario money not null,
    Subtotal money null,
    Impuestos tinyint not null default 21 check (Impuestos between 0 and 100),
    primary key (IDPedido, IDProducto)
)