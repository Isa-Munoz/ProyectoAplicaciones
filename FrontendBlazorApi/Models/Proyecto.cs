using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FrontendBlazorApi.Models
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }
        public int? IdProyectoPadre { get; set; }
        public int? IdResponsable { get; set; }
        public int? IdTipoProyecto { get; set; }
        public string? Codigo { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinPrevista { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public string? RutaLogo { get; set; }
        public Proyecto? ProyectoPadre { get; set; }
        public Responsable? Responsable { get; set; }
        public TipoProyecto? TipoProyecto { get; set; }
    }
}