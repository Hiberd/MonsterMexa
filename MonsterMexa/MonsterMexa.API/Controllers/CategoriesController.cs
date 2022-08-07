using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ICategotiesService _categotiesService;

        public CategoriesController(ILogger<CategoriesController> logger, IMapper mapper, ICategotiesService categotiesService)
        {
            _logger = logger;
            _mapper = mapper;
            _categotiesService = categotiesService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            var category = Category.Create(request.Name);
            if (category.IsFailure)
            {
                _logger.LogError(category.Error);
                return BadRequest(category.Error);
            }

            var categoryId = await _categotiesService.AddCategory(category.Value);
            return Ok(categoryId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categotiesService.GetAllCategories();

            return Ok(categories);
        }

        [HttpDelete("{categoryId:int}")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            await _categotiesService.Delete(categoryId);

            return Ok(categoryId);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryRequest request)
        {
            var category = Category.Create(request.Name);
            if (category.IsFailure)
            {
                _logger.LogError(category.Error);
                return BadRequest(category.Error);
            }

            await _categotiesService.Update(category.Value with { Id = request.Id });

            return Ok();
        }

        [HttpPost("{categoryId:int}/{productId:int}")]
        public async Task<IActionResult> AddProduct(int productId, int categoryId)
        {
            var result = await _categotiesService.AddProduct(productId, categoryId);

            if (result.IsFailure)
            {
                _logger.LogError(result.Error);
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
