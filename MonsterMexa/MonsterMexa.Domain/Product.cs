using CSharpFunctionalExtensions;

namespace MonsterMexa.Domain
{
    public record Product
    {
        public const int MaxProductName = 20;
        public const int MaxProductSize = 50;
        public const int MinProductSize = 25;
        private Product(int id, string name, int size, int? categoryId)
        {
            Id = id;
            Name = name;
            Size = size;
            CategoryId = categoryId;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public int Size { get; init; }
        public int? CategoryId { get; set; }

        public static Result<Product> Create(string name, int size)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Product>("Name cannot be null or whitespace");
            }

            if (name.Length > MaxProductName)
            {
                return Result.Failure<Product>($"Name cannot have more than {MaxProductName} char");
            }

            if (size < MinProductSize && size > MaxProductSize)
            {
                return Result.Failure<Product>($"Size cannot be more than {MaxProductSize} amd less than {MinProductSize}");
            }

            return new Product(0, name, size, null);
        }
    }
}
