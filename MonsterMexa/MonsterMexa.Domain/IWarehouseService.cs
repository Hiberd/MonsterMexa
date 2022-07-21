using CSharpFunctionalExtensions;

namespace MonsterMexa.Domain
{
    public interface IWarehouseService
    {
        public Task<int> AddProduct(Product product, int quantity);

        public Task<Result<int>> ChangeQuantity(int productId, int quantity);

        public Task<Models.ProductsFromWarehouse[]> GetAll();

        public Task<Result<Models.ProductsFromWarehouse>> GetById(int productId);

        public Task<bool> SoftDelete(int productId);

        public Task<bool> HardDelete(int productId);
    }
}
