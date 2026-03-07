using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Modelo
{
    public class Usuario1
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}