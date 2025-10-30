using FrontendBlazorApi.Models;

namespace FrontendBlazorApi
{
    public class Producto
    {
        public int Id { get; set; }
        public TipoProducto? IdTipoProducto { get; set; }
        public string? Codigo { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinPrevista { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public string? RutaLogo { get; set; }
    }
}