using System.Net.Http.Json;
using System.Text.Json;

namespace MainTz.Infrastructure.Services
{
    public class RestClient<TRequestContent, TResponse> : IDisposable where TResponse : class
    {
        private HttpClient _httpClient;
        protected readonly string _url;

        public RestClient(string url)
        {
            _url = url;
            _httpClient = CreateHttpClient(_url);
        }

        public async Task<TResponse> GetAsync(TRequestContent requestContent, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _url);
            request.Content = new StringContent(content: JsonSerializer.Serialize(requestContent));

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        protected virtual HttpClient CreateHttpClient(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
            return _httpClient;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
