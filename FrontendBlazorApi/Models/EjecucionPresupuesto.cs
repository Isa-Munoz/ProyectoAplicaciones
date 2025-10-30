namespace FrontendBlazorApi.Models
{
    public class EjecucionPresupuesto
    {
        public int? Id { get; set; }
        public Presupuesto? IdPresupuesto { get; set; }
        public int? Año { get; set; }
        public double MontoPlaneado { get; set; }
        public double MontoEjecutado { get; set; }
        public string? Observaciones { get; set; }
    }
}