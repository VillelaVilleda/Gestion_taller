using GestorTaller.Negocio;
using Microsoft.EntityFrameworkCore;

namespace GestorTaller.Datos;

public class GestorTallerDbContext : DbContext
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Orden> Ordenes => Set<Orden>();

    public GestorTallerDbContext(DbContextOptions<GestorTallerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entidad =>
        {
            entidad.ToTable("cliente");
            entidad.HasKey(c => c.Id);
            entidad.Property(c => c.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
            entidad.Property(c => c.Telefono).HasColumnName("telefono").HasMaxLength(20);
            entidad.Property(c => c.Correo).HasColumnName("correo").HasMaxLength(150);
            entidad.Property(c => c.Direccion).HasColumnName("direccion").HasMaxLength(300);
        });

        modelBuilder.Entity<Usuario>(entidad =>
        {
            entidad.ToTable("usuario");
            entidad.HasKey(u => u.Id);
            entidad.Property(u => u.NombreUsuario).HasColumnName("nombre_usuario").HasMaxLength(50).IsRequired();
            entidad.Property(u => u.Contrasena).HasColumnName("contrasena").HasMaxLength(255).IsRequired();
            entidad.Property(u => u.Rol).HasColumnName("rol").HasConversion<string>().HasMaxLength(20).IsRequired();
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
            entidad.Property(o => o.FechaRecepcion).HasColumnName("fecha_recepcion");

            entidad.Property(o => o.EmpleadoDiagnostico).HasColumnName("empleado_diagnostico").HasMaxLength(150);
            entidad.Property(o => o.DetalleDiagnostico).HasColumnName("detalle_diagnostico").HasMaxLength(500);

            entidad.Property(o => o.EmpleadoCotizacion).HasColumnName("empleado_cotizacion").HasMaxLength(150);
            entidad.Property(o => o.MontoCotizado).HasColumnName("monto_cotizado").HasColumnType("numeric(10,2)");
            entidad.Property(o => o.CotizacionAceptada).HasColumnName("cotizacion_aceptada");

            entidad.Property(o => o.EmpleadoReparacion).HasColumnName("empleado_reparacion").HasMaxLength(150);
            entidad.Property(o => o.DetalleReparacion).HasColumnName("detalle_reparacion").HasMaxLength(500);

            entidad.Property(o => o.EmpleadoEntrega).HasColumnName("empleado_entrega").HasMaxLength(150);
            entidad.Property(o => o.MontoPagado).HasColumnName("monto_pagado").HasColumnType("numeric(10,2)");
            entidad.Property(o => o.FechaEntrega).HasColumnName("fecha_entrega");

            entidad.Ignore(o => o.HistorialEstados);
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
    }
}