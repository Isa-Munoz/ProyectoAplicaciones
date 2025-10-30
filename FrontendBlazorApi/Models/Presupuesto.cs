namespace FrontendBlazorApi.Models
{
    public class Presupuesto
    {
        public int? Id { get; set; }
        public Proyecto? IdProyecto { get; set; }
        public double? MontoSolicitado { get; set; }
        public string? Estado { get; set; }
        public double? MontoAprobado { get; set; }
        public DateTime? PeriodoAño { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaAprobacion { get; set; } 
        public string? Observaciones { get; set; }
    }
}