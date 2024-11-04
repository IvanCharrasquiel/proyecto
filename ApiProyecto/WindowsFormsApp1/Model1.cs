using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WindowsFormsApp1
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Servicio> Servicio { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servicio>()
                .Property(e => e.Servicio1)
                .IsUnicode(false);

            modelBuilder.Entity<Servicio>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);
        }
    }
}
