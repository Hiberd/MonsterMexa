using System.ComponentModel.DataAnnotations;

namespace MonsterMexa.DataAccess.Postgres.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
