using Microsoft.EntityFrameworkCore;
using PruebaTecnicaRicardoAlfaro.Models;

namespace PruebaTecnicaRicardoAlfaro.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Definí mis tablas como conjuntos de datos (DbSets) para Usuarios y Tareas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
    }
}
