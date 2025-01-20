namespace MyBackendApp.Models
using System.ComponentModel.DataAnnotations;

{
    public class Usuario
    {
        //modelo de dados do usuário no login
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome {get;set;}

        [Required(ErrorMessage = "O cpf é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 dígitos.")]
        public string Cpf {get;set;}
        
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email {get; set;}
        
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres.")]
        public string Senha {get; set;}
        
        [Required(ErrorMessage = "a data de nascimento é obrigatória.")]
        public DateTime DataNascimento {get; set;}
    }
}