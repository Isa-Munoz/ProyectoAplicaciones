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

// DTO para usuario con roles
public class UsuarioConRoles
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("roles")]
    public List<RolDto> Roles { get; set; } = new();
}

public class RolDto
{
    [JsonPropertyName("idrol")]
    public int IdRol { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = string.Empty;
}