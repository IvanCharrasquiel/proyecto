using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Categoria
    {
        public int IdCategoria { get; set; }

        public string Categoria1 { get; set; }

        public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
    }
}
