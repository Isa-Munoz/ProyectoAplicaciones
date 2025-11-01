namespace FrontendBlazorApi.Models
{
    public class Proyecto_Producto
    {
        public int? IdProyecto { get; set; } 
        public int? IdProducto { get; set; }
        public DateTime? FechaAsociacion { get; set; }

        public Proyecto? Proyecto { get; set; }

        public Producto? Producto { get; set; }
            
    }
}