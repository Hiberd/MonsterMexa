using CSharpFunctionalExtensions;

namespace MonsterMexa.Domain
{
    public record Category
    {
        public const int MaxCategoryName = 15;
        private Category(int categoryId, string name, List<Product> products)
        {
            Id = categoryId;
            Name = name;
            Products = products;
        }

        public string Name { get; init; }

        public int Id { get; init; }

        public List<Product> Products { get; init; }

        public static Result<Category> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Category>("Name cannot be null or whitespace");
            }

            if (name.Length > MaxCategoryName)
            {
                return Result.Failure<Category>($"Name cannot have more than {MaxCategoryName} char");
            }

            return new Category(0, name, new List<Product>());
        }
    }
}
