using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MyBackendApp.Services;
using Renci.SshNet;

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


//----------------------- Requisições do perfil -----------------------


        //GET: api/perfil/me
        [HttpGet("me")]
        public async Task<IActionResult> ObterMeuPerfil()
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var perfilExistente = await _databaseService.ObterPerfilPeloId(usuarioId);

            if (perfilExistente == null)
                return NotFound("Perfil não encontrado");

            return Ok(perfilExistente);
        }

        //PUT: api/perfil/me
        [HttpPut("me")]
        public async Task<IActionResult> AtualizarPerfil(
            [FromForm] IFormFile? fotoPerfil,
            [FromForm] string? status,
            [FromForm] string? sobre)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var perfilExistente = await _databaseService.ObterPerfilPeloId(usuarioId);

            if (perfilExistente == null)
                return NotFound("Perfil não encontrado");

            if (!string.IsNullOrEmpty(status)) perfilExistente.Status = status;
            if (!string.IsNullOrEmpty(sobre)) perfilExistente.Sobre = sobre;

            if (fotoPerfil != null)
            {
                var fotoPerfilUrl = await SalvarArquivoNoServidor(fotoPerfil, "FotosPerfil");
                perfilExistente.FotoPerfil = fotoPerfilUrl;
            }

            bool resultado = await _databaseService.AtualizarPerfilNoBanco(perfilExistente);

            if (resultado)
                return Ok("Perfil atualizado com sucesso");

            return BadRequest("Erro ao atualizar perfil");
        }


//----------------------- Métodos do perfil -----------------------


        //Método para salvar arquivos no servidor
        public async Task<string> SalvarArquivoNoServidor(IFormFile arquivo, string pasta)
        {
            string servidor = "192.168.1.11";
            string usuario = "vboxuser";
            string senha = "changeme";
            string pastaRemota = $"/var/www/html/imagens/{pasta}/";

            string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(arquivo.FileName);
            string caminhoRemoto = pastaRemota + nomeArquivo;

            using (var stream = new MemoryStream())
            {
                await arquivo.CopyToAsync(stream);
                stream.Position = 0;

                using (var sftp = new SftpClient(servidor, usuario, senha))
                {
                    sftp.Connect();
                    if (!sftp.Exists(pastaRemota))
                    {
                        sftp.CreateDirectory(pastaRemota);
                    }
                    sftp.UploadFile(stream, caminhoRemoto);
                    sftp.Disconnect();
                }
            }

            return $"http://192.168.1.11/imagens/{pasta}/{nomeArquivo}";
        }
    }
}
