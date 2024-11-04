using Microsoft.EntityFrameworkCore;
namespace DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
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
        public virtual DbSet<CitaServicio> CitaServicio { get; set; }
        public virtual DbSet<ComboServicio> ComboServicios { get; set; }
        public virtual DbSet<MetodoPago> MetodoPagos { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
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
