using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public DbSet<Pago> Pago { get; set; }
        public DbSet<Persona> OPersona { get; set; }
        public DbSet<Promocion> OPromocion { get; set; }
        public DbSet<Servicio> OServicio { get; set; }
        public DbSet<Sexo> OSexo { get; set; }

    }
}
