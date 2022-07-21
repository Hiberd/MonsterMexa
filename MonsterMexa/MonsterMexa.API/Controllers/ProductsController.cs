using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonsterMexa.API.Contracts;
using MonsterMexa.BusinessLogic;
using MonsterMexa.Domain;
using MonsterMexa.API;

namespace MonsterMexa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsService _productService;
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger, IProductsService productService, IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> Get(int productId)
        {
            var product = await _productService.GetById(productId);

            if (product.IsFailure)
            {
                _logger.LogError(product.Error);
                return BadRequest(product.Error);
            }

            var productResponse = _mapper.Map<Domain.Product, Contracts.GetProductResponse>(product.Value);

            return Ok(productResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequest request)
        {
            var product = Product.Create(request.Name, request.Size);

            if (product.IsFailure)
            {
                _logger.LogError(product.Error);
                return BadRequest(product.Error);
            }

            await _productService.Update(product.Value with { Id = request.Id });

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            var product = Product.Create(request.Name, request.Size);
            if (product.IsFailure)
            {
                _logger.LogError(product.Error);
                return BadRequest(product.Error);
            }

            var productId = await _productService.CreateProduct(product.Value);

            return Ok(productId);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.Delete(id);

            return Ok(product);
        }
    }
}
