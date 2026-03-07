using Microsoft.EntityFrameworkCore;
using WebApplication2.Modelo;

namespace WebApplication2.Context
{
    public class LoginDBcontext : DbContext
    {
        public LoginDBcontext(DbContextOptions<LoginDBcontext> options) : base(options)
        {
        }

        public DbSet<Usuario1> Usuarios1 { get; set; }
    }
}