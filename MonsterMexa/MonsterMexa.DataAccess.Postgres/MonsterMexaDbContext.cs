using Microsoft.EntityFrameworkCore;

namespace MonsterMexa.DataAccess.Postgres
{
    public class MonsterMexaDbContext : DbContext
    {
        public MonsterMexaDbContext(DbContextOptions<MonsterMexaDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}