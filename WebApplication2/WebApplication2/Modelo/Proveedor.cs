using System.Text.Json.Serialization;

namespace WebApplication2.Modelo
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contacto { get; set; }
        [JsonIgnore]
        public ICollection<Producto?> Productos { get; set; } = new List<Producto>();
    }
}
