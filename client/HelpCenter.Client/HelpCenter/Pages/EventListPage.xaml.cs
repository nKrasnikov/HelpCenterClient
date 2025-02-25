namespace HelpCenter.Pages
{
    public partial class EventListPage : ContentPage
    {
        public EventListPage(EventListPageModel model)
        {
            BindingContext = model;
            InitializeComponent();
        }
    }
}