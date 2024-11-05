﻿using Microsoft.EntityFrameworkCore;
namespace DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        
        }

        public virtual DbSet<Cargo> Cargos { get; set; }

        public virtual DbSet<Categoria> Categoria { get; set; }

        public virtual DbSet<CitaServicio> CitaServicios { get; set; }

        public virtual DbSet<Cita> Cita { get; set; }

        public virtual DbSet<Cliente> Clientes { get; set; }

        public virtual DbSet<Combo> Combos { get; set; }

        public virtual DbSet<ComboServicio> ComboServicios { get; set; }

        public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

        public virtual DbSet<Empleado> Empleados { get; set; }

        public virtual DbSet<EmpleadoHorario> EmpleadoHorarios { get; set; }

        public virtual DbSet<Factura> Facturas { get; set; }

        public virtual DbSet<Horario> Horarios { get; set; }
        public virtual DbSet<MetodoPago> MetodoPago { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }

        public virtual DbSet<Persona> Personas { get; set; }

        public virtual DbSet<Promocion> Promocions { get; set; }

        public virtual DbSet<Servicio> Servicios { get; set; }

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

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.Categoria1)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CitaServicio>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("CitaServicio");

                entity.HasOne(d => d.IdCitaNavigation).WithMany()
                    .HasForeignKey(d => d.IdCita)
                    .HasConstraintName("FK_CitaServicio_Cita");
            });

            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(e => e.IdCita);

                entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_Cita_Cliente");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("Cliente");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Combo>(entity =>
            {
                entity.HasKey(e => e.IdCombo);

                entity.ToTable("Combo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPromocionNavigation).WithMany(p => p.Combos)
                    .HasForeignKey(d => d.IdPromocion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Combo_Promocion");
            });

            modelBuilder.Entity<ComboServicio>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("ComboServicio");

                entity.HasOne(d => d.IdComboNavigation).WithMany()
                    .HasForeignKey(d => d.IdCombo)
                    .HasConstraintName("FK_ComboServicio_Combo");

                entity.HasOne(d => d.IdServicioNavigation).WithMany()
                    .HasForeignKey(d => d.IdServicio)
                    .HasConstraintName("FK_ComboServicio_Servicio");
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

                entity.HasOne(d => d.IdPersonaEmpleadoNavigation).WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdPersonaEmpleado)
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

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura);

                entity.ToTable("Factura");

                entity.Property(e => e.MontoTotal).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdCitaNavigation).WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdCita)
                    .HasConstraintName("FK_Factura_Cita");
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => e.IdHorario);

                entity.ToTable("Horario");

                entity.Property(e => e.Dia)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("Persona");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");
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
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio);

                entity.ToTable("Servicio");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Servicio1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Servicio");

                entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Servicios)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Servicio_Categoria");
            });

        }


    }
}
