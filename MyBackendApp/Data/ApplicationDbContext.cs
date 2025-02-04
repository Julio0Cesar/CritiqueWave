using Microsoft.EntityFrameworkCore;
using MyBackendApp.Models;

namespace MyBackendApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Configurações do banco
        private readonly IConfiguration _configuration; 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration; 
        }

        // tabelas do banco
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        //modelando o banco com dado nas models
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .ToTable("usuario")
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Perfil>()
                .ToTable("perfil")
                .HasOne(p => p.Usuario)
                .WithOne(u => u.Perfil)
                .HasForeignKey<Perfil>(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

        }

        // pegando as configs do appsettings.json
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

    }
}
