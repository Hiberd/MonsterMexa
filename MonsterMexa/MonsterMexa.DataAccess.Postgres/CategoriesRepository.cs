using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonsterMexa.Domain;

namespace MonsterMexa.DataAccess.Postgres
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly MonsterMexaDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoriesRepository(MonsterMexaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> AddCategory(Category category)
        {
            var categoryEntity = _mapper.Map<Domain.Category, Entities.Category>(category);
            await _dbContext.Categories.AddAsync(categoryEntity);
            await _dbContext.SaveChangesAsync();

            return categoryEntity.Id;
        }

        public async Task AddProduct(int productId, int categoryId)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product != null && category != null)
            {
                category.Products.Add(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int categoryId)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Category[]> GetAll()
        {
            var categories = await _dbContext.Categories
                .AsNoTracking()
                .ToArrayAsync();

            return _mapper.Map<Entities.Category[], Domain.Category[]>(categories);
        }

        public async Task<Category> GetById(int categoryId)
        {
            var category = await _dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            return _mapper.Map<Entities.Category, Domain.Category>(category);
        }

        public async Task Update(Category category)
        {
            var oldCategory = await _dbContext.Categories
                 .FirstOrDefaultAsync(c => c.Id == category.Id);

            if (oldCategory != null)
            {
                oldCategory.Name = category.Name;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
