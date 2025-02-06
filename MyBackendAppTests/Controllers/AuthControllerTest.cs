using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyBackendApp.Controllers;
using MyBackendApp.Services;
using MyBackendApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Xunit;
namespace MyBackendAppTests.Controllers;

public class AuthControllerTest
{
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly Mock<DatabaseService> _mockDatabaseService;
    private readonly AuthController _controller;

    public AuthControllerTest()
    {
        _mockConfig = new Mock<IConfiguration>();
        _mockDatabaseService = new Mock<DatabaseService>(null);

        _mockConfig.Setup(config => config["Jwt:Key"]).Returns("YmFzZTY0ZW5jb2RlZGtleQ=="); // Chave fake base64
        _mockConfig.Setup(config => config["Jwt:Issuer"]).Returns("test_issuer");
        _mockConfig.Setup(config => config["Jwt:Audience"]).Returns("test_audience");

        _controller = new AuthController(_mockConfig.Object, _mockDatabaseService.Object);
    }

    private void SetAuthContext(int userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = principal };
        _controller.ControllerContext = new ControllerContext { HttpContext = context };
    }

    //[Fact]
    public async Task Login_DeveRetornarOk_QuandoCredenciaisSaoValidas()
    {
        // Arrange

        // Act

        // Assert

    }

    //[Fact]
    public async Task Login_DeveRetornarUnauthorized_QuandoUsuarioNaoExiste()
    {
        // Arrange

        // Act

        // Assert

    }

    //[Fact]
    public async Task Login_DeveRetornarUnauthorized_QuandoSenhaEstaIncorreta()
    {
        // Arrange

        // Act

        // Assert

    }

    //[Fact]
    public void ValidateToken_DeveRetornarOk_QuandoTokenEhValido()
    {
        // Arrange

        // Act

        // Assert

    }

}
