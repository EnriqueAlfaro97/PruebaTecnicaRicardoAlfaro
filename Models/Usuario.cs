using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace PruebaTecnicaRicardoAlfaro.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public bool Estado { get; set; }

        // Relación: Un usuario tiene muchas tareas
        public List<Tarea> Tareas { get; set; } = new();
    }
}
