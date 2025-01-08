using Microsoft.EntityFrameworkCore;
using ProductsService.Data;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext ao pipeline de serviços
builder.Services.AddDbContext<MarketplaceContextProduct>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Add services to the container.

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