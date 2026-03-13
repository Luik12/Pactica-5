using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Context;
using WebApplication2.Modelo;

namespace WebApplication2.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly LoginDBcontext _context;

        public ProductosController(LoginDBcontext context)
        {
            _context = context;
        }

        // GET todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos
                .Include(p => p.Proveedor)
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        // GET por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            return producto;
        }

        [HttpPost]
        public async Task<ActionResult> PostProducto(Producto producto)
        {
            var proveedorExiste = await _context.Proveedores.AnyAsync(p => p.Id == producto.IdProveedor);
            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == producto.IdCategoria);

            if (!proveedorExiste || !categoriaExiste)
            {
                return BadRequest("El proveedor o la categoría no existen.");
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return Ok(producto);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
                return BadRequest();

            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("estadisticas")]
        public IActionResult GetEstadisticas()
        {
            var productoMayorPrecio = _context.Productos
                .OrderByDescending(p => p.Precio)
                .FirstOrDefault();

            var productoMenorPrecio = _context.Productos
                .OrderBy(p => p.Precio)
                .FirstOrDefault();

            var sumaPrecios = _context.Productos.Sum(p => p.Precio);

            var promedioPrecios = _context.Productos.Average(p => p.Precio);

            return Ok(new
            {
                ProductoMasCaro = productoMayorPrecio,
                ProductoMasBarato = productoMenorPrecio,
                SumaTotalPrecios = sumaPrecios,
                PrecioPromedio = promedioPrecios
            });


        }
        [HttpGet("categoria/{idCategoria}")]
        public IActionResult GetProductosPorCategoria(int idCategoria)
        {
            var productos = _context.Productos
                .Where(p => p.IdCategoria == idCategoria)
                .ToList();

            return Ok(productos);
        }

        [HttpGet("proveedor/{idProveedor}")]
        public IActionResult GetProductosPorProveedor(int idProveedor)
        {
            var productos = _context.Productos
                .Where(p => p.IdProveedor == idProveedor)
                .ToList();

            return Ok(productos);
        }

        [HttpGet("total")]
        public IActionResult GetTotalProductos()
        {
            var total = _context.Productos.Count();

            return Ok(new { TotalProductos = total });
        }


    }

}
