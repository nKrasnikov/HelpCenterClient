#nullable disable
using Common.Models.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpCenter.Data;
using HelpCenter.Data.Http;
using HelpCenter.Models;
using HelpCenter.Services;

namespace HelpCenter.PageModels
{
    public partial class EventListPageModel : ObservableObject
    {

        [ObservableProperty]
        private List<EventDto> _projects = [];
        private readonly IUserApi api;

        [ObservableProperty]
        private string _icon = FluentUI.people_24_regular;

        public EventListPageModel(
            IUserApi api,
            ProjectRepository projectRepository)
        {
            this.api = api;
            //_projectRepository = projectRepository;
        }

        [RelayCommand]
        private async Task Appearing()
        {
            try
            {
                Projects = await api.EventList();  //await _projectRepository.ListAsync();
            }
            catch (Exception ex)
            {
                
            }
        }

        [RelayCommand]
        Task NavigateToProject(EventDto e)
            => Shell.Current.GoToAsync($"{Routes.EventDetailPage}?id={e.Id}");

        [RelayCommand]
        async Task AddEvent()
        {
            await Shell.Current.GoToAsync(Routes.EventCreatePage);
        }
    }
}