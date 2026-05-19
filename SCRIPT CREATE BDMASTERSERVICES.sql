USE BDMASTERSERVICES;

-- Borrado de tablas si existen (en orden inverso a las dependencias)
DROP TABLE IF EXISTS factura_eventos;
DROP TABLE IF EXISTS items_factura;
DROP TABLE IF EXISTS facturas;
DROP TABLE IF EXISTS detalle_ventas;
DROP TABLE IF EXISTS productos_servicios;
DROP TABLE IF EXISTS clientes_fiscal;
DROP TABLE IF EXISTS descuentos;
DROP TABLE IF EXISTS pagos;
DROP TABLE IF EXISTS ventas;
DROP TABLE IF EXISTS servicios_cliente;
DROP TABLE IF EXISTS reportes_financieros;
DROP TABLE IF EXISTS gastos;
DROP TABLE IF EXISTS clientes;
DROP TABLE IF EXISTS proveedores;
DROP TABLE IF EXISTS categorias;
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS roles;
DROP TABLE IF EXISTS metodos_pago;

-- Tablas base sin dependencias
CREATE TABLE roles (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    descripcion TEXT
);

CREATE TABLE categorias (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT
);

CREATE TABLE proveedores (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    contacto VARCHAR(100),
    telefono VARCHAR(20),
    email VARCHAR(100),
    tipo_servicio VARCHAR(100),
    fecha_registro DATETIME DEFAULT GETDATE()
);

CREATE TABLE metodos_pago (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    descripcion TEXT
);

-- Tablas con una dependencia
CREATE TABLE usuarios (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    contrasena VARCHAR(255) NOT NULL,
    rol_id INT,
    fecha_creacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (rol_id) REFERENCES roles(id)
);

CREATE TABLE clientes (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    email VARCHAR(100),
    fecha_registro DATETIME DEFAULT GETDATE(),
    tipo_documento VARCHAR(20),
    numero_documento VARCHAR(20),
    direccion VARCHAR(200),
    ciudad VARCHAR(100),
    departamento VARCHAR(100),
    codigo_postal VARCHAR(10)
);

-- Tablas con dependencias de productos y servicios
CREATE TABLE productos_servicios (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    tipo VARCHAR(20) NOT NULL, -- 'Producto' o 'Servicio'
    precio DECIMAL(10, 2) NOT NULL,
    stock INT DEFAULT NULL, -- NULL para servicios
    unidad_medida VARCHAR(20), -- 'Unidad', 'Hora', 'Kg', etc.
    categoria_id INT,
    proveedor_id INT,
    codigo_interno VARCHAR(50),
    estado bit DEFAULT 1,
    FOREIGN KEY (categoria_id) REFERENCES categorias(id),
    FOREIGN KEY (proveedor_id) REFERENCES proveedores(id)
);

-- Tablas relacionadas con ventas
CREATE TABLE ventas (
    id INT IDENTITY(1,1) PRIMARY KEY,
    cliente_id INT,
    fecha DATETIME DEFAULT GETDATE(),
    total DECIMAL(10, 2) NOT NULL,
    metodo_pago_id INT,
    descripcion TEXT,
    estado VARCHAR(20) DEFAULT 'Completado',
    tipo_documento VARCHAR(20) DEFAULT 'Factura',
    subtotal DECIMAL(10, 2),
    iva_total DECIMAL(10, 2),
    descuento_total DECIMAL(10, 2),
    moneda VARCHAR(3) DEFAULT 'COP',
    condicion_pago VARCHAR(50),
    fecha_vencimiento DATETIME,
    FOREIGN KEY (cliente_id) REFERENCES clientes(id),
    FOREIGN KEY (metodo_pago_id) REFERENCES metodos_pago(id)
);

CREATE TABLE detalle_ventas (
    id INT IDENTITY(1,1) PRIMARY KEY,
    venta_id INT,
    producto_servicio_id INT,
    cantidad INT NOT NULL,
    precio DECIMAL(10, 2) NOT NULL,
    descripcion TEXT,
    precio_unitario_sin_iva DECIMAL(10, 2),
    iva_porcentaje DECIMAL(5, 2),
    iva_valor DECIMAL(10, 2),
    subtotal_sin_iva DECIMAL(10, 2),
    subtotal_con_iva DECIMAL(10, 2),
    descuento_porcentaje DECIMAL(5, 2),
    descuento_valor DECIMAL(10, 2),
    FOREIGN KEY (venta_id) REFERENCES ventas(id),
    FOREIGN KEY (producto_servicio_id) REFERENCES productos_servicios(id)
);

