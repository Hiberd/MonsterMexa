using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonsterMexa.API.Contracts;
using MonsterMexa.BusinessLogic;
using MonsterMexa.Domain;

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
        public async Task<IActionResult> AllList()
        {
            var products = await _productService.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListById(int id)
        {
            var product = await _productService.GetById(id);

            if (product.Id != id)
            {
                return BadRequest("The entered ID does not exist");
            }

            return Ok(product);
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductRequest request)
        {
            var product = _mapper.Map<Contracts.UpdateProductRequest, Domain.Product>(request);

            _productService.Update(product);

            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateProductRequest request)
        {
            var product = Product.Create(request.Name, request.Size);
            if (product.IsFailure)
            {
                _logger.LogError(product.Error);
                return BadRequest(product.Error);
            }

            var productId = _productService.CreateProduct(product.Value);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _productService.Delete(id);

            return Ok(product);
        }
    }
}
