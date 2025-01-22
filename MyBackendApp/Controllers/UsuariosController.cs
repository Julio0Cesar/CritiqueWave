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
        private readonly DatabaseService _databaseService;

        public UsuariosController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        //GET: api/usuarios
        [HttpGet]
        public IActionResult ObterUsuarios()
        {
            var usuarios = _databaseService.ObterUsuariosDoBanco(); // Agora, chama o método do service

            if (usuarios == null)
            {
                return StatusCode(500, "Erro ao obter usuários do banco de dados.");
            }

            return Ok(usuarios);
        }

        //POST: api/usuarios
        [HttpPost]
        public IActionResult CriarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null){
                return BadRequest("Usuário não pode ser nulo");
            }

            bool usuarioCriado = _databaseService.CriarUsuarioNoBanco(usuario);

            if (!usuarioCriado){
                return StatusCode(500, "Erro ao criar usuário no banco de dados.");
            }   
            
            return Ok(new {Message = "Usuário criado com sucesso", Usuario = usuario});
        }

        //PUT: api/usuarios/atualizar-usuario
        [HttpPut("atualizar-usuario")]
        public IActionResult AtualizarUsuario([FromBody] Usuario usuario)
        {
            bool sucesso = _databaseService.AtualizarUsuarioNoBanco(usuario);

            if (sucesso)
            {
                return Ok("Usuário atualizado com sucesso!");
            }
            else
            {
                return StatusCode(500, "Erro ao atualizar o usuário.");
            }
        }
    
        //DELETE: api/usuarios/excluir-usuario
        [HttpDelete("excluir-usuario/{cpf}")]
        public IActionResult ExcluirUsuario(string cpf)
        {
            bool sucesso = _databaseService.ExcluirUsuarioDoBanco(cpf);

            if (sucesso)
            {
                return Ok("Usuário excluído com sucesso!");
            }
            else
            {
                return StatusCode(500, "Erro ao excluir o usuário.");
            }
        }

    }
}