-- Tablas relacionadas con clientes
CREATE TABLE servicios_cliente (
    id INT IDENTITY(1,1) PRIMARY KEY,
    cliente_id INT,
    servicio VARCHAR(100),
    FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);

CREATE TABLE clientes_fiscal (
    id INT IDENTITY(1,1) PRIMARY KEY,
    cliente_id INT NOT NULL,
    razon_social VARCHAR(200) NOT NULL,
    nit VARCHAR(20) NOT NULL,
    direccion_fiscal VARCHAR(200),
    ciudad VARCHAR(100),
    departamento VARCHAR(100),
    regimen_fiscal VARCHAR(50),
    FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);

-- Tablas de facturación
CREATE TABLE facturas (
    id INT IDENTITY(1,1) PRIMARY KEY,
    venta_id INT NOT NULL,
    numero_factura VARCHAR(50) NOT NULL UNIQUE,
    fecha_emision DATETIME DEFAULT GETDATE(),
    fecha_vencimiento DATETIME,
    subtotal DECIMAL(10, 2) NOT NULL,
    iva DECIMAL(10, 2) NOT NULL,
    total DECIMAL(10, 2) NOT NULL,
    estado VARCHAR(20) DEFAULT 'Emitida',
    notas TEXT,
    cliente_fiscal_id INT NOT NULL,
    FOREIGN KEY (venta_id) REFERENCES ventas(id),
    FOREIGN KEY (cliente_fiscal_id) REFERENCES clientes_fiscal(id)
);

CREATE TABLE items_factura (
    id INT IDENTITY(1,1) PRIMARY KEY,
    factura_id INT NOT NULL,
    detalle_venta_id INT NOT NULL,
    descripcion VARCHAR(200) NOT NULL,
    cantidad INT NOT NULL,
    precio_unitario DECIMAL(10, 2) NOT NULL,
    iva_porcentaje DECIMAL(5, 2) NOT NULL,
    subtotal DECIMAL(10, 2) NOT NULL,
    total DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (factura_id) REFERENCES facturas(id),
    FOREIGN KEY (detalle_venta_id) REFERENCES detalle_ventas(id)
);

CREATE TABLE factura_eventos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    factura_id INT NOT NULL,
    tipo_evento VARCHAR(50) NOT NULL,
    descripcion TEXT,
    fecha DATETIME DEFAULT GETDATE(),
    usuario_id INT,
    FOREIGN KEY (factura_id) REFERENCES facturas(id),
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
);

-- Tablas financieras
CREATE TABLE pagos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    venta_id INT,
    monto DECIMAL(10, 2) NOT NULL,
    fecha DATETIME DEFAULT GETDATE(),
    metodo_pago_id INT,
    estado VARCHAR(20),
    FOREIGN KEY (venta_id) REFERENCES ventas(id),
    FOREIGN KEY (metodo_pago_id) REFERENCES metodos_pago(id)
);

CREATE TABLE descuentos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    venta_id INT,
    monto_descuento DECIMAL(10, 2) NOT NULL,
    descripcion TEXT,
    FOREIGN KEY (venta_id) REFERENCES ventas(id)
);

CREATE TABLE gastos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    descripcion TEXT,
    monto DECIMAL(10, 2) NOT NULL,
    fecha DATETIME DEFAULT GETDATE()
);

CREATE TABLE reportes_financieros (
    id INT IDENTITY(1,1) PRIMARY KEY,
    periodo VARCHAR(7) NOT NULL,
    ingresos DECIMAL(10, 2) NOT NULL,
    gastos DECIMAL(10, 2) NOT NULL,
    beneficio DECIMAL(10, 2) NOT NULL,
    fecha_generacion DATETIME DEFAULT GETDATE()
);

-- Roles básicos
INSERT INTO roles (nombre, descripcion) VALUES
('Administrador', 'Control total del sistema'),
('Técnico', 'Encargado de reparaciones'),
('Vendedor', 'Manejo de ventas y atención al cliente'),
('Cajero', 'Manejo de pagos y facturación'),
('Almacenista', 'Control de inventario');

-- Categorías de productos y servicios
INSERT INTO categorias (nombre, descripcion) VALUES
('Repuestos PC', 'Componentes y repuestos para computadores de escritorio'),
('Repuestos Laptop', 'Componentes y repuestos para portátiles'),
('Servicios Técnicos', 'Servicios de reparación y mantenimiento'),
('Software', 'Servicios de instalación de programas'),
('Accesorios', 'Periféricos y accesorios varios');

