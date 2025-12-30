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
    public class Usuarios : ControllerBase
    {
        // Definí el contexto de base de datos como privado y de solo lectura para mayor seguridad
        private readonly ApplicationDbContext _db;

        // Implementé la inyección de dependencias a través del constructor para acceder a la base de datos
        public Usuarios(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET api/<Usuarios>
        // Obtengo el listado de usuarios de forma asíncrona para no bloquear el hilo de ejecución.
        // Utilicé .Include(u => u.Tareas) para cargar las tareas relacionadas de cada usuario (Eager Loading)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            return await _db.Usuarios.ToListAsync();
        }

        // GET api/<Usuarios>/5
        // Busco un usuario específico por su ID único. Si no existe, devuelvo un error 404 personalizado
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await _db.Usuarios.Include(u => u.Tareas)
                                           .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null) return NotFound("Usuario no encontrado.");

            return usuario;
        }

        // Implementé este endpoint para listar tareas por usuario específico
        [HttpGet("ListaTareasUsuarios/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTareasPorUsuario(int usuarioId)
        {
            // Busco al usuario incluyendo su lista de tareas relacionada
            var usuarioConTareas = await _db.Usuarios
                .Include(u => u.Tareas)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            // Valido si el usuario existe para evitar devolver datos vacíos
            if (usuarioConTareas == null)
            {
                return NotFound($"No se encontró el usuario con ID {usuarioId}");
            }

            // Retorno un objeto anónimo con la estructura exacta que necesito: 
            // Datos del usuario en el encabezado y su lista de tareas abajo.
            return Ok(new
            {
                Id = usuarioConTareas.Id,
                Nombre = usuarioConTareas.Nombre,
                Email = usuarioConTareas.Email,
                Tareas = usuarioConTareas.Tareas.Select(t => new {
                    t.Id,
                    t.Titulo,
                    t.Descripcion,
                    t.Estado,
                    t.FechaCreacion
                })
            });
        }

        // POST api/<Usuarios>
        // Implementé el método POST utilizando un DTO para recibir solo los datos necesarios.
        // Realicé el mapeo manual del DTO hacia mi entidad de modelo 'Usuario' antes de guardar
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post([FromBody] UsuarioCreateDto dto)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Estado = true
            };

            _db.Usuarios.Add(nuevoUsuario);
            await _db.SaveChangesAsync();

            // Devuelvo un código 201 Created junto con la ruta para consultar el nuevo recurso
            return CreatedAtAction(nameof(Get), new { id = nuevoUsuario.Id }, nuevoUsuario);
        }

        // PUT api/<Usuarios>/5
        // Configuré el método PUT para actualizar usuarios existentes.
        // Primero verifico la existencia del registro en la base de datos antes de intentar modificarlo
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioCreateDto dto)
        {
            var usuarioBd = await _db.Usuarios.FindAsync(id);

            if (usuarioBd == null)
            {
                return NotFound($"No existe un usuario con el ID {id}");
            }

            // Actualizo las propiedades de la entidad rastreada por Entity Framework
            usuarioBd.Nombre = dto.Nombre;
            usuarioBd.Email = dto.Email;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejo errores de concurrencia para asegurar la integridad de los datos
                return BadRequest("Error de concurrencia al actualizar.");
            }

            return NoContent(); // Respuesta estándar 204 para una actualización exitosa sin contenido
        }

        // DELETE api/<Usuarios>/5
        // Implementé la eliminación física del registro buscando primero la entidad por su ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _db.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            _db.Usuarios.Remove(usuario);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
