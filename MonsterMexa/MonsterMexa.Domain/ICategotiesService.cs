﻿using CSharpFunctionalExtensions;

namespace MonsterMexa.Domain
{
    public interface ICategotiesService
    {
        public Task<int> AddCategory(Category category);

        public Task<Result<bool>> AddProduct(int productId, int categoryId);

        public Task Delete(int categoryId);

        public Task<Category[]> GetAllCategories();

        public Task<Category> GetById(int categoryId);

        public Task Update(Category category);
    }
}
