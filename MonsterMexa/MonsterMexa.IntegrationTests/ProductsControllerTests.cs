using Microsoft.AspNetCore.Mvc.Testing;
using MonsterMexa.API.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace MonsterMexa.IntegrationTests
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

            var response = await client.PostAsJsonAsync("Products", request);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_IsNotValidRequest_ShouldReturnBadRequest()
        {
            var request = new CreateProductRequest("Test", 26);

            var response = await client.PostAsJsonAsync("Products", request);
            var response2 = await client.GetAsync("Products");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


    }

    public class BaseControllerTests
    {
        public BaseControllerTests(ITestOutputHelper outputHelper)
        {
            var application = new WebApplicationFactory<Program>();

            client = application.CreateDefaultClient(new LogginingHandler(outputHelper));
        }

        protected HttpClient client { get; }
    }

    public class LogginingHandler : DelegatingHandler
    {
        private readonly ITestOutputHelper _outputHelper;

        public LogginingHandler(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _outputHelper.WriteLine($"{request.Method} {request.RequestUri}");

            await PrintContent(request.Content);

            var response = await base.SendAsync(request, cancellationToken);

            await PrintContent(response.Content);

            return response;
        }

        private async Task PrintContent(HttpContent? content)
        {
            if (content == null)
            {
                return;
            }

            var stringContent = await content.ReadAsStringAsync();

            try
            {
                _outputHelper.WriteLine(JToken.Parse(stringContent).ToString());
            }
            catch
            {
                _outputHelper.WriteLine(stringContent);
            }
        }
    }
}
