using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyBackendApp.Models
{
    public class Perfil
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public string? FotoPerfil { get; set; }
        public string? Sobre { get; set; }
        public string? Status { get; set; }

        [JsonIgnore] // Evita o loop na serialização
        public Usuario Usuario { get; set; }

        public Perfil()
        {
            FotoPerfil = "http://192.168.1.11/imagens/FotosPerfil/3ffddfa6-576c-462c-a4ba-644cc7c386d8.png";
            Sobre = "Estou começando a usar o app agora!";
            Status = "Novato";
        }
    }
}
