using MyBackendApp.Models;
using MyBackendApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MyBackendApp.Services
{
    public class DatabaseService
    {
        //_connectionString acessa ao appsettings.json para saber as informações do banco
        private readonly ApplicationDbContext _context;

        //Construtor que recebe a string de conexão
        public DatabaseService(ApplicationDbContext context){
            _context = context;
        }


//------------------------ Consultas do usuário ------------------------

        //Método para obter usuário pelo ID
        public virtual async Task<Usuario> ObterUsuarioPeloId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        //Método para obter usuário pelo E-mail
        public virtual async Task<Usuario> ObterUsuarioPorEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        }

        //Método para criar usuário no banco
        public virtual async Task<bool> CriarUsuarioNoBanco(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            int resultado = await _context.SaveChangesAsync();

            if (resultado <= 0) return false;

            var perfil = new Perfil
            {
                UsuarioId = usuario.Id,
                FotoPerfil = null,
                Sobre = null,
                Status = null,
                CapaPerfil = null
            };

            _context.Perfis.Add(perfil);
            resultado = await _context.SaveChangesAsync();

            return resultado > 0; // Retorna true se o perfil foi adicionado com sucesso
        }

        //Método para atualizar um usuário no banco
        public virtual async Task<bool> AtualizarUsuarioNoBanco(Usuario usuario)
        {
            if (usuario == null) return false;

            _context.Usuarios.Update(usuario);

            int resultado = await _context.SaveChangesAsync();

            if (resultado <= 0) return false;

            return resultado > 0;
        }

        //Método para excluir um usuário no banco
        public virtual async Task<bool> ExcluirUsuarioNoBanco(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            int resultado = await _context.SaveChangesAsync();

            return resultado > 0;
        }


//------------------------ Consultas do perfil ------------------------


        //Método para obter perfil
        public virtual async Task<Perfil> ObterPerfilPeloId(int usuarioId)
        {
            return await _context.Perfis.FindAsync(usuarioId);
        }

        //Método para atualizar infomrações do perfil
        public virtual async Task<bool> AtualizarPerfilNoBanco(Perfil perfilAtualizado)
        {
            _context.Perfis.Update(perfilAtualizado);

            int resultado = await _context.SaveChangesAsync();

            if (resultado <= 0) return false;

            return true;

        }

        //Método para salvar arquivos no servidor
        public async Task<string> SalvarArquivo(IFormFile arquivo, string pasta)
        {
            // Diretório de armazenamento das imagens
            var diretórioDeArmazenamento = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", pasta);

            // Cria a pasta caso ela não exista
            if (!Directory.Exists(diretórioDeArmazenamento))
            {
                Directory.CreateDirectory(diretórioDeArmazenamento);
            }

            // Gera um nome único para o arquivo
            var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(arquivo.FileName);
            var caminhoArquivo = Path.Combine(diretórioDeArmazenamento, nomeArquivo);

            // Salva o arquivo fisicamente
            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            // Retorna o caminho público do arquivo wwwroot/uploads/FotosPerfil ou CapaPerfil
            return $"/uploads/{pasta}/{nomeArquivo}";
        }

        //Método para acessar o método SalvarArquivos fora do Service
        public virtual async Task<string> SalvarArquivoExterno(IFormFile arquivo, string pasta)
        {
            return await SalvarArquivo(arquivo, pasta);
        }

//------------------------ Consultas adicionais no banco ------------------------


        //Método para realizar teste consulta no banco
        public virtual async Task<bool> TestarConexao()
        {
            //Abre a conexão com o banco de dados e imprime uma mensagem de sucesso ou erro
            try
            {
                return await _context.Database.CanConnectAsync();
            }
            catch (Exception)
            {
                return false;

            }
        }
    }

}
