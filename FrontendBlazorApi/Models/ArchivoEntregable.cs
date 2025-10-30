using System.Text.Json.Serialization;
namespace FrontendBlazorApi.Models
{
    public class ArchivoEntregable
    {
        [JsonPropertyName("IdArchivo")]
        public int ArchivoId { get; set; }
        public Archivo? Archivo { get; set; }

        [JsonPropertyName("IdEntregable")]
        public int EntregableId { get; set; }
        public Entregable? Entregable { get; set; }
    }
}