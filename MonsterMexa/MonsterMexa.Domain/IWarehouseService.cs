using CSharpFunctionalExtensions;

namespace MonsterMexa.Domain
{
    public interface IWarehouseService
    {
        public Task<Result<int>> AddProduct(int productId, int quantity);

        public Task<Result<int>> ChangeQuantity(int productId, int quantity);

        public Task<Models.WarehouseProduct[]> GetAll();

        public Task<Result<Models.WarehouseProduct>> GetById(int productId);

        public Task<bool> SoftDelete(int productId);

        public Task<bool> HardDelete(int productId);
    }
}
