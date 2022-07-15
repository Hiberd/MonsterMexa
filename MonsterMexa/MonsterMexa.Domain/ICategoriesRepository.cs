

namespace MonsterMexa.Domain
{
    public interface ICategoriesRepository
    {
        public Task<int> AddCategory(Category category);

        public Task AddProduct(int productId, int categoryId);

        public Task Delete(int categoryId);

        public Task<Category[]> GetAll();

        public Task<Category> GetById(int categoryId);

        public Task Update(Category category);
    }
}
