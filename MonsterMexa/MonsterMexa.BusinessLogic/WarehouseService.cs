using CSharpFunctionalExtensions;
using Models;
using MonsterMexa.Domain;

namespace MonsterMexa.BusinessLogic
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IProductsPepository _productsPepository;

        public WarehouseService(IWarehouseRepository warehouseRepository, IProductsPepository productsPepository)
        {
            _warehouseRepository = warehouseRepository;
            _productsPepository = productsPepository;
        }

        public async Task<int> AddProduct(Product product, int quantity)
        {
            return await _warehouseRepository.AddProduct(product, quantity);
        }

        public async Task<ProductsFromWarehouse[]> GetAll()
        {
            return await _warehouseRepository.GetAll();
        }

        public async Task<Result<ProductsFromWarehouse>> GetById(int productId)
        {
            var product = await _warehouseRepository.GetById(productId);

            if (product == null)
            {
                return Result.Failure<ProductsFromWarehouse>("The entered ID does not exist");
            }

            return product;
        }

        public async Task<Result<int>> ChangeQuantity(int productId, int quantity)
        {
            var product = await _productsPepository.GetById(productId);

            if (product == null)
            {
                return Result.Failure<int>("The entered ID does not exist");
            }

            var warehouseProduct = await _warehouseRepository.GetById(productId);

            if (warehouseProduct == null)
            {
                return await _warehouseRepository.AddProduct(product, quantity);
            }

            return await _warehouseRepository.ChangeQuantity(productId, quantity);
        }

        public async Task<bool> HardDelete(int productId)
        {
            return await _warehouseRepository.HardDelete(productId);
        }

        public async Task<bool> SoftDelete(int productId)
        {
            return await _warehouseRepository.SoftDelete(productId);
        }
    }
}
