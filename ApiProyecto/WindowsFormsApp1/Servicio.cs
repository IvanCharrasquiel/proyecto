namespace WindowsFormsApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Servicio")]
    public partial class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdServicio { get; set; }

        [Column("Servicio")]
        [StringLength(50)]
        public string Servicio1 { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        public double? Precio { get; set; }

        public int? Minutos { get; set; }

        public int? IdCategoria { get; set; }
    }
}
