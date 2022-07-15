﻿
namespace MonsterMexa.Domain
{
    public interface ICartRepository
    {
        public Task AddProduct(int productId, string userId);

        public Task DeleteProduct(int productId, string userId);

        public Task ClearCart(string userId);

        public Task<Cart[]> GetAllProductsFromCart(string userId);
    }
}
