using System.ComponentModel.DataAnnotations;

namespace VentasAPI.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}