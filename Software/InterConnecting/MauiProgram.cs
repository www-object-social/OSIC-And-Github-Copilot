using Microsoft.Extensions.Logging;
using OSIC.Shared.Layout;
using OSIC.Software.Solution;

namespace InterConnecting;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.OSICSharedLayoutInjection().OSICSoftwareSolution(OSIC.Shared.Project.Software.InterConnecting);
        builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://interconnecting.info/") });
        #if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		        builder.Logging.AddDebug();
        #endif
        return builder.Build();
    }
}