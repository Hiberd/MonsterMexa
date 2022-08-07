using CSharpFunctionalExtensions;
using MonsterMexa.Domain;

namespace MonsterMexa.BusinessLogic
{
    public class CategoriesService : ICategotiesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IProductsPepository _productsPepository;

        public CategoriesService(ICategoriesRepository categoriesRepository, IProductsPepository productsPepository)
        {
            _categoriesRepository = categoriesRepository;
            _productsPepository = productsPepository;
        }

        public async Task<int> AddCategory(Category category)
        {
            return await _categoriesRepository.AddCategory(category);
        }

        public async Task<Result<bool>> AddProduct(int productId, int categoryId)
        {
            var category = await _categoriesRepository.GetById(categoryId);
            var product = await _productsPepository.GetById(productId);

            if (product == null)
            {
                return Result.Failure<bool>("The product doesn't exist");
            }

            if (category == null)
            {
                return Result.Failure<bool>("The category doesn't exist");
            }

            return await _categoriesRepository.AddProduct(productId, categoryId);
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
