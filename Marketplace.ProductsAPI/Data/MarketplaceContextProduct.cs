using Microsoft.EntityFrameworkCore;
using ProductsService.Models;
using Marketplace.UserAPI.Models;  // Referência ao modelo User (de outro microserviço)
using CategoryService.Models;     // Referência ao modelo Category (de outro microserviço)

namespace ProductsService.Data
{
    public class MarketplaceContextProduct : DbContext
    {
        public MarketplaceContextProduct(DbContextOptions<MarketplaceContextProduct> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        // Não precisamos do DbSet<User> ou DbSet<Category> aqui, já que estamos usando outras fontes para isso
        // public DbSet<User> Users { get; set; }  // Não é necessário se não for para manipular diretamente no banco de dados
        // public DbSet<Category> Categories { get; set; }  // Não é necessário se não for para manipular diretamente no banco de dados

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da chave estrangeira para UserID
            modelBuilder.Entity<Product>()
                .HasOne<Marketplace.UserAPI.Models.User>() // Referência ao modelo User do microserviço UserAPI
                .WithMany() // Um User pode ter muitos produtos
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Restrict); // Defina o comportamento ao excluir um usuário (se necessário)

            // Configuração da chave estrangeira para CategoryID
            modelBuilder.Entity<Product>()
                .HasOne<CategoryService.Models.Category>() // Referência ao modelo Category
                .WithMany() // Uma Category pode ter muitos produtos
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.Restrict); // Defina o comportamento ao excluir uma categoria (se necessário)
        }
    }
}

