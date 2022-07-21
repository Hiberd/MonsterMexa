using Microsoft.EntityFrameworkCore;
using MonsterMexa.DataAccess.Postgres.Entities;

namespace MonsterMexa.DataAccess.Postgres
{
    public class MonsterMexaDbContext : DbContext
    {
        public MonsterMexaDbContext(DbContextOptions<MonsterMexaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Cart> Cart { get; set; } = null!;

        public DbSet<Warehouse> Warehouse { get; set; } = null!;
    }
}