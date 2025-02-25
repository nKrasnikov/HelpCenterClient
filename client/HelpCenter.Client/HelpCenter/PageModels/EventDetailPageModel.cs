using Common.Models.Database;
using Common.Models.Request;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpCenter.Data.Http;
using HelpCenter.Models;

namespace HelpCenter.PageModels
{
    public partial class EventDetailPageModel : ObservableObject, IQueryAttributable
    {
        private EventDto? _event;
        private readonly IUserApi api;
        private readonly ProjectRepository _projectRepository;
        private readonly TaskRepository _taskRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly TagRepository _tagRepository;
        private readonly ModalErrorHandler _errorHandler;


        // ============== fields



        [ObservableProperty]
        private bool _isCreator = false;

        [ObservableProperty]
        private string _postTitle = string.Empty;

        [ObservableProperty]
        private string _postContent = string.Empty;

        [ObservableProperty]
        private string _comment = string.Empty;

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        [ObservableProperty]
        private DateTimeOffset _startDate;

        [ObservableProperty]
        private string _phone = string.Empty;

        [ObservableProperty]
        private string _city = string.Empty;

        [ObservableProperty]
        private string _address = string.Empty;

        // ===================

        //[ObservableProperty]
        //private List<ProjectTask> _tasks = [];

        //[ObservableProperty]
        //private List<Category> _categories = [];

        //[ObservableProperty]
        //private Category? _category;

        //[ObservableProperty]
        //private int _categoryIndex = -1;

        [ObservableProperty]
        private List<Tag> _allTags = [];

        [ObservableProperty]
        private List<UserDto> _participant = [];

        [ObservableProperty]
        private List<PostDto> _posts = [];

        [ObservableProperty]
        private string _icon = FluentUI.ribbon_24_regular;

        [ObservableProperty]
        bool _isBusy;

        [ObservableProperty]
        private List<string> _icons =
        [
            FluentUI.ribbon_24_regular,
            FluentUI.ribbon_star_24_regular,
            FluentUI.trophy_24_regular,
            FluentUI.badge_24_regular,
            FluentUI.book_24_regular,
            FluentUI.people_24_regular,
            FluentUI.bot_24_regular
        ];

        public EventDetailPageModel(IUserApi api,
                                    ProjectRepository projectRepository,
                                    TaskRepository taskRepository,
                                    CategoryRepository categoryRepository,
                                    TagRepository tagRepository,
                                    ModalErrorHandler errorHandler)
        {
            this.api = api;
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _errorHandler = errorHandler;

        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("id"))
            {
                var id = Guid.Parse(query["id"]!.ToString()!);

                LoadData(id).FireAndForgetSafeAsync(_errorHandler);
            }
            else if (query.ContainsKey("refresh"))
            {
                //RefreshData().FireAndForgetSafeAsync(_errorHandler);
            }
            else
            {

            }
        }

        //private async Task RefreshData()
        //{
        //    if (_project.IsNullOrNew())
        //    {
        //        if (_project is not null)
        //            Tasks = new(_project.Tasks);
        //        return;
        //    }

        //    Tasks = await _taskRepository.ListAsync(_project.ID);
        //    _project.Tasks = Tasks;
        //}

        private async Task LoadData(Guid id)
        {
            try
            {
                IsBusy = true;

                try
                {
                    _event = await api.GetEvent(id);

                    //_project = await _projectRepository.GetAsync(id);

                }
                catch (Exception ex)
                {
                    _errorHandler.HandleError(ex);
                    return;
                }

                Title = _event.Title;
                Phone = _event.Phone;
                City = _event.City;
                Description = _event.Description;
                Address = _event.Address;
                IsCreator = _event.IsCreator;

                //Categories = await _categoryRepository.ListAsync();
                //Category = Categories?.FirstOrDefault(c => c.ID == _project.CategoryID);
                //CategoryIndex = Categories?.FindIndex(c => c.ID == _project.CategoryID) ?? -1;

                var allTags = await _tagRepository.ListAsync();
                AllTags = new(allTags);

                Participant = _event.Paticipants.Select(x => x.Participant).ToList();
                Posts = [.. _event.Posts];
            }
            catch (Exception e)
            {
                _errorHandler.HandleError(e);
            }
            finally
            {
                IsBusy = false;
                //OnPropertyChanged(nameof(HasCompletedTasks));
            }
        }

