
namespace MonsterMexa.DataAccess.Postgres.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public DateTime? DeletedAt { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
