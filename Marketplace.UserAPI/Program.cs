using Marketplace.Data;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Marketplace.UserAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Autenticação com o JWT
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "local-issuer",
            ValidAudience = "local-audience",
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    });
// Adicionar autorização

builder.Services.AddAuthorization();

// Adiciona o DbContext ao pipeline de serviços
builder.Services.AddDbContext<MarketplaceContextUser>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Adiciona os controladores
builder.Services.AddControllers();

// Injetar dependências
builder.Services.AddSingleton<AuthenticationTokenService>();

// Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o Swagger para ser usado na interface
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Gera a documentação Swagger
    app.UseSwaggerUI();  // Exibe a interface gráfica do Swagger UI
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

app.Run();