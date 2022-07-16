using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonsterMexa.Domain;

namespace MonsterMexa.DataAccess.Postgres
{
    public class CartRepository : ICartRepository
    {
        private readonly MonsterMexaDbContext _dbContext;
        private readonly IMapper _mapper;

        public CartRepository(MonsterMexaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddProduct(int productId, string userId)
        {
            var cartItem = await _dbContext.Cart
                .FirstOrDefaultAsync(c => c.ProductId == productId
                && c.UserId == userId);
            var product = await _dbContext.Products
                    .FirstOrDefaultAsync(p => p.Id == productId);

            if (cartItem == null && product != null)
            {
                cartItem = new Entities.Cart()
                {
                    ProductId = productId,
                    UserId = userId,
                    Product = product,
                    Quantity = 1
                };

                await _dbContext.Cart.AddAsync(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task ClearCart(string userId)
        {
            var cart = await _dbContext.Cart
                 .Where(c => c.UserId == userId)
                 .ToListAsync();
            _dbContext.Cart.RemoveRange(cart);
            _dbContext.SaveChanges();
        }

        public async Task DeleteProduct(int productId, string userId)
        {
            var cart = await _dbContext.Cart
                .FirstOrDefaultAsync(c => c.ProductId == productId
                && c.UserId == userId);
            if (cart != null)
            {
                _dbContext.Cart.Remove(cart);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Cart[]> GetAllProductsFromCart(string userId)
        {
            var products = await _dbContext.Cart
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .ToArrayAsync();

            return _mapper.Map<Entities.Cart[], Domain.Cart[]>(products);
        }
    }
}
