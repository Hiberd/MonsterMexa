using CSharpFunctionalExtensions;

namespace MonsterMexa.Domain
{
    public interface ICartService
    {
        public Task<Result<int>> AddProduct(int productId, string userId);

        public Task DeleteProduct(int productId, string userId);

        public Task ClearCart(string userId);

        public Task<Cart[]> GetAllProductsFromCart(string userId);
    }
}
