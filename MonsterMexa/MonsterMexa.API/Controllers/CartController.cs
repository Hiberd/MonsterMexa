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
            var product = Store.GetAllProducts().FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return BadRequest("The entered ID does not exist");
            }

            Cart.AddProduct(product);

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

            return Ok(products);
        }
    }
}
