using CommunityToolkit.Maui;
using HelpCenter.Data.Http;
using Microsoft.Extensions.Logging;
using Refit;
using Syncfusion.Maui.Toolkit.Hosting;

namespace HelpCenter;

public class Routes
{
    public const string EventDetailPage = "event";
    public const string EventCreatePage = "create-event";
    public const string ProjectDetailPage = "project";
    public const string TaskDetailPage = "task";
    public const string CreateAccountPage = "register";
}

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionToolkit()
            .ConfigureMauiHandlers(handlers =>
            {
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
                fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
            });

#if DEBUG
		builder.Logging.AddDebug();
		builder.Services.AddLogging(configure => configure.AddDebug());
#endif

        builder.Services.AddRefitClient<IUserApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(AppsettingsConsts.ApiUrl))
            .AddHttpMessageHandler<ApiHeaderHandler>();

        builder.Services.AddTransient<ApiHeaderHandler>();

        builder.Services.AddSingleton<ProjectRepository>();
        builder.Services.AddSingleton<TaskRepository>();
        builder.Services.AddSingleton<CategoryRepository>();
        builder.Services.AddSingleton<TagRepository>();
        builder.Services.AddSingleton<SeedDataService>();
        builder.Services.AddSingleton<ModalErrorHandler>();
        builder.Services.AddSingleton<MainPageModel>();
        builder.Services.AddSingleton<ProjectListPageModel>();
        builder.Services.AddSingleton<EventListPageModel>();
        builder.Services.AddSingleton<ManageMetaPageModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<ApiManager>();

        builder.Services.AddTransientWithShellRoute<EventDetailPage, EventDetailPageModel>(Routes.EventDetailPage);
        builder.Services.AddTransientWithShellRoute<EventCreatePage, EventCreatePageModel>(Routes.EventCreatePage);
        builder.Services.AddTransientWithShellRoute<ProjectDetailPage, ProjectDetailPageModel>(Routes.ProjectDetailPage);
        builder.Services.AddTransientWithShellRoute<TaskDetailPage, TaskDetailPageModel>(Routes.TaskDetailPage);
        builder.Services.AddTransientWithShellRoute<CreateAccountPage, CreateAccountViewModel>(Routes.CreateAccountPage);

        return builder.Build();
    }
}
