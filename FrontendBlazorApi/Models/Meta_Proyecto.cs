namespace FrontendBlazorApi.Models
{
    public class Meta_Proyecto
    {
        public int? IdMeta { get; set; }
        public int? IdProyecta { get; set; }
        public DateTime? FechaAsociacion { get; set; }
        public MetaEstrategica? MetaEstrategica{ get; set; }
        public Proyecto? Proyecto { get; set; }
    }
}