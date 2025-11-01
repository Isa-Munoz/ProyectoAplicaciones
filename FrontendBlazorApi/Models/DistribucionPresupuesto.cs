using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FrontendBlazorApi.Models
{
    public class DistribucionPresupuesto
{
    [Key]
    public int Id { get; set; }
    public int? IdPresupuestoPadre { get; set; }
    public int? IdProyectoHijo { get; set; }
    
    [ForeignKey(nameof(IdPresupuestoPadre))]
    public Presupuesto? PresupuestoPadre { get; set; }
    [ForeignKey(nameof(IdProyectoHijo))]
    public Proyecto? ProyectoHijo { get; set; }
    public double? MontoAsignado { get; set; }
}
}