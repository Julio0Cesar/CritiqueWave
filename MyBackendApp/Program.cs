using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Dependency injection (acessa ao appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new MyBackendApp.Services.DatabaseService(connectionString));

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
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowFrontend"); // Habilita o CORS (politica de segurança dos navegadores)
app.UseHttpsRedirection(); // Habilita redirecionamento HTTP para HTTPS, se necessário
app.UseAuthentication(); // Habilita autenticação
app.UseAuthorization(); // Habilita autorização
app.MapControllers(); // Mapeia as controllers da API

app.Run(); // Inicia a aplicação