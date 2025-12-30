using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaRicardoAlfaro.DTOs
{
    public class TareaCreateDto
    {
        [Required]
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int UsuarioId { get; set; } 
    }
}
