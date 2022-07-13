using CSharpFunctionalExtensions;

namespace MonsterMexa.DataAccess.Postgres
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public int? CategoryId { get; set; }
    }
}
