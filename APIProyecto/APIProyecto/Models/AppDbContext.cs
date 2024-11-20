using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace APIProyecto.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Detallefactura> Detallefacturas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Empleadohorario> Empleadohorarios { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Horarioatencion> Horarioatencions { get; set; }

    public virtual DbSet<Metodopago> Metodopagos { get; set; }

    public virtual DbSet<Numerodocumento> Numerodocumentos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Promocion> Promocions { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<EmpleadoServicio> EmpleadoServicios { get; set; }

    public virtual DbSet<Servicioreserva> Servicioreservas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=pruebahorario;user=root;password=IvAn", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.1.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración global de collation y charset
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        // Configuración de Cargo
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo).HasName("PRIMARY");

            entity.ToTable("cargo");

            entity.Property(e => e.NombreCargo)
                .HasMaxLength(50)
                .IsRequired();
        });

        // Configuración de Cliente
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.IdPersona, "IdPersona");

            entity.Property(e => e.Contraseña)
                .HasMaxLength(256)
                .IsRequired();

            entity.HasOne(d => d.IdPersonaNavigation)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cliente_ibfk_1");
        });

        // Configuración de DetalleFactura
        modelBuilder.Entity<Detallefactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFactura).HasName("PRIMARY");

            entity.ToTable("detallefactura");

            entity.HasIndex(e => e.IdFactura, "IdFactura");
            entity.HasIndex(e => e.IdServicioReserva, "IdServicioReserva");

            entity.Property(e => e.PrecioServicio)
                .HasPrecision(10, 2)
                .IsRequired();

            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .IsRequired();

            entity.HasOne(d => d.IdFacturaNavigation)
                .WithMany(p => p.Detallefacturas)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detallefactura_ibfk_1");

            entity.HasOne(d => d.IdServicioReservaNavigation)
                .WithMany(p => p.Detallefacturas)
                .HasForeignKey(d => d.IdServicioReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detallefactura_ibfk_2");
        });

        // Configuración de Servicio
        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PRIMARY");

            entity.ToTable("servicio");

            entity.Property(e => e.NombreServicio)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(250);
        });

        // Configuración de EmpleadoServicio (Entidad Intermedia)
        modelBuilder.Entity<EmpleadoServicio>(entity =>
        {
            entity.HasKey(e => new { e.IdEmpleado, e.IdServicio }).HasName("PRIMARY");

            entity.ToTable("empleadoservicio");

            entity.HasOne(e => e.Empleado)
                .WithMany(e => e.EmpleadosServicio)
                .HasForeignKey(e => e.IdEmpleado)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("empleadoservicio_ibfk_1");

            entity.HasOne(e => e.Servicio)
                .WithMany(s => s.EmpleadosServicio)
                .HasForeignKey(e => e.IdServicio)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("empleadoservicio_ibfk_2");
        });

        // Configuración de Empleado
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PRIMARY");

            entity.ToTable("empleado");

            entity.HasIndex(e => e.IdPersona, "IdPersona");
            entity.HasIndex(e => e.IdCargo, "IdCargo");

            entity.Property(e => e.Comision)
                .HasPrecision(10, 2);

            entity.Property(e => e.Contraseña)
                .HasMaxLength(256)
                .IsRequired();

            entity.HasOne(d => d.IdPersonaNavigation)
                .WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("empleado_ibfk_1");

            entity.HasOne(d => d.IdCargoNavigation)
                .WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCargo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("empleado_ibfk_2");
        });

        // Configuración de EmpleadoHorario
        modelBuilder.Entity<Empleadohorario>(entity =>
        {
            entity.HasKey(e => new { e.IdEmpleado, e.IdHorario }).HasName("PRIMARY");

            entity.ToTable("empleadohorario");

            entity.HasIndex(e => e.IdHorario, "IdHorario");

            entity.Property(e => e.FechaInicio)
                .IsRequired();

            entity.Property(e => e.FechaFin);

            entity.Property(e => e.DiaSemana)
                .IsRequired();

            entity.Property(e => e.Disponible)
                .IsRequired();

            entity.HasOne(d => d.IdEmpleadoNavigation)
                .WithMany(p => p.Empleadohorarios)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("empleadohorario_ibfk_1");

            entity.HasOne(d => d.IdHorarioNavigation)
                .WithMany(p => p.Empleadohorarios)
                .HasForeignKey(d => d.IdHorario)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("empleadohorario_ibfk_2");
        });

        // Configuración de HorarioAtencion
        modelBuilder.Entity<Horarioatencion>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PRIMARY");

            entity.ToTable("horarioatencion");

            entity.Property(e => e.HoraInicio)
                .HasColumnType("time")
                .IsRequired();

            entity.Property(e => e.HoraFin)
                .HasColumnType("time")
                .IsRequired();
        });

        // Configuración de ServicioReserva
        modelBuilder.Entity<Servicioreserva>(entity =>
        {
            entity.HasKey(e => e.IdServicioReserva).HasName("PRIMARY");

            entity.ToTable("servicioreserva");

            entity.HasIndex(e => e.IdReserva, "IdReserva");
            entity.HasIndex(e => e.IdServicio, "IdServicio");

            entity.HasOne(d => d.IdReservaNavigation)
                .WithMany(p => p.Servicioreservas)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("servicioreserva_ibfk_1");

            entity.HasOne(d => d.IdServicioNavigation)
                .WithMany(p => p.Servicioreservas)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("servicioreserva_ibfk_2");
        });

        // Configuración de Factura
        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PRIMARY");

            entity.ToTable("factura");

            entity.HasIndex(e => e.IdReserva, "IdReserva");
            entity.HasIndex(e => e.NumeroDocumento, "NumeroDocumento").IsUnique();

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.MontoTotal)
                .HasPrecision(10, 2)
                .IsRequired();

            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsRequired();

            entity.HasOne(d => d.IdReservaNavigation)
                .WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("factura_ibfk_1");
        });

        // Configuración de Metodopago
        modelBuilder.Entity<Metodopago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PRIMARY");

            entity.ToTable("metodopago");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsRequired();
        });

        // Configuración de Numerodocumento
        modelBuilder.Entity<Numerodocumento>(entity =>
        {
            entity.HasKey(e => e.IdNumeroDocumento).HasName("PRIMARY");

            entity.ToTable("numerodocumento");
        });

        // Configuración de Pago
        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PRIMARY");

            entity.ToTable("pago");

            entity.HasIndex(e => e.IdFactura, "IdFactura");
            entity.HasIndex(e => e.IdMetodoPago, "IdMetodoPago"); // Índice para la relación

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .IsRequired();

            entity.HasOne(d => d.IdFacturaNavigation)
                .WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pago_ibfk_1");

            entity.HasOne(d => d.IdMetodoPagoNavigation)
                .WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdMetodoPago)
                .OnDelete(DeleteBehavior.SetNull) // Permitir null en la llave foránea
                .HasConstraintName("pago_ibfk_2");
        });

        // Configuración de Persona
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PRIMARY");

            entity.ToTable("persona");

            entity.HasIndex(e => e.Cedula, "Cedula").IsUnique();

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.FotoPerfil)
                .HasMaxLength(100);

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsRequired();
        });

        // Configuración de Promocion
        modelBuilder.Entity<Promocion>(entity =>
        {
            entity.HasKey(e => e.IdPromocion).HasName("PRIMARY");

            entity.ToTable("promocion");

            entity.HasIndex(e => e.IdServicio, "IdServicio");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsRequired();

            entity.Property(e => e.Descuento)
                .HasPrecision(10, 2)
                .IsRequired();

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsRequired();

            entity.HasOne(d => d.IdServicioNavigation)
                .WithMany(p => p.Promocions)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("promocion_ibfk_1");
        });

        // Configuración de Reserva
        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PRIMARY");

            entity.ToTable("reserva");

            entity.HasIndex(e => e.IdCliente, "IdCliente");
            entity.HasIndex(e => e.IdEmpleado, "IdEmpleado");

            entity.Property(e => e.EstadoReserva)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.HoraInicio)
                .HasColumnType("time")
                .IsRequired();

            entity.Property(e => e.HoraFin)
                .HasColumnType("time")
                .IsRequired();

            entity.HasOne(d => d.IdClienteNavigation)
                .WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reserva_ibfk_1");

            entity.HasOne(d => d.IdEmpleadoNavigation)
                .WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reserva_ibfk_2");
        });

        // Método parcial para configuraciones adicionales si las hay
        OnModelCreatingPartial(modelBuilder);
    }




    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
