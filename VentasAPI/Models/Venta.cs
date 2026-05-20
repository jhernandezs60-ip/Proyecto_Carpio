using System.ComponentModel.DataAnnotations;

namespace VentasAPI.Models
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }

        public int IdVendedor { get; set; }
        public string NombreVendedor { get; set; } = string.Empty;
        public decimal MontoVenta { get; set; }
        public decimal PorcentajeComision { get; set; }
        public decimal Comision { get; set; }
        public DateTime Fecha { get; set; }
    }
}