using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text;
using DotNetEnv;
using MyBackendApp.Services;
using MyBackendApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5218); // Escuta na porta 5218 dentro do container
});

// Dependency injection (acessa ao appsettings.json)
builder.Services.AddScoped<DatabaseService>(); 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);


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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
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
app.MapControllers(); // Mapeia as controllers da API

app.Run(); // Inicia a aplicação