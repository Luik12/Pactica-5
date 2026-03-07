using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Context;
using WebApplication2.Modelo;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly LoginDBcontext _context;

        public UsuariosController(LoginDBcontext context)
        {
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario1>>> GetUsuarios()
        {
            return await _context.Usuarios1.ToListAsync();
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario1>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios1.FindAsync(id);

            if (usuario == null)
                return NotFound("Usuario no encontrado");

            return usuario;
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario1>> PostUsuario(Usuario1 usuario)
        {
            // Encriptar contraseña
            usuario.Password = Sha256(usuario.Password);

            _context.Usuarios1.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario1 usuario)
        {
            if (id != usuario.Id)
                return BadRequest("El ID no coincide.");

            var usuarioExistente = await _context.Usuarios1.FindAsync(id);

            if (usuarioExistente == null)
                return NotFound("Usuario no encontrado.");

            usuarioExistente.Username = usuario.Username;

            // Encriptar contraseña al actualizar
            usuarioExistente.Password = Sha256(usuario.Password);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios1.FindAsync(id);

            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            _context.Usuarios1.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 🔐 Método para encriptar con SHA256
        private string Sha256(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }
    }
}