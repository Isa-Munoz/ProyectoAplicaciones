using System.Text.Json.Serialization;
namespace FrontendBlazorApi.Models
{
    public class Archivo
    {
        public int Id { get; set; }
        [JsonPropertyName("IdUsuario")]
        public int UsuarioId { get; set; }
        public string? Ruta { get; set; }
        public string? Nombre { get; set; }
        public DateTime Fecha { get; set; }

        public Usuario? Usuario { get; set; }
    }
}