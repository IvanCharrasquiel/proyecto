using Microsoft.EntityFrameworkCore;

namespace models;

public partial class ProyectoContext : DbContext
{
    public ProyectoContext()
    {
    }

    public ProyectoContext(DbContextOptions<ProyectoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EmpleadoHorario> EmpleadoHorarios { get; set; }

    public virtual DbSet<EmpleadoServicio> EmpleadoServicios { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<HorarioAtencion> HorarioAtencions { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Promocion> Promocions { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<ServicioReserva> ServicioReservas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo);

            entity.ToTable("Cargo");

            entity.Property(e => e.Cargo1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cargo");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.ToTable("Cliente");

            entity.Property(e => e.Contraseña)
                .HasMaxLength(256)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_Cliente_Persona");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFactura);

            entity.ToTable("DetalleFactura");

            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleFactura_Factura");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("FK_DetalleFactura_ServicioReserva");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado);

            entity.ToTable("Empleado");

            entity.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCargo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_Cargo");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_Persona");
        });

        modelBuilder.Entity<EmpleadoHorario>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EmpleadoHorario");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany()
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK_EmpleadoHorario_Empleado");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany()
                .HasForeignKey(d => d.IdHorario)
                .HasConstraintName("FK_EmpleadoHorario_Horario");
        });

        modelBuilder.Entity<EmpleadoServicio>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EmpleadoServicio");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany()
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK_EmpleadoServicio_Empleado");

            entity.HasOne(d => d.IdServicioNavigation).WithMany()
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("FK_EmpleadoServicio_Servicio");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura);

            entity.ToTable("Factura");

            entity.Property(e => e.MontoTotal).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK_Factura_Reserva");
        });

        modelBuilder.Entity<HorarioAtencion>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK_Horario");

            entity.ToTable("HorarioAtencion");

            entity.Property(e => e.Dia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoraFin).HasColumnType("datetime");
            entity.Property(e => e.HoraInicio).HasColumnType("datetime");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago);

            entity.ToTable("MetodoPago");

            entity.Property(e => e.MetodoPago1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MetodoPago");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago);

            entity.ToTable("Pago");

            entity.Property(e => e.IdPago).ValueGeneratedNever();
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_Pago_Factura");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdMetodoPago)
                .HasConstraintName("FK_Pago_MetodoPago");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona);

            entity.ToTable("Persona");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Promocion>(entity =>
        {
            entity.HasKey(e => e.IdPromocion);

            entity.ToTable("Promocion");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Promocions)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("FK_Promocion_Servicio");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK_Cita");

            entity.ToTable("Reserva");

            entity.Property(e => e.IdReserva).ValueGeneratedNever();
            entity.Property(e => e.EstadoReserva)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.HoraFin).HasColumnType("datetime");
            entity.Property(e => e.HoraInicio).HasColumnType("datetime");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_Reserva_Cliente");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK_Reserva_Empleado");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio);

            entity.ToTable("Servicio");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ServicioReserva>(entity =>
        {
            entity.HasKey(e => e.IdServicioReserva);

            entity.ToTable("ServicioReserva");

            entity.Property(e => e.IdServicioReserva).ValueGeneratedNever();

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.ServicioReservas)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK_ServicioReserva_Reserva");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.ServicioReservas)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("FK_ServicioReserva_Servicio");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
