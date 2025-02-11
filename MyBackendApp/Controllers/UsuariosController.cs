/*
Header para teste:
Content-Type  application/json

body para teste: raw
descrição do body:

{
  "Nome": "Teste",
  "Email": "email@example.com",
  "SenhaHash": "123456"
}

*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBackendApp.Models;
using MyBackendApp.Services;

namespace MyBackendApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        // Configuração do serviço do banco
        private readonly IConfiguration _configuration;
        private readonly DatabaseService _databaseService;
        public UsuariosController(IConfiguration configuration, DatabaseService databaseService)
        {
            _configuration = configuration;
            _databaseService = databaseService;
        }

//----------------------- CRUD do uduário -----------------------

        //GET: api/usuarios/me
        [HttpGet("me")]
        public async Task<IActionResult> ObterUsuario()
        {
            var userId = int.Parse(User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value);
            var usuarioExistente = await _databaseService.ObterUsuarioPeloId(userId);

            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado");
            }

            return Ok(usuarioExistente);
        }

        //POST: api/usuarios
        [HttpPost("criar")]
        [AllowAnonymous]
        public async Task<IActionResult> CriarUsuario([FromBody] Usuario usuario)
        {
            var email = usuario.Email;
            var usuarioExistente = await _databaseService.ObterUsuarioPorEmail(email);

            if (usuarioExistente != null)
                return BadRequest("Já existe um usuário com o mesmo e-mail");

            var sucesso = await _databaseService.CriarUsuarioNoBanco(usuario);
            
            if (!sucesso)
                return StatusCode(500, "Erro ao criar usuário no banco de dados");

            return Ok(new { Message = "Usuário criado com sucesso"});
        }

        //PUT: api/usuarios/atualizar-usuario
        [HttpPut("atualizar-usuario")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] Usuario usuarioAtualizado)
        {
            var userId = int.Parse(User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value);
            var usuarioExistente = await _databaseService.ObterUsuarioPeloId(userId);

            if (usuarioExistente == null)
                return NotFound("Usuário não encontrado");

            usuarioExistente.Nome = usuarioAtualizado.Nome;
            usuarioExistente.Username = usuarioAtualizado.Username;
            usuarioExistente.Email = usuarioAtualizado.Email;
            usuarioExistente.SenhaHash = usuarioAtualizado.SenhaHash;

            await _databaseService.AtualizarUsuarioNoBanco(usuarioExistente);

            return Ok("Usuário atualizado com sucesso!");
        }

        //DELETE: api/usuarios/excluir-usuario
        [HttpDelete("excluir-usuario")]
        public async Task<IActionResult> ExcluirUsuario()
        {
            var userId = int.Parse(User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value);
            var usuarioExistente = await _databaseService.ObterUsuarioPeloId(userId);

            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado");
            }

            await _databaseService.ExcluirUsuarioNoBanco(userId);

            return Ok("Usuário excluído com sucesso!");
        }

    }
}
