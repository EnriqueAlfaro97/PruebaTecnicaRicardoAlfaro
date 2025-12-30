namespace PruebaTecnicaRicardoAlfaro.DTOs
{
    public class TareaDetalleDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioId { get; set; }
        // Solo incluimos los datos básicos del usuario, sin su lista de tareas
        public UsuarioResumenDto? Usuario { get; set; }
    }

    public class UsuarioResumenDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
