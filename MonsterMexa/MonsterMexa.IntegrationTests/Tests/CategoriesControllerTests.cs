using MonsterMexa.API.Contracts;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using Xunit.Abstractions;

namespace MonsterMexa.IntegrationTests.Tests
{
    public class CategoriesControllerTests : BaseControllerTests
    {
        public CategoriesControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async Task Create_ShouldReturnOk()
        {
            var request = new CreateCategoryRequest("Test");

            var response = await Client.PostAsJsonAsync("Categories", request);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_IsNotValidRequest_ShouldReturnBadRequest()
        {
            var request = new CreateCategoryRequest("Test123456789101");

            var response = await Client.PostAsJsonAsync("Categories", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            var response = await Client.GetAsync("Categories");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_ShouldReturnOk()
        {
            var request = new UpdateCategoryRequest(1, "Test");

            var response = await Client.PutAsJsonAsync("Categories", request);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_IsNotValidRequest_ShouldReturnBadRequest()
        {
            var request = new UpdateCategoryRequest(1, "Test123456789101");

            var response = await Client.PutAsJsonAsync("Categories", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk()
        {
            var categoryId = await MakeCategory();

            var response = await Client.DeleteAsync($"Categories/{categoryId}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task AddProduct_IsNotValidRequest_ShouldReturnBadRequest()
        {
            var categoryId = await MakeCategory();
            var productId = 0;

            var response = await Client.PostAsJsonAsync($"Categories/{categoryId}/{productId}", productId);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task AddProduct_ShouldReturnOk()
        {
            var categoryId = await MakeCategory();
            var productId = await MakeProduct();

            var response = await Client.PostAsJsonAsync($"Categories/{categoryId}/{productId}", productId);

            response.EnsureSuccessStatusCode();
        }
    }
}
