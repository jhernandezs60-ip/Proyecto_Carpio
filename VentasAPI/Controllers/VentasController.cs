using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentasAPI.Data;
using VentasAPI.DTOs;
using VentasAPI.Models;
using VentasAPI.Services;

namespace VentasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ComisionService _comisionService;

        public VentasController(ApplicationDbContext context, ComisionService comisionService)
        {
            _context = context;
            _comisionService = comisionService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarVenta([FromBody] VentaRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    mensaje = "Debe enviar los datos de la venta."
                });
            }

            if (request.IdVendedor <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "Debe ingresar un vendedor válido."
                });
            }

            if (request.MontoVenta <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "El monto de venta debe ser mayor a cero."
                });
            }

            var vendedor = await _context.Vendedores.FindAsync(request.IdVendedor);

            if (vendedor == null)
            {
                return NotFound(new
                {
                    mensaje = "El vendedor no existe."
                });
            }

            var porcentaje = _comisionService.ObtenerPorcentajeComision(request.MontoVenta);
            var comision = _comisionService.CalcularComision(request.MontoVenta);

            var venta = new Venta
            {
                IdVendedor = vendedor.IdVendedor,
                NombreVendedor = vendedor.Nombre,
                MontoVenta = request.MontoVenta,
                Fecha = DateTime.Now,
                PorcentajeComision = porcentaje,
                Comision = comision
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Venta registrada correctamente",
                venta = venta
            });
        }

        [HttpGet("resumen")]
        public async Task<IActionResult> ObtenerResumen()
        {
            var resumen = await _context.Ventas
                .OrderByDescending(v => v.Fecha)
                .Select(v => new
                {
                    v.IdVenta,
                    v.IdVendedor,
                    v.NombreVendedor,
                    v.MontoVenta,
                    PorcentajeComision = v.PorcentajeComision * 100,
                    v.Comision,
                    v.Fecha
                })
                .ToListAsync();

            return Ok(resumen);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerVentaPorId(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);

            if (venta == null)
            {
                return NotFound(new
                {
                    mensaje = "Venta no encontrada."
                });
            }

            return Ok(venta);
        }

        [HttpGet("vendedor/{idVendedor}")]
        public async Task<IActionResult> ObtenerVentasPorVendedor(int idVendedor)
        {
            var vendedor = await _context.Vendedores.FindAsync(idVendedor);

            if (vendedor == null)
            {
                return NotFound(new
                {
                    mensaje = "El vendedor no existe."
                });
            }

            var ventas = await _context.Ventas
                .Where(v => v.IdVendedor == idVendedor)
                .OrderByDescending(v => v.Fecha)
                .Select(v => new
                {
                    v.IdVenta,
                    v.IdVendedor,
                    v.NombreVendedor,
                    v.MontoVenta,
                    PorcentajeComision = v.PorcentajeComision * 100,
                    v.Comision,
                    v.Fecha
                })
                .ToListAsync();

            return Ok(new
            {
                vendedor = vendedor.Nombre,
                totalVentas = ventas.Sum(v => v.MontoVenta),
                totalComision = ventas.Sum(v => v.Comision),
                cantidadVentas = ventas.Count,
                ventas = ventas
            });
        }

        [HttpGet("mejor-vendedor")]
        public async Task<IActionResult> ObtenerMejorVendedor()
        {
            var ventas = await _context.Ventas.ToListAsync();

            if (ventas.Count == 0)
            {
                return BadRequest(new
                {
                    mensaje = "No hay ventas registradas."
                });
            }

            var mejorVendedor = ventas
                .GroupBy(v => v.NombreVendedor)
                .Select(g => new
                {
                    Vendedor = g.Key,
                    TotalVentas = g.Sum(x => x.MontoVenta),
                    TotalComision = g.Sum(x => x.Comision),
                    CantidadVentas = g.Count()
                })
                .OrderByDescending(x => x.TotalVentas)
                .FirstOrDefault();

            return Ok(mejorVendedor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVenta(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);

            if (venta == null)
            {
                return NotFound(new
                {
                    mensaje = "Venta no encontrada."
                });
            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Venta eliminada correctamente."
            });
        }
    }
}