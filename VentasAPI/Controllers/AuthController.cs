using Microsoft.AspNetCore.Mvc;
using VentasAPI.Data;
using VentasAPI.DTOs;
using VentasAPI.Services;

namespace VentasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(ApplicationDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("Debe enviar usuario y contraseña.");
            }

            var usuario = _context.Usuarios.FirstOrDefault(u =>
                u.NombreUsuario == request.Usuario &&
                u.Password == request.Password
            );

            if (usuario == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            var token = _tokenService.GenerarToken(usuario);

            return Ok(new
            {
                mensaje = "Login correcto",
                usuario = usuario.NombreUsuario,
                rol = usuario.Rol,
                token = token
            });
        }
    }
}