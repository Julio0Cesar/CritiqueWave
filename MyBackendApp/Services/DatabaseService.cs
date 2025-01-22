using MySql.Data.MySqlClient;
using MyBackendApp.Models;

namespace MyBackendApp.Services
{
    public class DatabaseService
    {
        //_connectionString acessa ao appsettings.json para saber as informações do banco
        private readonly string _connectionString;

        //Construtor que recebe a string de conexão
        public DatabaseService(string connectionString){
            _connectionString = connectionString;
        }

        //Método para obter usuário pelo E-mail
        public Usuario ObterUsuarioPorEmail(string email)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                var query = "SELECT * FROM usuario WHERE email = @email";

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@email", email);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var usuario = new Usuario
                    {
                        Nome = reader["nome"].ToString(),
                        Cpf = reader["cpf"].ToString(),
                        Email = reader["email"].ToString(),
                        SenhaHash = reader["senha"].ToString(),
                        DataNascimento = DateTime.Parse(reader["data_nascimento"].ToString())
                    };
                    return usuario;
                }

                return null;
            }
            catch (System.Exception)
            {
                
                return null;
            }
        }

        // Método para obter todos os usuários do banco de dados
        public List<Usuario> ObterUsuariosDoBanco()
        {
            try
            {
                var usuarios = new List<Usuario>();

                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                var query = "SELECT * FROM usuario";

                using var cmd = new MySqlCommand(query, connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var usuario = new Usuario
                    {
                        Nome = reader["nome"].ToString(),
                        Cpf = reader["cpf"].ToString(),
                        Email = reader["email"].ToString(),
                        SenhaHash = reader["senha"].ToString(),
                        DataNascimento = DateTime.Parse(reader["data_nascimento"].ToString())
                    };
                    usuarios.Add(usuario);
                }

                return usuarios;
            }
            catch (Exception)
            {
                return null; // Ou lançar um erro específico se desejar
            }
        }

        // Método para criar um usuário no banco
        public bool CriarUsuarioNoBanco(Usuario usuario)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                var query = "INSERT INTO usuario (nome, cpf, email, senha, data_nascimento) VALUES (@nome, @cpf, @email, @senha, @data_nascimento)";

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@cpf", usuario.Cpf);
                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@senha", usuario.SenhaHash);
                cmd.Parameters.AddWithValue("@data_nascimento", usuario.DataNascimento);

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Método para realizar uma consulta no banco
        public void TestarConexao(){
            //Abre a conexão com o banco de dados e imprime uma mensagem de sucesso ou erro
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexão estabelecida com sucesso");
                }
                catch (Exception)
                {
                    Console.WriteLine("Erro aoi conectar ao banco");
                    
                }
            }
        }
      
        //Método para atualizar um usuário no banco
        public bool AtualizarUsuarioNoBanco(Usuario usuario)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();
                var query = "UPDATE usuario SET nome = @nome, email = @email, senha = @senha, data_nascimento = @data_nascimento WHERE cpf = @cpf";

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@cpf", usuario.Cpf);
                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@senha", usuario.SenhaHash);
                cmd.Parameters.AddWithValue("@data_nascimento", usuario.DataNascimento);

                var rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        //Método para excluir um usuário no banco
        public bool ExcluirUsuarioDoBanco(string cpf)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                var query = "DELETE FROM usuario WHERE cpf = @cpf";

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@cpf", cpf);

                var rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
            catch (Exception)
            {
                
                return false;
            }
        }


    }
}