-- Proveedores
INSERT INTO proveedores (nombre, contacto, telefono, email, tipo_servicio) VALUES
('TecnoPartes SAS', 'Juan Pérez', '3101234567', 'ventas@tecnopartes.co', 'Repuestos PC y Laptop'),
('SoftwarePro Colombia', 'María González', '3157894561', 'contact@softwarepro.co', 'Licencias Software'),
('Accesorios Digital', 'Carlos Ramírez', '3203216547', 'ventas@accesoriosdigital.com', 'Accesorios'),
('ImportTech', 'Ana Martínez', '3112589632', 'importtech@gmail.com', 'Componentes importados'),
('ServiTech', 'Pedro López', '3145896547', 'servicios@servitech.co', 'Servicios Técnicos');

-- Métodos de pago
INSERT INTO metodos_pago (nombre, descripcion) VALUES
('Efectivo', 'Pago en efectivo'),
('Tarjeta Débito', 'Pago con tarjeta débito'),
('Tarjeta Crédito', 'Pago con tarjeta crédito'),
('Transferencia', 'Transferencia bancaria'),
('Nequi', 'Pago por Nequi');

-- Usuarios
INSERT INTO usuarios (nombre, email, contrasena, rol_id) VALUES
('Admin Sistema', 'admin@pcservice.co', 'hash123', 1),
('Juan Técnico', 'tecnico@pcservice.co', 'hash456', 2),
('María Ventas', 'ventas@pcservice.co', 'hash789', 3),
('Carlos Caja', 'caja@pcservice.co', 'hashABC', 4),
('Ana Almacén', 'almacen@pcservice.co', 'hashDEF', 5);

-- Clientes
INSERT INTO clientes (nombre, telefono, email, tipo_documento, numero_documento, direccion, ciudad, departamento) VALUES
('Pedro Gómez', '3157894561', 'pedro@gmail.com', 'CC', '79854123', 'Calle 45 #23-67', 'Bogotá', 'Cundinamarca'),
('María López', '3209876543', 'maria@hotmail.com', 'CC', '52147896', 'Carrera 15 #78-90', 'Bogotá', 'Cundinamarca'),
('Empresa XYZ', '3112589632', 'contacto@xyz.com', 'NIT', '900852147', 'Av 68 #23-56', 'Bogotá', 'Cundinamarca'),
('Juan Castro', '3145896547', 'juan@yahoo.com', 'CC', '80123456', 'Calle 80 #45-67', 'Bogotá', 'Cundinamarca'),
('Ana Martínez', '3187894561', 'ana@gmail.com', 'CC', '51987654', 'Carrera 7 #45-89', 'Bogotá', 'Cundinamarca');

-- Productos y servicios
INSERT INTO productos_servicios (nombre, descripcion, tipo, precio, stock, unidad_medida, categoria_id, proveedor_id, codigo_interno) VALUES
('Disco Duro 1TB', 'Disco duro SATA 3.5" 7200RPM', 'Producto', 180000, 15, 'Unidad', 1, 1, 'HDD001'),
('Memoria RAM 8GB', 'Memoria DDR4 2666MHz', 'Producto', 150000, 20, 'Unidad', 1, 1, 'RAM001'),
('Mantenimiento Preventivo', 'Limpieza y diagnóstico general', 'Servicio', 80000, NULL, 'Servicio', 3, 5, 'SERV001'),
('Instalación Windows', 'Instalación Windows 10 + Drivers', 'Servicio', 60000, NULL, 'Servicio', 4, 2, 'SOFT001'),
('Batería Laptop', 'Batería para laptop HP/Dell', 'Producto', 220000, 10, 'Unidad', 2, 4, 'BAT001');

-- Ventas
INSERT INTO ventas (cliente_id, total, metodo_pago_id, descripcion, subtotal, iva_total, descuento_total) VALUES
(1, 214200, 1, 'Mantenimiento y memoria RAM', 180000, 34200, 0),
(2, 95200, 2, 'Mantenimiento preventivo', 80000, 15200, 0),
(3, 428400, 3, 'Disco duro y servicio instalación', 360000, 68400, 0),
(4, 261800, 1, 'Batería laptop', 220000, 41800, 0),
(5, 71400, 4, 'Instalación Windows', 60000, 11400, 0);

-- Detalle ventas
INSERT INTO detalle_ventas (venta_id, producto_servicio_id, cantidad, precio, precio_unitario_sin_iva, iva_porcentaje, iva_valor, subtotal_sin_iva, subtotal_con_iva) VALUES
(1, 2, 1, 150000, 150000, 19, 28500, 150000, 178500),
(1, 3, 1, 30000, 30000, 19, 5700, 30000, 35700),
(2, 3, 1, 80000, 80000, 19, 15200, 80000, 95200),
(3, 1, 1, 180000, 180000, 19, 34200, 180000, 214200),
(3, 4, 1, 180000, 180000, 19, 34200, 180000, 214200);

