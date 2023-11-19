using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class CustomHttpClient
{
    private readonly HttpClient _httpClient;

    private readonly TimeSpan delayBeforeNextRequest = TimeSpan.FromSeconds(60);

    public CustomHttpClient(string baseAddress, string token)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseAddress)
        };

        _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", token);
    }

    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

        if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            await Task.Delay(delayBeforeNextRequest);

            response = await _httpClient.GetAsync(requestUri);
        }


        return response;
    }

}