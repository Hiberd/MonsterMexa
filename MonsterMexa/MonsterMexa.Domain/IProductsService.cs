
namespace MonsterMexa.Domain
{
    public interface IProductsService
    {
        public Task<int> CreateProduct(Product product);

        public Task<bool> Delete(int productId);

        public Task<Product[]> GetAllProducts();

        public Task<Product> GetById(int productId);

        public Task<Product[]> GetByCategory(int categoryId);

        public Task Update(Product product);
    }
}
