using Microsoft.EntityFrameworkCore;
using ProductsService.Models;
using CategoryService.Models; // Referência ao modelo Category

namespace ProductsService.Data
{
    public class MarketplaceContextProduct : DbContext
    {
        public MarketplaceContextProduct(DbContextOptions<MarketplaceContextProduct> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da chave estrangeira para CategoryID
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category) // Relaciona com a propriedade de navegação
                .WithMany()              // Uma categoria pode ter muitos produtos
                .HasForeignKey(p => p.CategoryID) // Especifica a foreign key
                .OnDelete(DeleteBehavior.Cascade); // Define o comportamento ao excluir uma categoria

            // Relacionamento Simulado com Users
            modelBuilder.Entity<Product>()
                .Property(p => p.UserID) // UserId é apenas um campo, sem relacionamento
                .IsRequired();           // Certifica que o campo é obrigatório
        }
    }
}