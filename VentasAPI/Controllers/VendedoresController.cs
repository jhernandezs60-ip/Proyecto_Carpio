using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentasAPI.Data;
using VentasAPI.DTOs;
using VentasAPI.Models;

namespace VentasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VendedoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VendedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerVendedores()
        {
            var vendedores = await _context.Vendedores.ToListAsync();

            return Ok(vendedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerVendedorPorId(int id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);

            if (vendedor == null)
            {
                return NotFound(new
                {
                    mensaje = "Vendedor no encontrado."
                });
            }

            return Ok(vendedor);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarVendedor([FromBody] VendedorRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    mensaje = "Debe enviar los datos del vendedor."
                });
            }

            if (string.IsNullOrWhiteSpace(request.Nombre))
            {
                return BadRequest(new
                {
                    mensaje = "Debe ingresar el nombre del vendedor."
                });
            }

            var nombreNormalizado = request.Nombre.Trim().ToLower();

            var existeVendedor = await _context.Vendedores
                .AnyAsync(v => v.Nombre.ToLower() == nombreNormalizado);

            if (existeVendedor)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe un vendedor registrado con ese nombre."
                });
            }

            var vendedor = new Vendedor
            {
                Nombre = request.Nombre.Trim()
            };

            _context.Vendedores.Add(vendedor);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Vendedor registrado correctamente",
                vendedor = vendedor
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVendedor(int id, [FromBody] VendedorRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    mensaje = "Debe enviar los datos del vendedor."
                });
            }

            if (string.IsNullOrWhiteSpace(request.Nombre))
            {
                return BadRequest(new
                {
                    mensaje = "Debe ingresar el nombre del vendedor."
                });
            }

            var vendedor = await _context.Vendedores.FindAsync(id);

            if (vendedor == null)
            {
                return NotFound(new
                {
                    mensaje = "Vendedor no encontrado."
                });
            }

            var nombreNormalizado = request.Nombre.Trim().ToLower();

            var existeOtroVendedor = await _context.Vendedores
                .AnyAsync(v => v.IdVendedor != id && v.Nombre.ToLower() == nombreNormalizado);

            if (existeOtroVendedor)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe otro vendedor registrado con ese nombre."
                });
            }

            vendedor.Nombre = request.Nombre.Trim();

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Vendedor actualizado correctamente",
                vendedor = vendedor
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVendedor(int id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);

            if (vendedor == null)
            {
                return NotFound(new
                {
                    mensaje = "Vendedor no encontrado."
                });
            }

            var tieneVentas = await _context.Ventas
                .AnyAsync(v => v.IdVendedor == id);

            if (tieneVentas)
            {
                return BadRequest(new
                {
                    mensaje = "No se puede eliminar el vendedor porque tiene ventas registradas."
                });
            }

            _context.Vendedores.Remove(vendedor);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Vendedor eliminado correctamente."
            });
        }
    }
}