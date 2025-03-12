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
            return await _context.Usuarios
            .Include(u => u.Perfil)
            .FirstOrDefaultAsync(u => u.Id == id);
        }

        //Método para obter usuário pelo E-mail
        public virtual async Task<Usuario> ObterUsuarioPorEmail(string email)
        {
            return await _context.Usuarios
            .Include(u => u.Perfil)
            .FirstOrDefaultAsync(u => u.Email == email);

        }

        //Método para criar usuário no banco
        public virtual async Task<bool> CriarUsuarioNoBanco(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            int resultado = await _context.SaveChangesAsync();

            if (resultado <= 0) return false;

            var perfil = new Perfil
            {
                UsuarioId = usuario.Id
            };

            _context.Perfis.Add(perfil);
            resultado = await _context.SaveChangesAsync();

            return resultado > 0; // Retorna true se o perfil foi adicionado com sucesso
        }

        //Método para atualizar um usuário no banco
        public virtual async Task<bool> AtualizarUsuarioNoBanco(Usuario usuario)
        {
            if (usuario == null) return false;

            _context.Entry(usuario).State = EntityState.Modified;

            int resultado = await _context.SaveChangesAsync();

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

using (var db = new SqlConnection("YourConnectionStringHere"))
{
    var users = db.Query<User>("SELECT Id, Name FROM Users");
    foreach (var u in users) Console.WriteLine($"{u.Id} - {u.Name}");
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
