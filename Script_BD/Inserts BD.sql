USE DB_Ecommerce_DB_Programacion_III
GO

INSERT INTO Categorias (Descripcion) VALUES 
('Periféricos'),
('Hardware'),
('Muebles Gamer'),
('Audio'),
('Almacenamiento');

INSERT INTO Marcas (Descripcion) VALUES 
('Logitech'),
('Corsair'),
('Razer'),
('HyperX'),
('ASUS'),
('MSI'),
('SteelSeries');

INSERT INTO Productos (Nombre, Precio, Estado, IdCategoria, IdMarca) VALUES
('Mouse Gamer Logitech G502 Hero', 28000, 1, 1, 1),
('Teclado Mecánico Corsair K95 RGB', 42000, 1, 1, 2),
('Auriculares HyperX Cloud II', 33000, 1, 4, 4),
('Silla Gamer Razer Iskur', 180000, 1, 3, 3),
('Disco SSD M.2 NVMe 1TB Samsung 980 PRO', 72000, 1, 5, 5),
('Placa de Video ASUS RTX 4070 Ti', 850000, 1, 2, 5),
('Monitor MSI Optix 27” 165Hz', 190000, 1, 2, 6);

INSERT INTO ImagenesDeProductos (IdProducto, UrlImagen) VALUES
(1, 'https://www.malditohard.com.ar/wp-content/uploads/product/8/MOU094.webp'),
(2, 'https://www.afinformatica.com.ar/img/productos/724/ch-9127414-na-2-3.png'),
(3, 'https://spacegamer.com.ar/img/Public/1058-producto-d-nq-np-949149-mla43567600295-092020-v-2315.jpg'),
(4, 'https://assets2.razerzone.com/images/pnx.assets/75b4af343b3d3579b80e50f5e101645e/iskurx-mobile-usp4.webp'),
(5, 'https://tienda.starware.com.ar/wp-content/uploads/2021/02/disco-solido-ssd-nvme-m2-samsung-980-evo-pro-250gb-pcie-40-2273-3477.jpg'),
(6, 'https://spacegamer.com.ar/img/Public/1058-producto-1-6727.jpg'),
(7, 'https://asset.msi.com/resize/image/global/product/product_16278954729ea4df3a81312014ab4e46d130b517ed.png62405b38c58fe0f07fcef2367d8a9ba1/600.png');