using MonsterMexa.Domain;

namespace MonsterMexa.BusinessLogic
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task AddProduct(int productId, string userId)
        {
            await _cartRepository.AddProduct(productId, userId);
        }

        public async Task ClearCart(string userId)
        {
           await _cartRepository.ClearCart(userId);
        }

        public async Task DeleteProduct(int productId, string userId)
        {
            await _cartRepository.DeleteProduct(productId, userId);
        }

        public async Task<Cart[]> GetAllProductsFromCart(string userId)
        {
            return await _cartRepository.GetAllProductsFromCart(userId);
        }
    }
}
