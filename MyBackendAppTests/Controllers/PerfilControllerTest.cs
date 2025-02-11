using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyBackendApp.Controllers;
using MyBackendApp.Services;
using MyBackendApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
namespace MyBackendAppTests.Controllers;

public class PerfilControllerTest
{
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly Mock<DatabaseService> _mockDatabaseService;
    private readonly PerfilController _controller;

    public PerfilControllerTest()
    {
        _mockConfig = new Mock<IConfiguration>();
        _mockDatabaseService = new Mock<DatabaseService>(null);
        _controller = new PerfilController(_mockConfig.Object, _mockDatabaseService.Object);
    }

    private void SetPerfilContext(int userId)
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

    private IFormFile CriarArquivoMockado(string nomeArquivo, string extensao)
    {
        var arquivoMock = new Mock<IFormFile>();
        var conteudo = new byte[] { 1, 2, 3 };  // Simulando conteúdo do arquivo
        var stream = new MemoryStream(conteudo);
        arquivoMock.Setup(_ => _.OpenReadStream()).Returns(stream);
        arquivoMock.Setup(_ => _.FileName).Returns(nomeArquivo);
        arquivoMock.Setup(_ => _.ContentType).Returns($"image/{extensao}");
        return arquivoMock.Object;
    }

    [Fact]
    public async Task ObterPerfil_DeveRetornarOk_QuandoPerfilExiste()
    {
        //Arrange
        SetPerfilContext(1);
        var perfil = new Perfil { UsuarioId = 1 };
        _mockDatabaseService.Setup(s => s.ObterPerfilPeloId(1)).ReturnsAsync(perfil);

        //Act
        var resultado = await _controller.ObterMeuPerfil();

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(perfil, okResult.Value);
    }

    [Fact]
    public async Task AtualizarPerfil_DeveRetornarOk_QuandoPerfilExiste()
    {
        //Arrange
        SetPerfilContext(1);
        var perfil = new Perfil{UsuarioId = 1, Status = "Status Atualizado", Sobre = "Sobre atualizado"};
        var fotoPerfilMock = new Mock<IFormFile>();
        var capaPerfilMock = new Mock<IFormFile>();

        _mockDatabaseService.Setup(s => s.ObterPerfilPeloId(1))
            .ReturnsAsync(perfil);
        _mockDatabaseService.Setup(s => s.SalvarArquivoExterno(It.IsAny<IFormFile>(), It.IsAny<string>()))
            .ReturnsAsync("/uploads/perfil/fotoPerfil.jpg");
        _mockDatabaseService.Setup(s => s.AtualizarPerfilNoBanco(It.IsAny<Perfil>()))
            .ReturnsAsync(true);

        //Act
        var resultado = await _controller.AtualizarPerfil(
            fotoPerfil: fotoPerfilMock.Object,
            capaPerfil: capaPerfilMock.Object,
            status: perfil.Status,
            sobre: perfil.Sobre
        );

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal("Perfil atualizado com sucesso", okResult.Value);
    }
}
