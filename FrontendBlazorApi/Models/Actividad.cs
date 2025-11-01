namespace FrontendBlazorApi.Models
{
    public class Actividad
    {
        public int Id { get; set; }
        public int? IdEntregable { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinPrevista { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public string? Prioridad { get; set; }
        public int? PorcentajeAvance { get; set; }
        public Entregable? Entregable { get; set; }
    }
}