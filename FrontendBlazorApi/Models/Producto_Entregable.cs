namespace FrontendBlazorApi.Models
{
    public class Producto_Entregable
    {
        public int? IdProducto { get; set; }
        public int? IdEntregable { get; set; }
        public DateTime? FechaAsociacion { get; set; }
        public Producto? Producto { get; set; }
        public Entregable? Entregable { get; set; }
    }
}