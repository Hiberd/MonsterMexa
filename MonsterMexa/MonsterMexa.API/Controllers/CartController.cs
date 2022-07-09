using Microsoft.AspNetCore.Mvc;
using MonsterMexa.BusinessLogic;
using MonsterMexa.Domain;

namespace MonsterMexa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddProductToCart(int productId)
        {
            var products = Store.GetAllProducts();

            if (!products.Any(p => p.Id == productId))
            {
                return BadRequest("The entered ID does not exist");
            }

            Product product = products.First(p => p.Id == productId);

            Cart.GetAllProducts()
                .Add(product);

            return Ok(productId);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductFromCart(int productId)
        {
            var products = Cart.GetAllProducts();

            if (!products.Any(p => p.Id == productId))
            {
                return BadRequest("The entered ID does not exist");
            }

            products.RemoveAll(p => p.Id == productId);

            return Ok(productId);
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            Cart.GetAllProducts().Clear();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsFromCart()
        {
            var products = Cart.GetAllProducts();

            string list = String.Empty;

            foreach (var product in products)
            {
                list = $"{list}{product.Id}-{product.Name}-{product.Size}\n";
            }

            return Ok(list);
        }
    }
}
