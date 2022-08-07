using MonsterMexa.API.Contracts;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using Xunit.Abstractions;

namespace MonsterMexa.IntegrationTests.Tests
{
    public class CartControllerTests : BaseControllerTests
    {
        public CartControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async Task AddProductToCart_ShouldReturnOk()
        {
            var productId = await MakeProduct();
            await MakeWarehouseProduct(productId);

            var response = await Client.PostAsJsonAsync($"Cart/{productId}", productId);

            response.EnsureSuccessStatusCode();
        }
    }
}
