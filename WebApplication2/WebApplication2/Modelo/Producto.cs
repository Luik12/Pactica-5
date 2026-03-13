namespace WebApplication2.Modelo
{
    using System.Text.Json.Serialization;

    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public int IdProveedor { get; set; }

        [JsonIgnore]
        public Proveedor? Proveedor { get; set; }

        public int IdCategoria { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }

    }
}
