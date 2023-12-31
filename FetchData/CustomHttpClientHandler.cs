namespace FetchData
{
    public class CustomHttpClientHandler : HttpClientHandler
    {
        private readonly TimeSpan delayBeforeNextRequest;

        public CustomHttpClientHandler(TimeSpan delayBeforeNextRequest)
        {
            this.delayBeforeNextRequest = delayBeforeNextRequest;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //request.Headers.Add("X-Auth-Token", authToken);
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {

                await Task.Delay(delayBeforeNextRequest);

                response = await base.SendAsync(request, cancellationToken);
            }

            return response;
        }
    }
}
