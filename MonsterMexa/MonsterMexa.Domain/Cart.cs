namespace MonsterMexa.Domain
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int Quantity { get; set; } = 0;

        public int ProductId { get; set; }

        public Product? Product { get; set; }
    }
}
