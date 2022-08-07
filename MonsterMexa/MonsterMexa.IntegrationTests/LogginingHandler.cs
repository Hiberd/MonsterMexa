using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace MonsterMexa.IntegrationTests
{
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
