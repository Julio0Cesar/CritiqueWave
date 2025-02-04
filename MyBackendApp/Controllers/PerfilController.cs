using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MyBackendApp.Data;
using MyBackendApp.Models;
using MyBackendApp.Services;

namespace MyBackendApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        // Configuração do serviço do banco
        private readonly IConfiguration _configuration;
        private readonly DatabaseService _databaseService;
        public PerfilController(IConfiguration configuration, DatabaseService databaseService)
        {
            _configuration = configuration;
            _databaseService = databaseService;
        }

        //GET: api/perfil/me
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> ObterMeuPerfil()
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var perfilExistente = _databaseService.ObterPerfilPeloId(usuarioId);

            if (perfilExistente == null)
                return NotFound("Perfil não encontrado");

            return Ok(perfilExistente);
        }

        //POST: api/perfil/me
        [HttpPost("me")]
        [Authorize]
        public async Task<IActionResult> AtualizarPerfil([FromBody] Perfil perfilAtualizado)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var perfilExistente = _databaseService.ObterPerfilPeloId(usuarioId);

            if (perfilExistente == null)
                return NotFound("Perfil não encontrado");

            _databaseService.AtualizarPerfilNoBnaco(perfilAtualizado);

            return Ok("Perfil atualizado com sucesso");
        }


    }


}