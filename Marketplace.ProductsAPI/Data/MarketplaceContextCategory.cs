using Microsoft.EntityFrameworkCore;
using CategoryService.Models;

namespace CategoryService.Data
{
    public class MarketplaceContextCategory : DbContext
    {
        public MarketplaceContextCategory(DbContextOptions<MarketplaceContextCategory> options) : base(options) { }
        
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar a chave primária
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}