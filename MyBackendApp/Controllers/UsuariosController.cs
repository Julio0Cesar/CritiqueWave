using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBackendApp.Models;
using MyBackendApp.Services;
using MyBackendApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MyBackendApp.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: api/usuarios
        [HttpGet]
        public async Task<IActionResult> ObterUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }


        //GET: api/usuarios/me
        [HttpGet("me")]
        public async Task<IActionResult> ObterUsuarioPeloId()
        {
            var userId = int.Parse(User.FindFirst("sub")?.Value);
            var user = await _context.Usuarios.FindAsync(userId);
            if (user == null) 
                return NotFound("Usuário não encontrado");
            return Ok(user);
        }

        //POST: api/usuarios
        [HttpPost("criar")]
        [AllowAnonymous]
        public async Task<IActionResult> CriarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("Usuário não pode ser nulo");

            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Cpf == usuario.Cpf || u.Email == usuario.Email);

            if (usuarioExistente != null)
                return BadRequest("Já existe um usuário com o mesmo CPF ou e-mail");

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuário criado com sucesso", Usuario = usuario });
        }

        //PUT: api/usuarios/atualizar-usuario
        [HttpPut("atualizar-usuario")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(usuario.Id);

            if (usuarioExistente == null)
                return NotFound("Usuário não encontrado");

            usuarioExistente.Nome = usuario.Nome; // Exemplo de atualização de um campo
            _context.Usuarios.Update(usuarioExistente);
            await _context.SaveChangesAsync();

            return Ok("Usuário atualizado com sucesso!");
        }
    
        //DELETE: api/usuarios/excluir-usuario
        [HttpDelete("excluir-usuario/{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return NotFound("Usuário não encontrado");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuário excluído com sucesso!");
        }

    }
}