using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonsterMexa.Domain;

namespace MonsterMexa.DataAccess.Postgres
{
    public class ProductsRepository : IProductsPepository
    {
        private readonly MonsterMexaDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductsRepository(MonsterMexaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Add(Product product)
        {
            var productEntity = _mapper.Map<Domain.Product, Entities.Product>(product);

            await _dbContext.Products.AddAsync(productEntity);
            await _dbContext.SaveChangesAsync();

            return productEntity.Id;
        }

        public async Task<bool> Delete(int productId)
        {
            var product = await _dbContext.Products
                .SingleOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                return false;
            }

            _dbContext.Products.Remove(product);

            return true;
        }

        public async Task<Product[]> GetAll()
        {
            var products = await _dbContext.Products
                .AsNoTracking()
                .ToArrayAsync();

            return _mapper.Map<Entities.Product[], Domain.Product[]>(products);
        }

        public async Task<Product> GetById(int productId)
        {
            var product = await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId);

            return _mapper.Map<Entities.Product, Domain.Product>(product);
        }

        public async Task<Product[]> GetByCategory(int categoryId)
        {
            var products = await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .ToArrayAsync();

            return _mapper.Map<Entities.Product[], Domain.Product[]>(products);
        }

        public async Task Update(Product product)
        {
            var newProduct = _mapper.Map<Domain.Product, Entities.Product>(product);

            _dbContext.Products.Update(newProduct);
            await _dbContext.SaveChangesAsync();
        }
    }
}
