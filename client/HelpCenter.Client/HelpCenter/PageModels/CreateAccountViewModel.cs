using Common.Models.Request;
using Common.Models.Response;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpCenter.Data.Http;
using Refit;

namespace HelpCenter.PageModels;

public partial class CreateAccountViewModel : ObservableObject
{
    private readonly IUserApi _api;
    private readonly ApiManager _apiManager;
    private readonly ModalErrorHandler _errorHandler;


    public CreateAccountViewModel(IUserApi api, ApiManager apiManager, ModalErrorHandler errorHandler)
    {
        _api = api;
        _apiManager = apiManager;
        _errorHandler = errorHandler;
    }

    [ObservableProperty] private string userLoginName = string.Empty;
    [ObservableProperty] private string userPassword = string.Empty;
    [ObservableProperty] private string userRepeatPassword = string.Empty;
    [ObservableProperty] private string loginMessage = string.Empty;
    [ObservableProperty] private bool turnLoginMessage = false;

    [ObservableProperty] private bool isBusy = false;
    [ObservableProperty] private bool isVisible = true;

    partial void OnIsBusyChanged(bool oldValue, bool newValue) => IsVisible = !newValue;

    [RelayCommand]
    private async Task RegisterNewUser()
    {
        if (UserPassword != UserRepeatPassword)
        {
            LoginMessage = "пароли не совпадают";
            TurnLoginMessage = true;
            return;
        }

        LoginMessage = string.Empty;
        IsBusy = true;
        UserMainInfo user = new UserMainInfo
        {
            email = UserLoginName,
            password = UserPassword,
        };

        try
        {
            await _api.Register(user);
            var tokens = await _api.Login(user);
            _apiManager.SetTokens(tokens);
            var route = "//events";
            await Shell.Current.GoToAsync(route);

        }
        catch (ApiException ex)
        {
            ErrorResponse? error = await ex.Err();
            LoginMessage = error?.GetError;
            _errorHandler.HandleError(new Exception(LoginMessage));
        }
        catch (Exception ex)
        {
            _errorHandler.HandleError(ex);
        }
        finally
        {
            IsBusy = false;
        }
    }



    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

}
