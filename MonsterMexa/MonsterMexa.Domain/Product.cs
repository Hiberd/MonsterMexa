using CSharpFunctionalExtensions;

namespace MonsterMexa.Domain
{
    public record Product
    {
        public const int MaxProductName = 20;
        private Product(int id, string name, int size, int? categotyId)
        {
            Id = id;
            Name = name;
            Size = size;
            CategoryId = categotyId;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public int Size { get; init; }
        public int? CategoryId { get; set; }

        public static Result<Product> Create(string name, int size)
        {
            if (string.IsNullOrWhiteSpace(name)) // Зачем проверять, если атрибуты валидации срабатывают раньше
            {
                return Result.Failure<Product>("Name cannot be null or whitespace");
            }

            if (name.Length > MaxProductName)
            {
                return Result.Failure<Product>($"Name cannot have more than {MaxProductName} char");
            }

            return new Product(0, name, size, null);
        }

        public static Result<Product> Update(int id, string name, int size)
        {
            return new Product(id, name, size, null);
        }

    }
}
