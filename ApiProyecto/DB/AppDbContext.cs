using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cargo> OCargo { get; set; }
        public DbSet<Categoria> OCategoria { get; set; }
        public DbSet<Cita> OCita { get; set; }
        public DbSet<Cliente> OCliente { get; set; }
        public DbSet<Combos> OCombos { get; set; }
        public DbSet<DetalleFactura> ODetalleFactura { get; set; }
        public DbSet<Empleado> OEmpleado { get; set; }
        public DbSet<Estado> OEstado { get; set; }
        public DbSet<Factura> OFactura { get; set; }
        public DbSet<Horario> OHorario { get; set; }
        public DbSet<MetodoPago> OMetodoPago { get; set; }
        public DbSet<Pago> OPago { get; set; }
        public DbSet<Persona> OPersona { get; set; }
        public DbSet<Promocion> OPromocion { get; set; }
        public DbSet<Servicio> OServicio { get; set; }
        public DbSet<Sexo> OSexo { get; set; }

        // Tablas de unión de muchos a muchos
        public DbSet<CitaServicio> OCitaServicio { get; set; }
        public DbSet<ComboServicio> OComboServicio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>().ToTable("Cargo");
            modelBuilder.Entity<Categoria>().ToTable("Categoria");
            modelBuilder.Entity<Cita>().ToTable("Cita");
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Combos>().ToTable("Combos");
            modelBuilder.Entity<DetalleFactura>().ToTable("DetalleFactura");
            modelBuilder.Entity<Empleado>().ToTable("Empleado");
            modelBuilder.Entity<Estado>().ToTable("Estado");
            modelBuilder.Entity<Factura>().ToTable("Factura");
            modelBuilder.Entity<Horario>().ToTable("Horario");
            modelBuilder.Entity<MetodoPago>().ToTable("MetodoPago");
            modelBuilder.Entity<Pago>().ToTable("Pago");
            modelBuilder.Entity<Persona>().ToTable("Persona");
            modelBuilder.Entity<Promocion>().ToTable("Promocion");
            modelBuilder.Entity<Servicio>().ToTable("Servicio");
            modelBuilder.Entity<Sexo>().ToTable("Sexo");
            modelBuilder.Entity<CitaServicio>().ToTable("CitaServicio");
            modelBuilder.Entity<ComboServicio>().ToTable("ComboServicio");



            modelBuilder.Entity<Promocion>()
                       .Property(p => p.Descuento)
                       .HasPrecision(5, 2); // 5 dígitos en total, 2 de ellos después del punto decimal

            modelBuilder.Entity<Cita>()
                        .HasOne(c => c.PersonaEmpleado)
                        .WithMany()
                        .HasForeignKey(c => c.IdPersonaEmpleado)
                        .OnDelete(DeleteBehavior.Restrict); // Cambiado a DeleteBehavior.Restrict

            // Asegúrate de que todas las demás relaciones en las entidades relacionadas estén configuradas
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Cliente)
                .WithMany()
                .HasForeignKey(f => f.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Factura)
                .WithMany()
                .HasForeignKey(df => df.IdFactura)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Servicio)
                .WithMany()
                .HasForeignKey(df => df.IdServicio)
                .OnDelete(DeleteBehavior.Restrict);


            // Configuración de la relación de Cita con Sexo
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Sexo) // Asumiendo que tienes una propiedad de navegación en Cita para Sexo
                .WithMany() // Relación de uno a muchos
                .HasForeignKey(c => c.IdSexo) // La clave foránea en Cita
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            // También puedes aplicar esto a otras relaciones que puedan causar el mismo problema
            // Ejemplo con otra entidad
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.PersonaEmpleado)
                .WithMany()
                .HasForeignKey(c => c.IdPersonaEmpleado)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Empleado) // Asegúrate de que tienes una propiedad de navegación en Factura
                .WithMany() // Relación de uno a muchos
                .HasForeignKey(f => f.IdEmpleado) // La clave foránea en Factura
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            // También puedes configurar otras relaciones que puedan estar causando problemas
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.PersonaEmpleado)
                .WithMany()
                .HasForeignKey(c => c.IdPersonaEmpleado)
                .OnDelete(DeleteBehavior.NoAction);

            // Configurar relación de Factura con PersonaCliente
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.PersonaCliente) // Asegúrate de que tienes una propiedad de navegación en Factura
                .WithMany() // Relación de uno a muchos
                .HasForeignKey(f => f.IdPersonaCliente) // La clave foránea en Factura
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            // Configurar relación de Factura con Empleado
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Empleado) // Asegúrate de que tienes una propiedad de navegación en Factura
                .WithMany() // Relación de uno a muchos
                .HasForeignKey(f => f.IdEmpleado) // La clave foránea en Factura
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            // Configuración de otras entidades si es necesario
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.PersonaEmpleado)
                .WithMany()
                .HasForeignKey(c => c.IdPersonaEmpleado)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuración de la relación de Factura con PersonaCliente
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.PersonaCliente) // Asegúrate de que tienes la propiedad de navegación
                .WithMany() // Relación de uno a muchos
                .HasForeignKey(f => f.IdPersonaCliente) // Clave foránea en Factura
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            // Configuración de la relación de Factura con Empleado
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Empleado) // Asegúrate de que tienes la propiedad de navegación
                .WithMany() // Relación de uno a muchos
                .HasForeignKey(f => f.IdEmpleado) // Clave foránea en Factura
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            // Configuración de la relación de Factura con PersonaEmpleado
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.PersonaEmpleado) // Asegúrate de que tienes la propiedad de navegación
                .WithMany() // Relación de uno a muchos
                .HasForeignKey(f => f.IdPersonaEmpleado) // Clave foránea en Factura
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            // Configuración de la relación de DetalleFactura con Cliente
            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Cliente) // Asegúrate de que tienes la propiedad de navegación
                .WithMany() // Relación de uno a muchos
                .HasForeignKey(df => df.IdCliente) // Clave foránea en DetalleFactura
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<DetalleFactura>()
               .HasOne(df => df.Empleado) // Asegúrate de que tienes la propiedad de navegación
               .WithMany() // Relación de uno a muchos
               .HasForeignKey(df => df.IdEmpleado) // Clave foránea en DetalleFactura
               .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada


            // Configuración de la relación de DetalleFactura con Persona (cliente)
            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Cliente) // Asegúrate de que tienes la propiedad de navegación correcta
                .WithMany() // Relación de uno a muchos
                .HasForeignKey(df => df.IdPersonaCliente) // Clave foránea en DetalleFactura
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            // También revisa la relación de DetalleFactura con Empleado si no lo has hecho
            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Empleado)
                .WithMany()
                .HasForeignKey(df => df.IdEmpleado)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            // Relación entre DetalleFactura y Cliente
            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Cliente)
                .WithMany() // O sin el WithMany si no tienes navegación inversa
                .HasForeignKey(df => df.IdPersonaCliente)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            // Relación entre DetalleFactura y Empleado
            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Empleado)
                .WithMany()
                .HasForeignKey(df => df.IdEmpleado)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<DetalleFactura>()
       .HasOne(df => df.Factura)
       .WithMany() // Asumiendo que Factura tiene una colección de DetalleFactura
       .HasForeignKey(df => df.IdFactura)
       .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Cita)
                .WithMany() // Asumiendo que Cita tiene una colección de DetalleFactura
                .HasForeignKey(df => df.IdCita)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Cliente)
                .WithMany() // Asumiendo que Cliente tiene una colección de DetalleFactura
                .HasForeignKey(df => df.IdCliente)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.PersonaCliente)
                .WithMany() // Asumiendo que Persona tiene una colección de DetalleFactura
                .HasForeignKey(df => df.IdPersonaCliente)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Empleado)
                .WithMany() // Asumiendo que Empleado tiene una colección de DetalleFactura
                .HasForeignKey(df => df.IdEmpleado)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.PersonaEmpleado)
                .WithMany() // Asumiendo que Persona tiene una colección de DetalleFactura
                .HasForeignKey(df => df.IdPersonaEmpleado)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Servicio)
                .WithMany() // Asumiendo que Servicio tiene una colección de DetalleFactura
                .HasForeignKey(df => df.IdServicio)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<DetalleFactura>()
                .HasOne(df => df.Categoria)
                .WithMany() // Asumiendo que Categoria tiene una colección de DetalleFactura
                .HasForeignKey(df => df.IdCategoria)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            // Configuración para Pago
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Estado)
                .WithMany() // Asumiendo que Estado tiene una colección de Pagos
                .HasForeignKey(p => p.IdEstado)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Factura)
                .WithMany() // Asumiendo que Factura tiene una colección de Pagos
                .HasForeignKey(p => p.IdFactura)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Cita)
                .WithMany() // Asumiendo que Cita tiene una colección de Pagos
                .HasForeignKey(p => p.IdCita)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Cliente)
                .WithMany() // Asumiendo que Cliente tiene una colección de Pagos
                .HasForeignKey(p => p.IdCliente)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.PersonaCliente)
                .WithMany() // Asumiendo que Persona tiene una colección de Pagos
                .HasForeignKey(p => p.IdPersonaCliente)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Empleado)
                .WithMany() // Asumiendo que Empleado tiene una colección de Pagos
                .HasForeignKey(p => p.IdEmpleado)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.PersonaEmpleado)
                .WithMany() // Asumiendo que Persona tiene una colección de Pagos
                .HasForeignKey(p => p.IdPersonaEmpleado)
                .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada







        }


    }
}
