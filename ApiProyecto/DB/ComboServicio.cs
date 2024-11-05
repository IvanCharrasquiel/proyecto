namespace DB
{
    public class ComboServicio
    {
        public int? IdCombo { get; set; }

        public int? IdServicio { get; set; }

        public virtual Combo? IdComboNavigation { get; set; }

        public virtual Servicio? IdServicioNavigation { get; set; }
    }
}
