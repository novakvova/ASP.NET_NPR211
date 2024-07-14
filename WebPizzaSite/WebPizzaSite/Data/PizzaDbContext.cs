using Microsoft.EntityFrameworkCore;
using WebPizzaSite.Data.Entities;

namespace WebPizzaSite.Data
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options) { }

        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
