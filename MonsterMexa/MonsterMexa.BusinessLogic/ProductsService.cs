﻿using CSharpFunctionalExtensions;
using MonsterMexa.Domain;

namespace MonsterMexa.BusinessLogic
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsPepository _pepository;

        public ProductsService(IProductsPepository pepository)
        {
            _pepository = pepository;
        }

        public async Task<int> CreateProduct(Product product)
        {
            return await _pepository.Add(product);
        }

        public async Task<bool> Delete(int productId)
        {
            return await _pepository.Delete(productId);
        }

        public async Task<Product[]> GetAllProducts()
        {
            return await _pepository.GetAll();
        }

        public async Task<Product[]> GetByCategory(int categoryId)
        {
            return await _pepository.GetByCategory(categoryId);
        }

        public async Task<Result<Product>> GetById(int productId)
        {
            var product = await _pepository.GetById(productId);

            if (product == null)
            {
                return Result.Failure<Product>("The entered ID does not exist");
            }

            return product;
        }

        public async Task Update(Product product)
        {
            await _pepository.Update(product);
        }
    }
}
