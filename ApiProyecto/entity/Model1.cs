using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace entity
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Cargo> Cargo { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cita> Cita { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Combo> Combo { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFactura { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<Horario> Horario { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Promocion> Promocion { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>()
                .Property(e => e.Cargo1)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .Property(e => e.Categoria1)
                .IsUnicode(false);

            modelBuilder.Entity<Combo>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Horario>()
                .Property(e => e.Dia)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Cita)
                .WithOptional(e => e.Persona)
                .HasForeignKey(e => e.IdPersonaCliente);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Cita1)
                .WithOptional(e => e.Persona1)
                .HasForeignKey(e => e.IdPersonaEmpleado);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Cliente)
                .WithOptional(e => e.Persona)
                .HasForeignKey(e => e.id_Persona);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.DetalleFactura)
                .WithOptional(e => e.Persona)
                .HasForeignKey(e => e.IdPersonaCliente);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.DetalleFactura1)
                .WithOptional(e => e.Persona1)
                .HasForeignKey(e => e.IdPersonaEmpleado);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Empleado)
                .WithOptional(e => e.Persona)
                .HasForeignKey(e => e.IdPersonaEmpleado);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Factura)
                .WithOptional(e => e.Persona)
                .HasForeignKey(e => e.IdPersonaCliente);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Factura1)
                .WithOptional(e => e.Persona1)
                .HasForeignKey(e => e.IdPersonaEmpleado);

            modelBuilder.Entity<Promocion>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Promocion>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Servicio>()
                .Property(e => e.Servicio1)
                .IsUnicode(false);

            modelBuilder.Entity<Servicio>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);
        }
    }
}
