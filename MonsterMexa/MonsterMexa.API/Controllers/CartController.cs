using Microsoft.AspNetCore.Mvc;
using MonsterMexa.Domain;

namespace MonsterMexa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart(int productId)
        {
            string userId = GetUserId();

            var id = await _cartService.AddProduct(productId, userId);

            if (id.IsFailure)
            {
                _logger.LogError(id.Error);
                return BadRequest(id.Error);
            }

            return Ok(id.Value);
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
