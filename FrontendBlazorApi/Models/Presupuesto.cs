using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FrontendBlazorApi.Models
{
    public class Presupuesto
    {
        [Key]
        public int Id { get; set; }
        public int? IdProyecto { get; set; }
        public double? MontoSolicitado { get; set; }
        public string? Estado { get; set; }
        public double? MontoAprobado { get; set; }
        public DateTime? PeriodoAño { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string? Observaciones { get; set; }

        [ForeignKey(nameof(IdProyecto))]
        public Proyecto? Proyecto { get; set; }
    }
}