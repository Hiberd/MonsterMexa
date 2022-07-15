
namespace MonsterMexa.Domain
{
    public interface IProductsPepository
    {
        public Task<int> Add(Product product);

        public Task<bool> Delete(int productId);

        public Task<Product[]> GetAll();

        public Task<Product> GetById(int productId);

        public Task<Product[]> GetByCategory(int categoryId);

        public Task Update(Product product);
    }
}
