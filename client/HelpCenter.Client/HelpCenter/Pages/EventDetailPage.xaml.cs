namespace HelpCenter.Pages
{
    public partial class EventDetailPage : ContentPage
    {
        public EventDetailPage(EventDetailPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}