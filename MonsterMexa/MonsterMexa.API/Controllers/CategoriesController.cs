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

            string list = String.Empty;

            foreach (var category in categories)
            {
                list = $"{list}{category.Id}-{category.Name}\n";
            }

            return Ok(list);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var categories = CategoriesService.GetAllCategories();
            var products = Store.GetAllProducts();

            if (!categories.Any(p => p.Id == id))
            {
                return BadRequest("The entered ID does not exist");
            }

            categories.RemoveAll(c => c.Id == id);
            products.Where(p => p.CategoryId == id)
                .Select(u => u with { CategoryId = null })
                .ToList();

            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryRequest request)
        {
            var categories = CategoriesService.GetAllCategories();

            if (!categories.Any(p => p.Id == request.Id))
            {
                return BadRequest("The entered ID does not exist");
            }

            categories.Where(c => c.Id == request.Id)
                .Select(u => u with { Name = request.Name })
                .ToList();

            return Ok();
        }

        [HttpPost("{categoryId:int}/products")]
        public async Task<IActionResult> AddProduct(int productId, int categoryId)
        {
            var categories = CategoriesService.GetAllCategories();
            var products = Store.GetAllProducts();

            if (!categories.Any(p => p.Id == categoryId) || !products.Any(p => p.Id == productId))
            {
                return BadRequest("The entered ID does not exist");
            }

            Category category = categories.First(c => c.Id == categoryId);

            Product product = products.First(p => p.Id == productId);

            category.Products.Add(product);
            product.CategoryId = categoryId;

            return Ok();
        }
    }
}
