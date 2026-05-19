using MasterServicesAPI.Models;
using Microsoft.EntityFrameworkCore;

public partial class AppDbContext : DbContext

{
    private IConfiguration _configuration;
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;

    }

    public DbSet<MasterServicesAPI.Models.FacturaEvento> FacturaEventos { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.ItemsFactura> ItemsFactura { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.Factura> Facturas { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.DetalleVenta> DetalleVentas { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.ProductosServicio> ProductosServicios { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.ClientesFiscal> ClientesFiscal { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.Descuento> Descuentos { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.Pago> Pagos { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.Venta> Ventas { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.ServiciosCliente> ServiciosClientes { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.ReportesFinanciero> ReportesFinancieros { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.Gasto> Gastos { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.Cliente> Clientes { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.Proveedore> Proveedores { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.Categoria> Categorias { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.Usuario> Usuarios { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.Role> Roles { get; set; } = default!;

    public DbSet<MasterServicesAPI.Models.AuditLog> AuditLogs { get; set; } = default!;
    public DbSet<MasterServicesAPI.Models.MetodosPago> MetodosPago { get; set; } = default!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("AppDbContext");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F16AE7B6B");

            entity.ToTable("categorias");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__clientes__3213E83F65A1DF1A");

            entity.ToTable("clientes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.Departamento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("departamento");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_documento");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_documento");
        });

        modelBuilder.Entity<ClientesFiscal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__clientes__3213E83FCF041EF1");

            entity.ToTable("clientes_fiscal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Departamento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("departamento");
            entity.Property(e => e.DireccionFiscal)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("direccion_fiscal");
            entity.Property(e => e.Nit)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nit");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("razon_social");
            entity.Property(e => e.RegimenFiscal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("regimen_fiscal");

            entity.HasOne(d => d.Cliente).WithMany(p => p.ClientesFiscals)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__clientes___clien__7755B73D");
        });

        modelBuilder.Entity<Descuento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__descuent__3213E83FD82E6081");

            entity.ToTable("descuentos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.MontoDescuento)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto_descuento");
            entity.Property(e => e.VentaId).HasColumnName("venta_id");

            entity.HasOne(d => d.Venta).WithMany(p => p.Descuentos)
                .HasForeignKey(d => d.VentaId)
                .HasConstraintName("FK__descuento__venta__0E391C95");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalle___3213E83F10B2D8EC");

            entity.ToTable("detalle_ventas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.DescuentoPorcentaje)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("descuento_porcentaje");
            entity.Property(e => e.DescuentoValor)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("descuento_valor");
            entity.Property(e => e.IvaPorcentaje)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("iva_porcentaje");
            entity.Property(e => e.IvaValor)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("iva_valor");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.PrecioUnitarioSinIva)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario_sin_iva");
            entity.Property(e => e.ProductoServicioId).HasColumnName("producto_servicio_id");
            entity.Property(e => e.SubtotalConIva)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal_con_iva");
            entity.Property(e => e.SubtotalSinIva)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal_sin_iva");
            entity.Property(e => e.VentaId).HasColumnName("venta_id");

            entity.HasOne(d => d.ProductoServicio).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.ProductoServicioId)
                .HasConstraintName("FK__detalle_v__produ__719CDDE7");

            entity.HasOne(d => d.Venta).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.VentaId)
                .HasConstraintName("FK__detalle_v__venta__70A8B9AE");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__facturas__3213E83FA09EB81B");

            entity.ToTable("facturas");

            entity.HasIndex(e => e.NumeroFactura, "UQ__facturas__3DC4B2410A413955").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClienteFiscalId).HasColumnName("cliente_fiscal_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Emitida")
                .HasColumnName("estado");
            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_emision");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("datetime")
                .HasColumnName("fecha_vencimiento");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("iva");
            entity.Property(e => e.Notas)
                .HasColumnType("text")
                .HasColumnName("notas");
            entity.Property(e => e.NumeroFactura)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_factura");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");
            entity.Property(e => e.VentaId).HasColumnName("venta_id");

            entity.HasOne(d => d.ClienteFiscal).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ClienteFiscalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__facturas__client__7E02B4CC");

            entity.HasOne(d => d.Venta).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__facturas__venta___7D0E9093");
        });

        modelBuilder.Entity<FacturaEvento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__factura___3213E83FEE084456");

            entity.ToTable("factura_eventos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.FacturaId).HasColumnName("factura_id");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.TipoEvento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_evento");



            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Factura).WithMany(p => p.FacturaEventos)
                .HasForeignKey(d => d.FacturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__factura_e__factu__05A3D694");

            entity.HasOne(d => d.Usuario).WithMany(p => p.FacturaEventos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__factura_e__usuar__0697FACD");
        });

        modelBuilder.Entity<Gasto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__gastos__3213E83F48747A41");

            entity.ToTable("gastos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");
        });

        modelBuilder.Entity<ItemsFactura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__items_fa__3213E83F83B0ED39");

            entity.ToTable("items_factura");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.DetalleVentaId).HasColumnName("detalle_venta_id");
            entity.Property(e => e.FacturaId).HasColumnName("factura_id");
            entity.Property(e => e.IvaPorcentaje)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("iva_porcentaje");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.DetalleVenta).WithMany(p => p.ItemsFacturas)
                .HasForeignKey(d => d.DetalleVentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__items_fac__detal__01D345B0");

            entity.HasOne(d => d.Factura).WithMany(p => p.ItemsFacturas)
                .HasForeignKey(d => d.FacturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__items_fac__factu__00DF2177");
        });

        modelBuilder.Entity<MetodosPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__metodos___3213E83FE7E87425");

            entity.ToTable("metodos_pago");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pagos__3213E83FF8324485");

            entity.ToTable("pagos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.MetodoPagoId).HasColumnName("metodo_pago_id");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.VentaId).HasColumnName("venta_id");

            entity.HasOne(d => d.MetodoPago).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.MetodoPagoId)
                .HasConstraintName("FK__pagos__metodo_pa__0B5CAFEA");

            entity.HasOne(d => d.Venta).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.VentaId)
                .HasConstraintName("FK__pagos__venta_id__0A688BB1");
        });

        modelBuilder.Entity<ProductosServicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producto__3213E83F53ACF41B");

            entity.ToTable("productos_servicios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.CodigoInterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigo_interno");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.ProveedorId).HasColumnName("proveedor_id");
            entity.Property(e => e.Stock)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("stock");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo");
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("unidad_medida");
            entity.Property(e => e.ExcluidoIva)
                .HasDefaultValue(false)
                .HasColumnName("excluido_iva");

            entity.Property(e => e.IVA)
                .HasColumnName("IVA");

            entity.HasOne(d => d.Categoria).WithMany(p => p.ProductosServicios)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK__productos__categ__65370702");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.ProductosServicios)
                .HasForeignKey(d => d.ProveedorId)
                .HasConstraintName("FK__productos__prove__662B2B3B");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__proveedo__3213E83F66E2123E");

            entity.ToTable("proveedores");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contacto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contacto");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoServicio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipo_servicio");
        });

        modelBuilder.Entity<ReportesFinanciero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reportes__3213E83FF75FBBE2");

            entity.ToTable("reportes_financieros");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Beneficio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("beneficio");
            entity.Property(e => e.FechaGeneracion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_generacion");
            entity.Property(e => e.Gastos)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("gastos");
            entity.Property(e => e.Ingresos)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("ingresos");
            entity.Property(e => e.Periodo)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("periodo");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F2917D218");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<ServiciosCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__servicio__3213E83F4CA5E428");

            entity.ToTable("servicios_cliente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Servicio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("servicio");

            entity.HasOne(d => d.Cliente).WithMany(p => p.ServiciosClientes)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__servicios__clien__74794A92");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83FCD48643D");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "UQ__usuarios__AB6E6164F2D2161D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.Estado)
                .HasColumnName("estado")
                .HasDefaultValue(false);
            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__usuarios__rol_id__5D95E53A");

            // Relación uno a muchos con AuditLog
            entity.HasMany(d => d.AuditLogs)
                .WithOne(e => e.Usuario)  // Cada AuditLog pertenece a un Usuario
                .HasForeignKey(e => e.UsuarioId)  // Clave foránea en AuditLog
                .OnDelete(DeleteBehavior.SetNull); // Definir el comportamiento de eliminación (puedes modificarlo si es necesario)
        });
        // Configuración de la entidad AuditLog
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__audit_logs__3213E83F2917D218");
            entity.ToTable("AuditLogs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioId");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TableName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Changes)
                .HasColumnType("text");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("Timestamp");
            entity.Property(e => e.AdditionalInfo)
                .HasColumnType("text")
                .HasColumnName("AdditionalInfo");

            // Relación con Usuario (muchos a uno)
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.SetNull);  // Puedes ajustar el comportamiento de eliminación aquí también
        });
        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ventas__3213E83F85D2D6AE");

            entity.ToTable("ventas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.CondicionPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("condicion_pago");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.DescuentoTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("descuento_total");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Completado")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("datetime")
                .HasColumnName("fecha_vencimiento");
            entity.Property(e => e.IvaTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("iva_total");
            entity.Property(e => e.MetodoPagoId).HasColumnName("metodo_pago_id");
            entity.Property(e => e.Moneda)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValue("COP")
                .HasColumnName("moneda");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Factura")
                .HasColumnName("tipo_documento");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Venta)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__ventas__cliente___6CD828CA");

            entity.HasOne(d => d.MetodoPago).WithMany(p => p.Venta)
                .HasForeignKey(d => d.MetodoPagoId)
                .HasConstraintName("FK__ventas__metodo_p__6DCC4D03");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
