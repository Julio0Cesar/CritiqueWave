using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyBackendApp.Controllers;
using MyBackendApp.Services;
using MyBackendApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MyBackendAppTests.Controllers;

public class UsuariosControllerTests
{
    private readonly Mock<IConfiguration> _mockConfig; //simula config
    private readonly Mock<DatabaseService> _mockDatabaseService; // simula o acesso ao banco
    private readonly UsuariosController _controller; //instancia o Controller que vai ser testado

    //Criando as instancias dos mocks
    public UsuariosControllerTests()
    {
        _mockConfig = new Mock<IConfiguration>();
        _mockDatabaseService = new Mock<DatabaseService>(null); // Null pois não estamos usando o contexto real do banco
        _controller = new UsuariosController(_mockConfig.Object, _mockDatabaseService.Object);
    }

    //Criando usuário para simular requisição
    private void SetUserContext(int userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = principal };
        _controller.ControllerContext = new ControllerContext { HttpContext = context };
    }

    [Fact]
    public async Task ObterUsuario_DeveRetornarNotFound_QuandoUsuarioNaoExiste()
    {
        // Arrange
        SetUserContext(1);
        _mockDatabaseService.Setup(s => s.ObterUsuarioPeloId(1)).ReturnsAsync((Usuario)null);

        // Act
        var resultado = await _controller.ObterUsuario();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado);
        Assert.Equal("Usuário não encontrado", notFoundResult.Value);
    }

    [Fact]
    public async Task ObterUsuario_DeveRetornarOk_QuandoUsuarioExiste()
    {
        // Arrange
        SetUserContext(1);
        var usuario = new Usuario { Id = 1, Email = "teste@email.com" };
        _mockDatabaseService.Setup(s => s.ObterUsuarioPeloId(1)).ReturnsAsync(usuario);

        // Act
        var resultado = await _controller.ObterUsuario();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(usuario, okResult.Value);
    }

    [Fact]
    public async Task CriarUsuario_DeveRetornarBadRequest_QuandoEmailJaExiste()
    {
        //Arrange
        var usuarioExistente = new Usuario { Id = 1, Email = "testes@email.com" };
        var novoUsuario = new Usuario { Email = "testes@email.com" };

        _mockDatabaseService
            .Setup(s => s.ObterUsuarioPorEmail(novoUsuario.Email))
            .ReturnsAsync(usuarioExistente);

        //Act
        var resultado = await _controller.CriarUsuario(novoUsuario);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal("Já existe um usuário com o mesmo e-mail", badRequestResult.Value);
    }

    [Fact]
    public async Task CriarUsuario_DeveCriarUsuarioERetoranrOk_QuandoEmailNaoExiste()
    {
        var novoUsuario = new Usuario { Email = "novo@email.com", SenhaHash = "123456" };

        _mockDatabaseService
            .Setup(s => s.ObterUsuarioPorEmail(novoUsuario.Email))
            .ReturnsAsync((Usuario)null); //Simulando que o email nao existe

        _mockDatabaseService
            .Setup(s => s.ObterUsuarioPorEmail(novoUsuario.Email))
            .Verifiable(); //Verifica se o metodo foi chamado

        //Act
        var resultado = await _controller.CriarUsuario(novoUsuario);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal("Usuário criado com sucesso", okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value, null));

        //Verifica se foi chamado exatamente uma vez
        _mockDatabaseService.Verify(s => s.CriarUsuarioNoBanco(novoUsuario), Times.Once);
    }

    [Fact]
    public async Task AtualizarUsuario_DeveRetornarNotFound_QuandoUsuarioNaoExiste()
    {
        //Arrange
        var usuario = new Usuario { Id = 1};

        _mockDatabaseService.Setup(s => s.ObterUsuarioPeloId(usuario.Id))
            .ReturnsAsync((Usuario)null);

        //Act
        var resultado = await _controller.AtualizarUsuario(usuario);

        //Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado);
        Assert.Equal("Usuário não encontrado", notFoundResult.Value);
    }

    [Fact]
    public async Task AtualizarUsuario_DeveRetornarOk_QuandoUsuarioExiste()
    {
        //Arrange
        var usuario = new Usuario { Id = 1};

        _mockDatabaseService.Setup(s => s.ObterUsuarioPeloId(usuario.Id))
            .ReturnsAsync(usuario); //Simula que o usuario existe no banco

        _mockDatabaseService.Setup(s => s.AtualizarUsuarioNoBanco(usuario));

        //Act
        var resultado = await _controller.AtualizarUsuario(usuario);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal("Usuário atualizado com sucesso!", okResult.Value);

    }

    [Fact]
    public async Task ExcluirUsuario_DeveRetornarNotFound_QuandoUsuarioNaoExiste()
    {
        // Arrange
        var usuario = new Usuario{Id = 1};

        _mockDatabaseService.Setup(s => s.ObterUsuarioPeloId(usuario.Id)).ReturnsAsync((Usuario)null);

        // Act
        var resultado = await _controller.ExcluirUsuario(usuario.Id);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado);
        Assert.Equal("Usuário não encontrado", notFoundResult.Value);
    }

    [Fact]
    public async Task ExcluirUsuario_DeveRetornarOk_QuandoUsuarioExiste()
    {
        // Arrange
        var usuario = new Usuario { Id = 1 };
        _mockDatabaseService.Setup(s => s.ObterUsuarioPeloId(usuario.Id)).ReturnsAsync(usuario);

        // Act
        var resultado = await _controller.ExcluirUsuario(usuario.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal("Usuário excluído com sucesso!", okResult.Value);
    }

}
