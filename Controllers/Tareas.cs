using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaRicardoAlfaro.Data;
using PruebaTecnicaRicardoAlfaro.DTOs;
using PruebaTecnicaRicardoAlfaro.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaTecnicaRicardoAlfaro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tareas : ControllerBase
    {
        // Utilicé el contexto de la base de datos inyectado para gestionar la persistencia
        private readonly ApplicationDbContext _db;

        public Tareas(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET api/<Tareas>
        // Implementé el método GET para listar todas las tareas.
        // Utilicé '.Include(t => t.Usuario)' para realizar un "Eager Loading" y traer la información del usuario asociado, demostrando el manejo de relaciones en Entity Framework.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> Get()
        {
            // 1. Obtenemos las tareas incluyendo el usuario
            var tareas = await _db.Tareas
                .Include(t => t.Usuario)
                .ToListAsync();

            // 2. Mapeamos manualmente a nuestro DTO de salida
            var respuesta = tareas.Select(t => new TareaDetalleDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                FechaCreacion = t.FechaCreacion,
                UsuarioId = t.UsuarioId,
                Usuario = t.Usuario != null ? new UsuarioResumenDto
                {
                    Id = t.Usuario.Id,
                    Nombre = t.Usuario.Nombre
                } : null
            });

            return Ok(respuesta);
        }

        // GET api/<Tareas>/5
        // Obtengo una tarea específica filtrada por su ID, asegurándome de incluir los datos del usuario para mantener la integridad de la información consultada.
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> Get(int id)
        {
            var tarea = await _db.Tareas.Include(t => t.Usuario)
                                        .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null) return NotFound();

            return tarea;
        }

        // POST api/<Tareas>
        /* Para el método POST, agregué una validación de integridad referencial. 
         * Verifico que el 'UsuarioId' proporcionado realmente exista en la tabla Usuarios 
         * antes de intentar crear la tarea, evitando errores de clave foránea.
        */
        [HttpPost]
        public async Task<ActionResult<Tarea>> Post([FromBody] TareaCreateDto dto)
        {
            var usuarioExiste = await _db.Usuarios.AnyAsync(u => u.Id == dto.UsuarioId);
            if (!usuarioExiste)
            {
                return BadRequest($"No se puede crear la tarea: El UsuarioId {dto.UsuarioId} no existe.");
            }

            var nuevaTarea = new Tarea
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                UsuarioId = dto.UsuarioId,
                Estado = true,
                FechaCreacion = DateTime.Now
            };

            _db.Tareas.Add(nuevaTarea);
            await _db.SaveChangesAsync();

            // AGREGAR ESTA LÍNEA: 
            // Obliga a Entity Framework a traer la información del Usuario asociado desde la base de datos
            await _db.Entry(nuevaTarea).Reference(t => t.Usuario).LoadAsync();

            return CreatedAtAction(nameof(Get), new { id = nuevaTarea.Id }, nuevaTarea);
        }

        // PATCH api/Tareas/{id}/estado
        // Implementé este endpoint para gestionar el ciclo de vida de la tarea.
        // Según la lógica de negocio establecida: True = Activa/Pendiente, False = Terminada/Completada.
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] bool nuevoEstado)
        {
            var tarea = await _db.Tareas.FindAsync(id);
            if (tarea == null) return NotFound("No se encontró la tarea para actualizar su estado.");

            // Al asignar 'false', la tarea se marca técnicamente como terminada.
            tarea.Estado = nuevoEstado;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // PUT api/<Tareas>/5
        //Implementé el método PUT para permitir la actualización de campos específicos.
        //Al encontrar el registro original, actualizo sus propiedades con los datos del DTO.
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TareaCreateDto dto)
        {
            var tareaBd = await _db.Tareas.FindAsync(id);
            if (tareaBd == null) return NotFound();

            tareaBd.Titulo = dto.Titulo;
            tareaBd.Descripcion = dto.Descripcion;
            tareaBd.UsuarioId = dto.UsuarioId;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<Tareas>/5
        // Gestioné la eliminación de tareas validando primero la existencia del recurso.
        // El uso de Programación Asíncrona garantiza que la API sea altamente escalable.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarea = await _db.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();

            _db.Tareas.Remove(tarea);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
