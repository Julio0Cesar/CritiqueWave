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

        //Método para obter usuário pelo ID
        public Usuario ObterUsuarioPeloId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        //Método para obter usuário pelo E-mail
        public async Task<Usuario> ObterUsuarioPorEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        }

        //Método para criar usuário no banco
        public async Task<bool> CriarUsuarioNoBanco(Usuario usuario)
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

        //Método para realizar uma consulta no banco
        public async Task<bool> TestarConexao()
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
      
        //Método para atualizar um usuário no banco
        public async Task<bool> AtualizarUsuarioNoBanco(Usuario usuario)
        {
            if (usuario == null) return false;

            _context.Usuarios.Update(usuario);

            int resultado = await _context.SaveChangesAsync();

            if (resultado <= 0) return false; 
            
            return resultado > 0;
        }

        //Método para excluir um usuário no banco
        public async Task<bool> ExcluirUsuarioNoBanco(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            return _context.SaveChanges() > 0;
        }

        //Método para obter perfil
        public Perfil ObterPerfilPeloId(int usuarioId)
        {
            return _context.Perfis.Find(usuarioId);
        }

        public async Task<bool> AtualizarPerfilNoBnaco(Perfil perfilAtualizado)
        {
            _context.Perfis.Update(perfilAtualizado);

            int resultado = await _context.SaveChangesAsync();

            if (resultado <= 0) return false; 
            
            return resultado > 0;

        }
    }
}