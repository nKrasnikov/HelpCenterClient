using Common.Models.Database;
using Common.Models.Request;
using Common.Models.Response;
using Refit;

namespace HelpCenter.Data.Http;

public interface IUserApi
{
    [Post("/register")]
    Task Register([Body] UserMainInfo user, CancellationToken cancellationToken = default);

    [Post("/login")]
    Task<TokenResponse> Login([Body] UserMainInfo user, CancellationToken cancellationToken = default);

    [Post("/refresh")]
    Task<TokenResponse> Refresh([Body] RefreshTokenRequest user, CancellationToken cancellationToken = default);

    [Get("/event")]
    Task<List<EventDto>> EventList(CancellationToken cancellationToken = default);

    [Get("/event/{id}")]
    Task<EventDto> GetEvent([AliasAs("id")] Guid id);

    [Post("/event/signIn")]
    Task<UserEventDto> SignInEvent([Body] SignInRequest requesst);

    [Post("/event")]
    Task<EventDto> CreateEvent([Body] EventCreateRequest request);

    [Post("/message")]
    Task<MessageDto> AddComment([Body] CreateMessageRequest request);

    [Post("/post")]
    Task<PostDto> AddPost([Body] CreatePostRequest request);
}

//public class UserData : IDisposable
//{

//    //public readonly static string deviceAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
//    public readonly static string deviceAddress = "http://213.171.4.235:5000";
//    private readonly static string address = deviceAddress + "/auth/";

//    public static readonly HttpClient _client = new HttpClient()
//    {
//        BaseAddress = new Uri(address)
//    };

//    private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };



//    public UserData()
//    {
//    }
//    }

//    public async Task<Options<>> Login(UserMainInfo user)
//    {


//        string json = JsonSerializer.Serialize(user);

//        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
//        HttpResponseMessage response = await _client.PostAsync("login", data);

//        string result = await response.Content.ReadAsStringAsync();

//        if (response.IsSuccessStatusCode)
//        {
//            var u = JsonSerializer.Deserialize<User>(result, CaseInsensitive);
//            UserAppInfo.UserData = u;
//            return null;
//        }

//        var er = JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive);
//        return er;
//    }

//    public async Task<InfoModel> Register(UserRegisterDto userDto)
//    {

//        string json = JsonSerializer.Serialize(userDto);

//        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

//        HttpResponseMessage response = await _client.PostAsync("register", data);

//        string result = await response.Content.ReadAsStringAsync();


//        if (response.IsSuccessStatusCode)
//        {
//            UserLoginDto loginDto = new(userDto.name, userDto.password);
//            return await Login(loginDto);
//        }

//        try
//        {
//            var er = JsonSerializer.Deserialize<InfoModel>(result, CaseInsensitive);
//            return er;
//        }
//        catch (Exception)
//        {
//            InfoModel er = new InfoModel();
//            er.message = result;
//            return er;
//        }
//    }



//    public void Dispose()
//    {
//        _client?.Dispose();
//    }
//}