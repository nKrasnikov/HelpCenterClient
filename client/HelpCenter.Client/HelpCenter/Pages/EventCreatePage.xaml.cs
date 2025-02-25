namespace HelpCenter.Pages
{
    public partial class EventCreatePage : ContentPage
    {
        public EventCreatePage(EventCreatePageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}