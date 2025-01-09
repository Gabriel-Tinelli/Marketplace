using Microsoft.EntityFrameworkCore;
using CategoryService.Data;
using ProductsService.Data;
using Marketplace.Data;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o HttpClient para ser injetado em controllers e outros serviços
builder.Services.AddHttpClient();  // Adiciona o HttpClient

// Adiciona o DbContext da categoria ao pipeline de serviços
builder.Services.AddDbContext<MarketplaceContextCategory>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddDbContext<MarketplaceContextProduct>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddDbContext<MarketplaceContextUser>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnectionUsers"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnectionUsers"))));

// Adiciona os controllers ao pipeline
builder.Services.AddControllers();

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

app.UseAuthorization();

app.MapControllers();

app.Run();