using MonsterMexa.API.Contracts;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using Xunit.Abstractions;

namespace MonsterMexa.IntegrationTests.Tests
{
    public class ProductsControllerTests : BaseControllerTests
    {
        public ProductsControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async Task Create_ShouldReturnOk()
        {
            var request = new CreateProductRequest("Test", 26);

            var response = await Client.PostAsJsonAsync("Products", request);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_IsNotValidRequest_ShouldReturnBadRequest()
        {
            var request = new CreateProductRequest("Test", 24);

            var response = await Client.PostAsJsonAsync("Products", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            var response = await Client.GetAsync("Products");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_ShouldReturnOk()
        {
            var productId = await MakeProduct();

            var response = await Client.GetAsync($"Products/{productId}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_ShouldReturnOk()
        {
            var request = new UpdateProductRequest(1, "Test", 26);

            var response = await Client.PutAsJsonAsync("Products", request);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_IsNotValidRequest_ShouldReturnBadRequest()
        {
            var request = new UpdateProductRequest(1, "Test", 24);

            var response = await Client.PutAsJsonAsync("Products", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk()
        {
            var productId = await MakeProduct();

            var response = await Client.DeleteAsync($"Products/{productId}");

            response.EnsureSuccessStatusCode();
        }
    }
}
