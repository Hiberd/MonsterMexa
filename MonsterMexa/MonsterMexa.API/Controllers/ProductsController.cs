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
        private List<Product>? products;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> AllList()
        {
            products = Store.GetAllProducts();

            string list = String.Empty;

            foreach (var product in products)
            {
                list = $"{list}{product.Id}-{product.Name}-{product.Size}\n";
            }

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListById(int id)
        {
            products = Store.GetAllProducts();

            if (!products.Any(p => p.Id == id))
            {
                return BadRequest("The entered ID does not exist");
            }

            products = products
                .Where(p => p.Id == id)
                .ToList();

            string list = String.Empty;

            foreach (var product in products)
            {
                list = $"{list}{product.Id}-{product.Name}-{product.Size}\n";
            }

            return Ok(list);
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductRequest request)
        {
            products = Store.GetAllProducts();

            if (!products.Any(p => p.Id == request.Id))
            {
                return BadRequest("The entered ID does not exist");
            }

            products.RemoveAll(p => p.Id == request.Id);

            var product = Product.Update(request.Id, request.Name, request.Size);

            var productId = Store.UpdateProduct(product.Value);
            return Ok(productId);
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

            var productId = Store.CreateProduct(product.Value);
            return Ok(productId);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            products = Store.GetAllProducts();

            if (!products.Any(p => p.Id == id))
            {
                return BadRequest("The entered ID does not exist");
            }

            products.RemoveAll(p => p.Id == id);

            return Ok();
        }
    }
}
