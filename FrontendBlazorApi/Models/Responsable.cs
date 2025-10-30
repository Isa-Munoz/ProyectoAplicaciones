using System.Text.Json.Serialization;
namespace FrontendBlazorApi.Models
{
    public class Responsable
    {
        public int Id { get; set; }
        [JsonPropertyName("IdTipoResponsable")]
        public int TipoResponsableId { get; set; }
        public TipoResponsable? TipoResponsable { get; set; }
        [JsonPropertyName("IdUsuario")]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public string? Nombre { get; set; }
    }
}