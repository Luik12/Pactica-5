using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication2.Modelo;

namespace WebApplication2.Context
{
    public class LoginDBcontext : DbContext
    {
        public LoginDBcontext(DbContextOptions<LoginDBcontext> options) : base(options)
        {


        }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();
        }
    }
    
   

}
