namespace FrontendBlazorApi.Models
{
    public class DistribucionPresupuesto
    {
        public int? Id { get; set; }
        public Presupuesto? IdPresupuestoPadre { get; set; }
        public Proyecto? IdProyectoHijo { get; set; }
        public double MontoAsignado { get; set; }
    }
}