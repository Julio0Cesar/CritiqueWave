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
            var usuarios = _databaseService.ObterUsuarioDoBanco(); // Agora, chama o método do service

            if (usuarios == null)
            {
                return StatusCode(500, "Erro ao obter usuários do banco de dados.");
            }

            return Ok(usuarios);
        }

        //GET: api/usuarios/me
        [HttpGet("me")]
        public async Task<IActionResult> ObterUsuarioPeloId()
        {
            var userId = int.Parse(User.FindFirst("sub")?.Value);
            var user = _databaseService.ObterUsuarioPeloId(userId.ToString());  
            if (user == null) 
                return NotFound("Usuário não encontrado");
            
            return Ok(new {
                user.Id,
                user.Nome
            });
        }




        //POST: api/usuarios
        [HttpPost("criar")]
        [AllowAnonymous]
        public IActionResult CriarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("Usuário não pode ser nulo");
            
            //if (usuario.DataNascimento == default(DateTime))
            //    return BadRequest("Data de nascimento inválida.");
            
            var usuarioExistente = _databaseService.ObterUsuarioPorCpfOuEmail(usuario.Cpf, usuario.Email);
            if(usuarioExistente != null)
                return BadRequest("Já esxite um usuário com o mesmo CPF ou e-mail");

            if (!_databaseService.CriarUsuarioNoBanco(usuario))
                return StatusCode(500, "Erro ao criar usuário no banco de dados.");
            
            
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