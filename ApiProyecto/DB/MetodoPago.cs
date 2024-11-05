using System.ComponentModel.DataAnnotations;

namespace DB
{
    public class MetodoPago
    {
        [Key]
        public int IdMetodoPago { get; set; }
        public string metodoPago { get; set; }
    }
}
