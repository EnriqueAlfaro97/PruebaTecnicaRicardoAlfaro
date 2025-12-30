using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaRicardoAlfaro.DTOs
{
    public class UsuarioCreateDto
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
