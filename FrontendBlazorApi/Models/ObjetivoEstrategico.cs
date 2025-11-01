namespace FrontendBlazorApi.Models
{
    public class ObjetivoEstrategico
    {
        public int? Id { get; set; }
        public int? IdVariable { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public VariableEstrategica? VariableEstrategica { get; set; }
    }
}