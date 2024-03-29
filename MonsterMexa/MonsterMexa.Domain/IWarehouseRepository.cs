﻿

namespace MonsterMexa.Domain
{
    public interface IWarehouseRepository
    {
        public Task<int> AddProduct(Product product, int quantity);

        public Task<int> ChangeQuantity(int productId, int quantity);

        public Task<Models.WarehouseProduct[]> GetAll();

        public Task<Models.WarehouseProduct> GetById(int productId);

        public Task<bool> SoftDelete(int productId);

        public Task<bool> HardDelete(int productId);
    }
}
