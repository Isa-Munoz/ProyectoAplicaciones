using System.Text.Json.Serialization;
namespace FrontendBlazorApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Contrasena { get; set; } 
        [JsonPropertyName("rutaavatar")]
        public string? RutaAvatar { get; set; } 
        public bool Activo { get; set; }
    }
}