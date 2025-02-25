
using Common.Models.Response;

namespace HelpCenter.Data.Http;
public class ApiManager
{
    public string BearerToken { get; set; }
    public string RefreshToken { get; set; }

    public void SetTokens(TokenResponse res)
    {
        BearerToken = res.accessToken;
        RefreshToken = res.refreshToken;
    }
}
