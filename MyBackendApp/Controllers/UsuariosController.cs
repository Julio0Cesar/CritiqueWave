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

        //GET: api/usuarios/me
        [HttpGet("me")]
        public async Task<IActionResult> ObterUsuario()
        {
            var userId = int.Parse(User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value);
            var usuarioExistente = _databaseService.ObterUsuarioPeloId(userId);
            
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

            _databaseService.CriarUsuarioNoBanco(usuario);

            return Ok(new { Message = "Usuário criado com sucesso"});
        }

        //PUT: api/usuarios/atualizar-usuario
        [HttpPut("atualizar-usuario")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] Usuario usuario)
        {
            var usuarioExistente = _databaseService.ObterUsuarioPeloId(usuario.Id);

            if (usuarioExistente == null)
                return NotFound("Usuário não encontrado");

            _databaseService.AtualizarUsuarioNoBanco(usuario);

            return Ok("Usuário atualizado com sucesso!");
        }
    
        //DELETE: api/usuarios/excluir-usuario
        [HttpDelete("excluir-usuario/{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            var usuarioExistente = _databaseService.ObterUsuarioPeloId(id);

            if (usuarioExistente == null)
                return NotFound("Usuário não encontrado");

            _databaseService.ExcluirUsuarioNoBanco(id);

            return Ok("Usuário excluído com sucesso!");
        }

    }
}