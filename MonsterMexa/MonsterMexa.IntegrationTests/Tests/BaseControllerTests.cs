using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MonsterMexa.DataAccess.Postgres;
using Xunit.Abstractions;

namespace MonsterMexa.IntegrationTests.Tests
{
    public class BaseControllerTests
    {
        public BaseControllerTests(ITestOutputHelper outputHelper)
        {
            var application = new WebApplicationFactory<Program>();

            Client = application.CreateDefaultClient(new LogginingHandler(outputHelper));

            Fixture = new Fixture();

            DbContext = application.Services.CreateScope().ServiceProvider.GetRequiredService<MonsterMexaDbContext>();
        }

        protected HttpClient Client { get; }

        protected Fixture Fixture { get; }

        protected MonsterMexaDbContext DbContext { get; }

        protected async Task<int> MakeProduct()
        {
            var product = Fixture.Build<DataAccess.Postgres.Entities.Product>()
                .Without(p => p.Id)
                .Without(p => p.Category)
                .Without(p => p.CategoryId)
                .Create();

            DbContext.Products.Add(product);
            await DbContext.SaveChangesAsync();

            return product.Id;
        }

        protected async Task<int> MakeCategory()
        {
            var category = Fixture.Build<DataAccess.Postgres.Entities.Category>()
                .Without(c => c.Id)
                .Without(c => c.Products)
                .Create();

            DbContext.Categories.Add(category);
            await DbContext.SaveChangesAsync();

            return category.Id;
        }

        protected async Task<int> MakeWarehouseProduct(int productId)
        {
            var warehouseProduct = Fixture.Build<DataAccess.Postgres.Entities.WarehouseProduct>()
                .With(c => c.Quantity, 999999999)
                .With(c => c.ProductId, productId)
                .Without(c => c.Id)
                .Without(c => c.DeletedAt)
                .Without(c => c.Product)
                .Create();

            DbContext.Warehouse.Add(warehouseProduct);
            await DbContext.SaveChangesAsync();

            return warehouseProduct.Id;
        }
    }
}
