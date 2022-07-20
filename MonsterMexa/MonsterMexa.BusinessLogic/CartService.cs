using CSharpFunctionalExtensions;
using MonsterMexa.Domain;

namespace MonsterMexa.BusinessLogic
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductsPepository _productsPepository;

        public CartService(ICartRepository cartRepository, IProductsPepository productsPepository)
        {
            _cartRepository = cartRepository;
            _productsPepository = productsPepository;
        }

        public async Task<Result<int>> AddProduct(int productId, string userId)
        {
            var product = await _productsPepository.GetById(productId);

            if (product == null)
            {
                return Result.Failure<int>("The entered ID does not exist");
            }

            var id = await _cartRepository.AddProduct(productId, userId);

            return id;
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