-- Servicios cliente
INSERT INTO servicios_cliente (cliente_id, servicio) VALUES
(1, 'Mantenimiento PC Desktop'),
(2, 'Mantenimiento Laptop'),
(3, 'Instalación Software'),
(4, 'Cambio Batería'),
(5, 'Instalación Windows');

-- Clientes fiscal
INSERT INTO clientes_fiscal (cliente_id, razon_social, nit, direccion_fiscal, ciudad, departamento, regimen_fiscal) VALUES
(1, 'Pedro Gómez', '79854123-1', 'Calle 45 #23-67', 'Bogotá', 'Cundinamarca', 'Simplificado'),
(2, 'María López', '52147896-2', 'Carrera 15 #78-90', 'Bogotá', 'Cundinamarca', 'Simplificado'),
(3, 'Empresa XYZ SAS', '900852147-3', 'Av 68 #23-56', 'Bogotá', 'Cundinamarca', 'Común'),
(4, 'Juan Castro', '80123456-4', 'Calle 80 #45-67', 'Bogotá', 'Cundinamarca', 'Simplificado'),
(5, 'Ana Martínez', '51987654-5', 'Carrera 7 #45-89', 'Bogotá', 'Cundinamarca', 'Simplificado');

-- Facturas
INSERT INTO facturas (venta_id, numero_factura, fecha_emision, fecha_vencimiento, subtotal, iva, total, cliente_fiscal_id) VALUES
(1, 'FACT-001', GETDATE(), DATEADD(day, 30, GETDATE()), 180000, 34200, 214200, 1),
(2, 'FACT-002', GETDATE(), DATEADD(day, 30, GETDATE()), 80000, 15200, 95200, 2),
(3, 'FACT-003', GETDATE(), DATEADD(day, 30, GETDATE()), 360000, 68400, 428400, 3),
(4, 'FACT-004', GETDATE(), DATEADD(day, 30, GETDATE()), 220000, 41800, 261800, 4),
(5, 'FACT-005', GETDATE(), DATEADD(day, 30, GETDATE()), 60000, 11400, 71400, 5);

-- Items factura
INSERT INTO items_factura (factura_id, detalle_venta_id, descripcion, cantidad, precio_unitario, iva_porcentaje, subtotal, total) VALUES
(1, 1, 'Memoria RAM 8GB', 1, 150000, 19, 150000, 178500),
(2, 2, 'Mantenimiento Preventivo', 1, 80000, 19, 80000, 95200),
(3, 3, 'Disco Duro 1TB', 1, 180000, 19, 180000, 214200),
(4, 4, 'Batería Laptop', 1, 220000, 19, 220000, 261800),
(5, 5, 'Instalación Windows', 1, 60000, 19, 60000, 71400);

-- Factura eventos
INSERT INTO factura_eventos (factura_id, tipo_evento, descripcion, usuario_id) VALUES
(1, 'Emisión', 'Factura emitida', 4),
(2, 'Emisión', 'Factura emitida', 4),
(3, 'Emisión', 'Factura emitida', 4),
(4, 'Emisión', 'Factura emitida', 4),
(5, 'Emisión', 'Factura emitida', 4);

-- Pagos
INSERT INTO pagos (venta_id, monto, metodo_pago_id, estado) VALUES
(1, 214200, 1, 'Completado'),
(2, 95200, 2, 'Completado'),
(3, 428400, 3, 'Completado'),
(4, 261800, 1, 'Completado'),
(5, 71400, 4, 'Completado');

-- Descuentos (solo para ejemplo, en este caso no se aplicaron descuentos)
INSERT INTO descuentos (venta_id, monto_descuento, descripcion) VALUES
(1, 0, 'Sin descuento'),
(2, 0, 'Sin descuento'),
(3, 0, 'Sin descuento'),
(4, 0, 'Sin descuento'),
(5, 0, 'Sin descuento');

-- Gastos
INSERT INTO gastos (descripcion, monto) VALUES
('Arriendo local', 2500000),
('Servicios públicos', 450000),
('Insumos limpieza', 150000),
('Papelería', 100000),
('Internet y telefonía', 200000);

-- Reportes financieros
INSERT INTO reportes_financieros (periodo, ingresos, gastos, beneficio) VALUES
('2024-01', 5000000, 3400000, 1600000),
('2024-02', 5500000, 3500000, 2000000),
('2024-03', 4800000, 3300000, 1500000),
('2024-04', 5200000, 3450000, 1750000),
('2024-05', 5300000, 3400000, 1900000);
