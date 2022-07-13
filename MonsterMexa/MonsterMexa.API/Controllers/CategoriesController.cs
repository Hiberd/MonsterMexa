using Microsoft.AspNetCore.Mvc;
using MonsterMexa.API.Contracts;
using MonsterMexa.BusinessLogic;
using MonsterMexa.Domain;

namespace MonsterMexa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            var categories = Category.Create(request.Name);
            if (categories.IsFailure)
            {
                _logger.LogError(categories.Error);
                return BadRequest(categories.Error);
            }

            var categoryId = CategoriesService.CreateCategory(categories.Value);
            return Ok(categoryId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = CategoriesService.GetAllCategories();

            return Ok(categories);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var categories = CategoriesService.GetAllCategories();
            var products = Store.GetAllProducts()
                .Where(p => p.CategoryId == id).ToList();

            if (!categories.Any(p => p.Id == id))
            {
                return BadRequest("The entered ID does not exist");
            }

            categories.RemoveAll(c => c.Id == id);

            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    Product.Create(product.Name, product.Size);

                    var productId = Store.ClearCategoryIdFromProduct(product);
                }
            }

            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryRequest request)
        {
            var oldCategory = CategoriesService.GetAllCategories()
                .FirstOrDefault(c => c.Id == request.Id);

            if (oldCategory is null)
            {
                return BadRequest("The entered ID does not exist");
            }

            var updateCategory = Category.Create(request.Name);

            if (updateCategory.IsFailure)
            {
                _logger.LogError(updateCategory.Error);
                return BadRequest(updateCategory.Error);
            }

            var categoryId = CategoriesService.UpdateCategory(updateCategory.Value);
            return Ok();
        }

        [HttpPost("{categoryId:int}/products")]
        public async Task<IActionResult> AddProduct(int productId, int categoryId)
        {
            var category = CategoriesService.GetAllCategories()
                .FirstOrDefault(c => c.Id == categoryId);

            var product = Store.GetAllProducts()
                .FirstOrDefault(p => p.Id == productId);

            if (product is null || category is null)
            {
                return BadRequest("The entered ID does not exist");
            }

            category.Products.Add(product);
            product.CategoryId = categoryId;

            return Ok();
        }
    }
}
