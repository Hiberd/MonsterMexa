using MonsterMexa.Domain;

namespace MonsterMexa.BusinessLogic
{
    public static class Store
    {
        private static List<Product> products = new List<Product>();

        public static int CreateProduct(Product product)
        {
            int id = 1;
            if (products.Count > 0)
            {
                id = products.Max(p => p.Id) + 1;
            }

            products.Add(product with { Id = id });
            return id;
        }

        public static int UpdateProduct(Product product)
        {
            products.RemoveAll(p => p.Id == product.Id);
            products.Add(product with { Id = product.Id });
            return product.Id;
        }

        public static int ClearCategoryIdFromProduct(Product product)
        {
            products.RemoveAll(p => p.Id == product.Id);
            products.Add(product with { Id = product.Id, CategoryId = null });
            return product.Id;
        }

        public static List<Product> GetAllProducts()
        {
            return products;
        }
    }
}
