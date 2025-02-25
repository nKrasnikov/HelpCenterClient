using System.Net.Http.Headers;

namespace HelpCenter.Data.Http;

internal class ApiHeaderHandler(ApiManager apiManager) : DelegatingHandler
{
    private readonly ApiManager _apiManager = apiManager;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                 CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(_apiManager.BearerToken))
        {
            string token = _apiManager.BearerToken;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        var result =  await base.SendAsync(request, cancellationToken);
        return result;
    }
}
