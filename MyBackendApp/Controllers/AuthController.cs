using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
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


//-------------------------- Autenticação do usuário --------------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            var usuario = await _databaseService.ObterUsuarioPorEmail(userLogin.EmailDTO);

            if (usuario == null)
                return Unauthorized(new { Message = "Usuário não encontrado" });

            if (usuario.SenhaHash != userLogin.SenhaDTO)
                return Unauthorized(new { Message = "Credenciais inválidas" });

            var token = GenerateJwtToken(usuario);

            return Ok(new { usuario, token });
        }


//-------------------------- Geração e validação do token --------------------------

        //Método para gerar token
        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim("sub", usuario.Id.ToString())
            };

            var keyBytes = Convert.FromBase64String(_configuration["Jwt:Key"]);
            var key = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(30),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine($"Token gerado: {tokenString}");

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // endpoint para validação do token
        [HttpGet("validate")]
        [Authorize]
        public IActionResult ValidateToken()
        {
             // Obtém as claims do usuário autenticado
             var claims = User.Claims.Select(c => new { c.Type, c.Value });
             return Ok(new
             {
                 Message = "Token válido",
                 Claims = claims
             });
        }

    }
}
