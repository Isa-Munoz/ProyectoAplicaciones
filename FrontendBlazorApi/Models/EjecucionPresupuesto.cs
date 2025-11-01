namespace FrontendBlazorApi.Models
{
    public class EjecucionPresupuesto
    {
        public int? Id { get; set; }
        public int? IdPresupuesto { get; set; }
        public int? Anio { get; set; }
        public double? MontoPlaneado { get; set; }
        public double? MontoEjecutado { get; set; }
        public string? Observaciones { get; set; }
        public Presupuesto? Presupuesto { get; set; }
    }
}