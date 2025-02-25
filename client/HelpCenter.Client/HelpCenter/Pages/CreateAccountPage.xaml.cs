

namespace HelpCenter.Pages;

public partial class CreateAccountPage : ContentPage
{
	public CreateAccountPage(CreateAccountViewModel model)
	{
		InitializeComponent();

        //MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
        //MainBackground.Aspect = Aspect.Fill;

        BindingContext = model;
    }
}