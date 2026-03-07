using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Modelo
{
    public class UsuarioLogin
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}