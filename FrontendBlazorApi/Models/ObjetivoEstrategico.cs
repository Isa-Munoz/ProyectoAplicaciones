namespace FrontendBlazorApi.Models
{
    public class ObjetivoEstrategico
    {
        public int? Id { get; set; }
        public VariableEstrategica? IdVariable { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
    }
}