using System.ComponentModel.DataAnnotations;

namespace MyBackendApp.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        
        public string Nome {get;set;}

        public string Cpf {get;set;}
        
        public string Email {get; set;}
        
        public string SenhaHash {get; set;}
        
        public DateTime DataNascimento {get; set;}

        public string Role { get; set;} = "Usuario";
    }
}