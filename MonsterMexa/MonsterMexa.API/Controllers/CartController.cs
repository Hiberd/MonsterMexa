using Microsoft.AspNetCore.Mvc;
using MonsterMexa.Domain;

namespace MonsterMexa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ControllerContext _context;

        public CartController(ICartService cartService, ControllerContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart(int productId)
        {
            string userId = GetUserId();

            await _cartService.AddProduct(productId, userId);

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductFromCart(int productId)
        {
            string userId = GetUserId();

            await _cartService.DeleteProduct(productId, userId);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            string userId = GetUserId();

            await _cartService.ClearCart(userId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsFromCart()
        {
            string userId = GetUserId();

            var products = await _cartService.GetAllProductsFromCart(userId);

            return Ok(products);
        }

        [NonAction]
        public string GetUserId()
        {
            if (Request.Cookies.ContainsKey("UserId"))
            {
                return Request.Cookies["UserId"];
            }
            else
            {
                string tempUserId = Guid.NewGuid().ToString();
                Response.Cookies.Append("UserId", tempUserId);

                return tempUserId;
            }
        }
    }
}
