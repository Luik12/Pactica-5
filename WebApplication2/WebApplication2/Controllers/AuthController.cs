using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication2.Context;
using WebApplication2.Modelo;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LoginDBcontext _context;
        private readonly IConfiguration _configuration;

        public AuthController(LoginDBcontext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // REFRESH TOKEN (requiere token)
        [HttpPost("refresh")]
        public IActionResult RefreshToken()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (username == null)
                return Unauthorized();

            var usuario = _context.Usuarios1.FirstOrDefault(x => x.Username == username);

            if (usuario == null)
                return Unauthorized();

            var token = GenerarToken(usuario);

            return Ok(new { token });
        }

        // LOGIN (NO requiere token)
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLogin login)
        {
            string passwordHash = Sha256(login.Password);

            var usuario = _context.Usuarios1
                .FirstOrDefault(x => x.Username == login.Username && x.Password == passwordHash);

            if (usuario == null)
            {
                return Unauthorized("Credenciales incorrectas");
            }

            var token = GenerarToken(usuario);

            return Ok(new { token });
        }

        private string GenerarToken(Usuario1 usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim("Id", usuario.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

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