        [RelayCommand]
        private async Task SignIn()
        {
            try
            {
                SignInRequest req = new(_event!.Id);
                UserEventDto res = await api.SignInEvent(req);
                await LoadData(_event!.Id);
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(ex);
            }
        }

        [RelayCommand]
        private async Task AddComment(Guid postId)
        {
            if (string.IsNullOrEmpty(Comment))
            {
                return;
            }

            try
            {
                CreateMessageRequest request = new CreateMessageRequest(Comment, postId);
                MessageDto res = await api.AddComment(request);
                Comment = string.Empty;
                await LoadData(_event!.Id);
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(ex);
            }
        }
        
        [RelayCommand]
        private async Task AddPost()
        {
            try
            {
                CreatePostRequest request = new CreatePostRequest(PostTitle, PostContent, _event!.Id);
                PostDto res = await api.AddPost(request);
                PostTitle = string.Empty;
                PostContent = string.Empty;
                await LoadData(_event!.Id);
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(ex);
            }
        }

        //[RelayCommand]
        //private async Task TaskCompleted(ProjectTask task)
        //{
        //    await _taskRepository.SaveItemAsync(task);
        //    OnPropertyChanged(nameof(HasCompletedTasks));
        //}


        //[RelayCommand]
        //private async Task Save()
        //{
        //    if (_event is null)
        //    {
        //        _errorHandler.HandleError(
        //            new Exception("Project is null. Cannot Save."));

        //        return;
        //    }

        //    _event.Name = Name;
        //    _project.Description = Description;
        //    _project.CategoryID = Category?.ID ?? 0;
        //    _project.Icon = Icon ?? FluentUI.ribbon_24_regular;
        //    await _projectRepository.SaveItemAsync(_project);

        //    if (_project.IsNullOrNew())
        //    {
        //        foreach (var tag in AllTags)
        //        {
        //            if (tag.IsSelected)
        //            {
        //                await _tagRepository.SaveItemAsync(tag, _project.ID);
        //            }
        //        }
        //    }

        //    foreach (var task in _project.Tasks)
        //    {
        //        if (task.ID == 0)
        //        {
        //            task.ProjectID = _project.ID;
        //            await _taskRepository.SaveItemAsync(task);
        //        }
        //    }

        //    await Shell.Current.GoToAsync("..");
        //    await AppShell.DisplayToastAsync("Project saved");
        //}

        //[RelayCommand]
        //private async Task AddTask()
        //{
        //    if (_project is null)
        //    {
        //        _errorHandler.HandleError(
        //            new Exception("Project is null. Cannot navigate to task."));

        //        return;
        //    }

        //    // Pass the project so if this is a new project we can just add
        //    // the tasks to the project and then save them all from here.
        //    await Shell.Current.GoToAsync($"task",
        //        new ShellNavigationQueryParameters(){
        //            {TaskDetailPageModel.ProjectQueryKey, _project}
        //        });
        //}

        //[RelayCommand]
        //private async Task Delete()
        //{
        //    if (_project.IsNullOrNew())
        //    {
        //        await Shell.Current.GoToAsync("..");
        //        return;
        //    }

        //    await _projectRepository.DeleteItemAsync(_project);
        //    await Shell.Current.GoToAsync("..");
        //    await AppShell.DisplayToastAsync("Project deleted");
        //}

        //[RelayCommand]
        //private Task NavigateToTask(ProjectTask task) =>
        //    Shell.Current.GoToAsync($"task?id={task.ID}");

        //[RelayCommand]
        //private async Task ToggleTag(Tag tag)
        //{
        //    tag.IsSelected = !tag.IsSelected;

        //    if (!_project.IsNullOrNew())
        //    {
        //        if (tag.IsSelected)
        //        {
        //            await _tagRepository.SaveItemAsync(tag, _project.ID);
        //        }
        //        else
        //        {
        //            await _tagRepository.DeleteItemAsync(tag, _project.ID);
        //        }
        //    }

        //    AllTags = new(AllTags);
        //}

        //[RelayCommand]
        //private async Task CleanTasks()
        //{
        //    var completedTasks = Tasks.Where(t => t.IsCompleted).ToArray();
        //    foreach (var task in completedTasks)
        //    {
        //        await _taskRepository.DeleteItemAsync(task);
        //        Tasks.Remove(task);
        //    }

        //    Tasks = new(Tasks);
        //    OnPropertyChanged(nameof(HasCompletedTasks));
        //    await AppShell.DisplayToastAsync("All cleaned up!");
        //}
    }
}