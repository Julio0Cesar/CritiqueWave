using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyBackendApp.Models;

namespace MyBackendApp.Models
{
    public class Perfil
    {
        [Key]
        public int Id {get; set;}

        [ForeignKey("usuario")]
        public int UsuarioId {get; set;}

        public string? FotoPerfil {get; set;}

        public string? Sobre {get; set;}

        public string? Status {get; set;}

        public string? CapaPerfil {get; set;}

        public Usuario Usuario {get; set;}
    }
}