using GestorTaller.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestorTaller.Datos;

public class GestorTallerDbContext : DbContext
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Empleado> Empleados => Set<Empleado>();
    public DbSet<Orden> Ordenes => Set<Orden>();
    public DbSet<Repuesto> Repuestos => Set<Repuesto>();
    public DbSet<OrdenRepuesto> OrdenRepuestos => Set<OrdenRepuesto>();

    public GestorTallerDbContext(DbContextOptions<GestorTallerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // PostgreSQL (timestamp with time zone) solo acepta fechas en UTC, pero el resto de la
        // aplicacion trabaja con la hora local (DateTime.Now). Se guarda en UTC y al leer se
        // convierte de vuelta a hora local.
        var fechaAUtc = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc).ToLocalTime());

        var fechaNulaAUtc = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue ? v.Value.ToUniversalTime() : v,
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc).ToLocalTime() : v);

        modelBuilder.Entity<Cliente>(entidad =>
        {
            entidad.ToTable("cliente");
            entidad.HasKey(c => c.Id);
            entidad.Property(c => c.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
            entidad.Property(c => c.Telefono).HasColumnName("telefono").HasMaxLength(20);
            entidad.Property(c => c.Correo).HasColumnName("correo").HasMaxLength(150);
            entidad.Property(c => c.Direccion).HasColumnName("direccion").HasMaxLength(300);
        });

        modelBuilder.Entity<Empleado>(entidad =>
        {
            entidad.ToTable("empleado");
            entidad.HasKey(e => e.Id);
            entidad.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
            entidad.Property(e => e.NombreUsuario).HasColumnName("nombre_usuario").HasMaxLength(50).IsRequired();
            entidad.HasIndex(e => e.NombreUsuario).IsUnique();
            entidad.Property(e => e.PasswordUsuario).HasColumnName("password_usuario").HasMaxLength(255).IsRequired();
            entidad.Property(e => e.EsAdministrador).HasColumnName("es_administrador").IsRequired();
        });

        modelBuilder.Entity<Orden>(entidad =>
        {
            entidad.ToTable("orden");
            entidad.HasKey(o => o.Id);
            entidad.Property<Guid>("ClienteId").HasColumnName("cliente_id");
            entidad.HasOne(o => o.Cliente).WithMany().HasForeignKey("ClienteId").IsRequired();
            entidad.Property(o => o.DescripcionObjeto).HasColumnName("descripcion_objeto").HasMaxLength(300).IsRequired();
            entidad.Property(o => o.DescripcionProblema).HasColumnName("descripcion_problema").HasMaxLength(300).IsRequired();
            entidad.Property(o => o.CostoDiagnostico).HasColumnName("costo_diagnostico").HasColumnType("numeric(10,2)");
            entidad.Property(o => o.Estado).HasColumnName("estado").HasConversion<string>().HasMaxLength(20).IsRequired();
            entidad.Property(o => o.FechaRecepcion).HasColumnName("fecha_recepcion").HasConversion(fechaAUtc);

            entidad.Property<Guid?>("EmpleadoDiagnosticoId").HasColumnName("empleado_diagnostico_id");
            entidad.HasOne(o => o.EmpleadoDiagnostico).WithMany().HasForeignKey("EmpleadoDiagnosticoId").OnDelete(DeleteBehavior.Restrict);
            entidad.Property(o => o.DetalleDiagnostico).HasColumnName("detalle_diagnostico").HasMaxLength(500);

            entidad.Property<Guid?>("EmpleadoCotizacionId").HasColumnName("empleado_cotizacion_id");
            entidad.HasOne(o => o.EmpleadoCotizacion).WithMany().HasForeignKey("EmpleadoCotizacionId").OnDelete(DeleteBehavior.Restrict);
            entidad.Property(o => o.MontoCotizado).HasColumnName("monto_cotizado").HasColumnType("numeric(10,2)");
            entidad.Property(o => o.CotizacionAceptada).HasColumnName("cotizacion_aceptada");

            entidad.Property<Guid?>("EmpleadoReparacionId").HasColumnName("empleado_reparacion_id");
            entidad.HasOne(o => o.EmpleadoReparacion).WithMany().HasForeignKey("EmpleadoReparacionId").OnDelete(DeleteBehavior.Restrict);
            entidad.Property(o => o.DetalleReparacion).HasColumnName("detalle_reparacion").HasMaxLength(500);

            entidad.Property<Guid?>("EmpleadoEntregaId").HasColumnName("empleado_entrega_id");
            entidad.HasOne(o => o.EmpleadoEntrega).WithMany().HasForeignKey("EmpleadoEntregaId").OnDelete(DeleteBehavior.Restrict);
            entidad.Property(o => o.MontoPagado).HasColumnName("monto_pagado").HasColumnType("numeric(10,2)");
            entidad.Property(o => o.FechaEntrega).HasColumnName("fecha_entrega").HasConversion(fechaNulaAUtc);

            entidad.Ignore(o => o.HistorialEstados);
        });

        modelBuilder.Entity<Repuesto>(entidad =>
        {
            entidad.ToTable("repuesto", tabla =>
            {
                tabla.HasCheckConstraint("ck_repuesto_precio", "precio >= 0");
                tabla.HasCheckConstraint("ck_repuesto_existencia", "existencia >= 0");
            });
            entidad.HasKey(r => r.Id);
            entidad.Property(r => r.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
            entidad.Property(r => r.Existencia).HasColumnName("existencia").IsRequired();
            entidad.Property(r => r.Precio).HasColumnName("precio").HasPrecision(10, 2).IsRequired();
        });

        modelBuilder.Entity<HistorialEstadoOrden>(entidad =>
        {
            entidad.ToTable("orden_historial_estado");
            entidad.HasKey(h => h.Id);
            entidad.Property(h => h.Id).ValueGeneratedOnAdd();
            entidad.Property(h => h.Estado).HasColumnName("estado").HasConversion<string>().HasMaxLength(20).IsRequired();
            entidad.Property(h => h.OrdenPosicion).HasColumnName("orden_posicion");
            entidad.HasOne<Orden>().WithMany().HasForeignKey(h => h.OrdenId);
        });

        modelBuilder.Entity<OrdenRepuesto>(entidad =>
        {
            entidad.ToTable("orden_repuesto", tabla =>
            {
                tabla.HasCheckConstraint("ck_orden_repuesto_cantidad", "cantidad > 0");
                tabla.HasCheckConstraint("ck_orden_repuesto_precio_unitario", "precio_unitario >= 0");
            });
            entidad.HasKey(r => new { r.OrdenId, r.RepuestoId });
            entidad.Property(r => r.OrdenId).HasColumnName("orden_id");
            entidad.Property(r => r.RepuestoId).HasColumnName("repuesto_id");
            entidad.Property(r => r.Cantidad).HasColumnName("cantidad").IsRequired();
            entidad.Property(r => r.PrecioUnitario).HasColumnName("precio_unitario").HasPrecision(10, 2).IsRequired();
            entidad.HasOne<Orden>().WithMany(o => o.Repuestos).HasForeignKey(r => r.OrdenId);
            entidad.HasOne(r => r.Repuesto).WithMany().HasForeignKey(r => r.RepuestoId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}