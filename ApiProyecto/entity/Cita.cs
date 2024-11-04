//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cita
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cita()
        {
            this.DetalleFactura = new HashSet<DetalleFactura>();
            this.Factura = new HashSet<Factura>();
        }
    
        public int IdCita { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> IdPersonaCliente { get; set; }
        public Nullable<int> IdEmpleado { get; set; }
        public Nullable<int> IdPersonaEmpleado { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<int> IdHorario { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual Horario Horario { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual Persona Persona1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura> Factura { get; set; }
    }
}