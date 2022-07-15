using MonsterMexa.Domain;

namespace MonsterMexa.BusinessLogic
{
    public class CategoriesService : ICategotiesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<int> AddCategory(Category category)
        {
            return await _categoriesRepository.AddCategory(category);
        }

        public async Task AddProduct(int productId, int categoryId)
        {
            await _categoriesRepository.AddProduct(productId, categoryId);
        }

        public async Task Delete(int categoryId)
        {
            await _categoriesRepository.Delete(categoryId);
        }

        public async Task<Category[]> GetAllCategories()
        {
            return await _categoriesRepository.GetAll();
        }

        public Task<Category> GetById(int categoryId)
        {
            return _categoriesRepository.GetById(categoryId);
        }

        public async Task Update(Category category)
        {
            await _categoriesRepository.Update(category);
        }
    }
}
