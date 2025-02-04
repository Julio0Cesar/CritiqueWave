using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using MyBackendApp.Services;
using MyBackendApp.Data;
using MyBackendApp.Middleware;

//Build
var builder = WebApplication.CreateBuilder(args);

// Escuta na porta 5218 dentro do container
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5218);
});

// Dependency injection (acessa ao appsettings.json)
builder.Services.AddScoped<DatabaseService>(); 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Validação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Convert.FromBase64String(builder.Configuration["Jwt:Key"])
            )
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var authHeader = context.Request.Headers["Authorization"].ToString();
                Console.WriteLine("Authorization header recebido: " + authHeader);
                
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    // Remove o prefixo "Bearer " e atribui somente o token
                    context.Token = authHeader.Substring("Bearer ".Length).Trim();
                }

                Console.WriteLine("Token recebido após processamento: " + context.Token);
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("Falha na autenticação: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

//Liberando requisição do frontend requisições HTTP (GET POST etc), Header HTTP, credenciais
builder.Services.AddCors(Options =>
{
    Options.AddPolicy("AllowFrontend", policy =>
    {
        policy.SetIsOriginAllowed(_ => true) //WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Adicionando controladores e rodando a API
builder.Services.AddControllers();
var app = builder.Build();

// Testar a conexão com o banco de dados
using (var scope = app.Services.CreateScope())
{
    var dbService = scope.ServiceProvider.GetRequiredService<DatabaseService>();
    if (await dbService.TestarConexao())
    {
        Console.WriteLine("Conectado ao banco de dados com sucesso");
    }
    else 
    {
        Console.WriteLine("Erro ao conectar ao banco de dados");
    }
}

app.UseCors("AllowFrontend"); // Habilita o CORS (politica de segurança dos navegadores)
app.UseHttpsRedirection(); // Habilita redirecionamento HTTP para HTTPS, se necessário
app.UseAuthentication(); // Habilita autenticação
app.UseAuthorization(); // Habilita autorização
// app.UseMiddleware<JwtMiddleware>(); Validação do JWT
app.UseMiddleware<LoggingMiddleware>(); // log requisições
app.UseMiddleware<JsonForceMiddleware>(); // força header json
app.UseMiddleware<ErrorHandlingMiddleware>(); // tratamento global de erros
app.UseMiddleware<ResponseTimeMiddleware>(); // mede tempo de respostas
app.MapControllers(); // Mapeia as controllers da API
app.Run(); // Inicia a aplicação