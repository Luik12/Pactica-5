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
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Proveedor)
                .WithMany(p => p.Productos)
                .HasForeignKey(p => p.IdProveedor);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.IdCategoria);
        }
    } 
}
