using CommunityToolkit.Mvvm.Input;
using HelpCenter.Models;

namespace HelpCenter.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}