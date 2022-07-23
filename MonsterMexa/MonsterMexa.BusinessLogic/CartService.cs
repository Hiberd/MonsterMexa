using CSharpFunctionalExtensions;
using MonsterMexa.Domain;

namespace MonsterMexa.BusinessLogic
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IWarehouseRepository _warehouseRepository;

        public CartService(ICartRepository cartRepository, IWarehouseRepository warehouseRepository)
        {
            _cartRepository = cartRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<Result<int>> AddProduct(int productId, string userId)
        {
            var productFromCart = await _cartRepository.Get(productId);

            var productFromWarehouse = await _warehouseRepository.GetById(productId);

            if (productFromWarehouse == null)
            {
                return Result.Failure<int>("The entered ID does not exist");
            }

            if ((productFromCart == null && productFromWarehouse.Quantity > 0) ||
                 productFromWarehouse.Quantity > productFromCart?.Quantity)
            {
                await _cartRepository.AddProduct(productId, userId);
            }
            else
            {
                return Result.Failure<int>("The product is over");
            }

            return productId;
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
