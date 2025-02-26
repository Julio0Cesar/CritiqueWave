using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyBackendApp.Models
{
    public class Likes
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Perfil")]
        public int PerfilId { get; set; }
        [ForeignKey("Posts")]
        public int PostsId { get; set; }

        [JsonIgnore] // Evita o loop na serialização
        public Perfil Perfil { get; set; }
        [JsonIgnore] // Evita o loop na serialização
        public Posts Post { get; set; }
    }
}
