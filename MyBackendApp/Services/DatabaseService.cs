using MyBackendApp.Models;
using MyBackendApp.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

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
        public Usuario ObterUsuarioPeloId(string id)
        {
            return _context.Usuarios.Find(id);
        }

        //Método para verificar usuários pelo CPF ou Email
        public Usuario ObterUsuarioPorCpfOuEmail(string cpf, string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Cpf == cpf || u.Email == email);
            
        }

        //Método para obter usuário pelo E-mail
        public async Task<Usuario> ObterUsuarioPorEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        }

        // Método para obter todos os usuários do banco de dados
        public List<Usuario> ObterUsuarioDoBanco()
        {
            return _context.Usuarios.ToList();

        }

        // Método para criar um usuário no banco
        public bool CriarUsuarioNoBanco(Usuario usuario)
        {
            if (usuario == null) return false;
            _context.Usuarios.Add(usuario);
            return _context.SaveChanges() > 0;
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
        public bool AtualizarUsuarioNoBanco(Usuario usuario)
        {
            if (usuario == null) return false;
            _context.Usuarios.Update(usuario);
            return _context.SaveChanges() > 0;
        }

        //Método para excluir um usuário no banco
        public bool ExcluirUsuarioDoBanco(string id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            return _context.SaveChanges() > 0;
        }


    }
}