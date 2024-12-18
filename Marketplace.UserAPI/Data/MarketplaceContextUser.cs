using Microsoft.EntityFrameworkCore;
using Marketplace.UserAPI.Models;

namespace Marketplace.Data
{
    public class MarketplaceContextUser : DbContext
    {
        public MarketplaceContextUser(DbContextOptions<MarketplaceContextUser> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
