# MasterServices API

Sistema integral de gestión empresarial: facturación, inventario, clientes, ventas y reportes financieros.

## Screenshots

> Próximamente — agrega capturas del sistema aquí.

<!-- Reemplaza esta línea con tu imagen:
![Dashboard](docs/screenshots/dashboard.png)
-->

## Tecnologías

- **Framework:** ASP.NET Core 8.0 (C#)
- **ORM:** Entity Framework Core 8.0
- **Base de datos:** SQL Server
- **Autenticación:** JWT Bearer
- **Documentación API:** Swagger / OpenAPI

## Características

- Autenticación y autorización basada en roles con JWT
- CRUD completo de clientes, proveedores, productos y categorías
- Gestión de ventas y facturación electrónica
- Control de pagos y métodos de pago
- Generación de reportes financieros
- Registro de auditoría de cambios del sistema
- Integración con APIs externas de facturación

## Módulos

| Módulo | Descripción |
|--------|-------------|
| Autenticación | Login con JWT y control de sesiones |
| Clientes | Gestión de clientes y datos fiscales |
| Proveedores | Administración de proveedores |
| Inventario | Productos, servicios y categorías |
| Ventas | Ciclo completo de ventas y detalles |
| Facturación | Facturas, items y eventos de factura |
| Pagos | Métodos y registros de pago |
| Usuarios | Gestión de usuarios y roles |
| Reportes | Reportes financieros y estadísticas |
| Auditoría | Trazabilidad de cambios en el sistema |

## Requisitos previos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (Express o superior)

## Instalación

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/tu-usuario/masterservices-api.git
   cd masterservices-api
   ```

2. Copiar los archivos de configuración:
   ```bash
   cp appsettings.example.json appsettings.json
   cp appsettings.Development.example.json appsettings.Development.json
   ```

3. Editar `appsettings.json` con tus valores reales:
   - Cadena de conexión a SQL Server
   - Clave secreta JWT (mínimo 32 caracteres)

4. Crear la base de datos ejecutando el script incluido:
   ```bash
   sqlcmd -S . -i "SCRIPT CREATE BDMASTERSERVICES.sql"
   ```

5. Ejecutar la aplicación:
   ```bash
   dotnet run
   ```

6. Acceder a Swagger UI en: `https://localhost:7060/swagger`

## Configuración

Copiar `appsettings.example.json` → `appsettings.json` y completar los valores:

| Clave | Descripción |
|-------|-------------|
| `ConnectionStrings:AppDbContext` | Cadena de conexión a SQL Server |
| `Jwt:Key` | Clave secreta para firmar tokens JWT (mín. 32 caracteres) |
| `Jwt:Issuer` | Emisor del token JWT |
| `Jwt:Audience` | Audiencia del token JWT |

## Estructura del proyecto

```
MasterServicesAPI/
├── Controllers/          # Endpoints REST (17 controladores)
├── Models/               # Entidades de base de datos
├── Data/                 # AppDbContext (Entity Framework Core)
├── Security/             # Configuración JWT y políticas de autorización
├── appsettings.example.json     # Plantilla de configuración
└── Program.cs            # Configuración de servicios y middleware
```

## Licencia

MIT License. Ver [LICENSE](LICENSE) para más detalles.
