using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MyBackendApp.Services;
using Renci.SshNet;

namespace MyBackendApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        // Configuração do serviço do banco
        private readonly IConfiguration _configuration;
        private readonly DatabaseService _databaseService;
        public PostsController(IConfiguration configuration, DatabaseService databaseService)
        {
            _configuration = configuration;
            _databaseService = databaseService;
        }
    }
}