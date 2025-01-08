using Microsoft.EntityFrameworkCore;
using ProductsService.Models;

namespace ProductsService.Data
{
    public class MarketplaceContextProduct : DbContext
    {
        public MarketplaceContextProduct(DbContextOptions<MarketplaceContextProduct> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}

