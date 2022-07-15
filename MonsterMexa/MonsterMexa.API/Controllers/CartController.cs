using Microsoft.AspNetCore.Mvc;
using MonsterMexa.Domain;

namespace MonsterMexa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly HttpContext _context;

        public CartController(ICartService cartService, HttpContext context)
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

            return Ok();
        }

        [NonAction]
        public string GetUserId()
        {
            if (!_context.Session.Keys.Contains("UserId"))
            {
                string tempUserId = Guid.NewGuid().ToString();
                _context.Session.SetString("UserId", tempUserId);
            }

            return _context.Session.GetString("UserId");
        }
    }
}
