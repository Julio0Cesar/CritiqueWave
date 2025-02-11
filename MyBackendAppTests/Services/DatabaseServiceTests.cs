using Moq;
using Microsoft.EntityFrameworkCore;
using MyBackendApp.Models;
using MyBackendApp.Data;
using MyBackendApp.Services;
using System.Threading.Tasks;

namespace MyBackendAppTests.Services;

public class DatabaseServiceTests
{
    private readonly Mock<ApplicationDbContext> _mockContext;
    private readonly Mock<DbSet<Usuario>> _mockDbSetUsuarios;
    private readonly DatabaseService _databaseService;

    public DatabaseServiceTests()
    {
        _mockContext = new Mock<ApplicationDbContext>();
        _mockDbSetUsuarios = new Mock<DbSet<Usuario>>();

        //Configurar o mock do DbSet para retornar um usuário específico
        _mockContext.Setup(m => m.Usuarios).Returns(_mockDbSetUsuarios.Object);
        _databaseService = new DatabaseService(_mockContext.Object);
    }

    //[Fact]
    public async Task ObterUsuarioPeloId_DeveRetornarUsuario_QuandoUsuarioExistir()
    {
        // Arrange

        // Act

        // Assert
    }

    //[Fact]
    public async Task ObterUsuarioPeloId_DeveRetornarNull_QuandoUsuarioNaoExistir()
    {
        //Arrange

        //ACt

        //Assert

    }


}
