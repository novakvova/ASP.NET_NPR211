using Microsoft.EntityFrameworkCore;
using WebAlina.Data.Entities;

namespace WebAlina.Data
{
    public class AlinaDbContext : DbContext
    {
        public AlinaDbContext(DbContextOptions<AlinaDbContext> contextOptions)
            : base(contextOptions)
        {  }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductImageEntity> ProductImages { get; set; }
    }
}
