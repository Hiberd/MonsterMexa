using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonsterMexa.Domain;

namespace MonsterMexa.DataAccess.Postgres
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly MonsterMexaDbContext _dbContext;
        private readonly IMapper _mapper;

        public WarehouseRepository(MonsterMexaDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<int> AddProduct(Product product, int quantity)
        {
            var warehouse = new Entities.WarehouseProduct()
            {
                Quantity = quantity,
                ProductId = product.Id,
            };

            await _dbContext.Warehouse.AddAsync(warehouse);

            await _dbContext.SaveChangesAsync();

            return product.Id;
        }

        public async Task<int> ChangeQuantity(int productId, int quantity)
        {
            var product = await _dbContext.Warehouse.FirstOrDefaultAsync(i => i.ProductId == productId);

            product.Quantity = quantity;

            await _dbContext.SaveChangesAsync();

            return quantity;
        }

        public async Task<Models.WarehouseProduct[]> GetAll()
        {
            var products = await _dbContext.Warehouse
                .Where(p => p.DeletedAt == null)
                .Select(p => new Models.WarehouseProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    Name = p.Product.Name,
                    Size = p.Product.Size
                }).ToArrayAsync();

            return products;
        }

        public async Task<Models.WarehouseProduct> GetById(int productId)
        {
            var product = await _dbContext.Warehouse
                .Where(p => p.DeletedAt == null && p.ProductId == productId)
                .Select(p => new Models.WarehouseProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    Name = p.Product.Name,
                    Size = p.Product.Size
                }).FirstOrDefaultAsync();

            return product;
        }

        public async Task<bool> HardDelete(int productId)
        {
            var product = await _dbContext.Warehouse
                .FirstOrDefaultAsync(i => i.ProductId == productId && i.DeletedAt != null);

            if (product == null)
            {
                return false;
            }

            _dbContext.Warehouse.Remove(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SoftDelete(int productId)
        {
            var product = await _dbContext.Warehouse
                .FirstOrDefaultAsync(i => i.ProductId == productId);

            if (product == null)
            {
                return false;
            }

            product.DeletedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
