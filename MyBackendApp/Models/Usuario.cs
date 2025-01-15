namespace MyBackendApp.Models
{
    public class Usuario
    {
        //modelo de dados do usuário no login
        public string Nome {get;set;}
        public string Cpf {get;set;}
        public string Email {get; set;}
        public string Senha {get; set;}
        public DateTime DataNascimento {get; set;}
    }
}