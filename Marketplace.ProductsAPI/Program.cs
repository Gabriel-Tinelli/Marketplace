using Microsoft.EntityFrameworkCore;
using ProductsService.Data;
using CategoryService.Data;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext do produto ao pipeline de serviços
builder.Services.AddDbContext<MarketplaceContextProduct>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Adiciona o DbContext da categoria ao pipeline de serviços
builder.Services.AddDbContext<MarketplaceContextCategory>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

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