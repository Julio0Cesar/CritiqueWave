using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyBackendApp.Models
{
    public class Posts
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Perfil")]
        public int PerfilId { get; set; }

        public string FotoPost { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPost { get; set; }
        public List<Likes> Likes { get; set; }

        [JsonIgnore] // Evita o loop na serialização
        public Perfil Perfil { get; set; }
    }
}
