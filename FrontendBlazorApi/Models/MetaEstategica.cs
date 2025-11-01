namespace FrontendBlazorApi.Models
{
    public class MetaEstrategica
    {
        public int? Id { get; set; }
        public int? IdObjetivo { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }

        public ObjetivoEstrategico? ObjetivoEstrategico { get; set; }
    }
}