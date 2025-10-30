using System.Text.Json.Serialization;
namespace FrontendBlazorApi.Models
{
    public class ResponsableEntregable
    {
        [JsonPropertyName("IdResponsable")]
        public int ResponsableId { get; set; }
        public Responsable? Responsable { get; set; }
        
        [JsonPropertyName("IdEntregable")]
        public int EntregableId { get; set; }
        public Entregable? Entregable { get; set; }
        public DateTime FechaAsociacion { get; set; }
    }
}