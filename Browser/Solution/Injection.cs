using Microsoft.Extensions.DependencyInjection;
namespace OSIC.Browser.Solution;
public static class Injection
{
    public static IServiceCollection OSICBrowserSolution(this IServiceCollection Services, Shared.Project.Software Software)
    {
        Services.AddSingleton(x => new OSIC.Shared.Project.Configation(Software, Shared.Project.Type.Browser));
        Services.AddSingleton<OSIC.Shared.Layout.network.Handler, network.Handler>();
        return Services;
    }
}