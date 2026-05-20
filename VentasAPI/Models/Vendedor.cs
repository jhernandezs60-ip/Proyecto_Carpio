using System.ComponentModel.DataAnnotations;

namespace VentasAPI.Models
{
    public class Vendedor
    {
        [Key]
        public int IdVendedor { get; set; }

        public string Nombre { get; set; } = string.Empty;
    }
}