using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using MyBackendApp.Models;
using MyBackendApp.Services;

namespace MyBackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseService _databaseService;

        public AuthController(IConfiguration configuration, DatabaseService databaseService)
        {
            _configuration = configuration;
            _databaseService = databaseService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userLogin)
        {

            var usuario = _databaseService.ObterUsuarioPorEmail(userLogin.EmailDTO);

            if (usuario == null) {return Unauthorized(new {Message = "Usuario não encontrado"});}
            if (usuario.SenhaHash != userLogin.SenhaDTO)
            {
                return Unauthorized(new { Message = "Credenciias invalidas"});
            }

            var token = GenerateJwtToken(usuario);

            return Ok(new { usuario, token });
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.NameIdentifier, usuario.Cpf)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}