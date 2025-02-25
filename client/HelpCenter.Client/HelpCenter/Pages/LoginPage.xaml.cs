
namespace HelpCenter.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel model)
    {
        InitializeComponent();

        BindingContext = model;
    }
}