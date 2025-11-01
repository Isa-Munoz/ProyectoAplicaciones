namespace FrontendBlazorApi.Models
{
    public class Estado_Proyecto
    {
        public int? IdProyecto { get; set; }
        public int? IdEstado { get; set; }
        public Proyecto? Proyecto { get; set; }
        public Estado? Estado { get; set; }
    
    }
}