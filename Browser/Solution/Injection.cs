using Microsoft.Extensions.DependencyInjection;
namespace OSIC.Browser.Solution;
public static class Injection
{
    public static IServiceCollection OSICBrowserSolution(this IServiceCollection Services, Shared.Project.Software Software)
    {
        Services
            .AddSingleton(x => new OSIC.Shared.Project.Configation(Software, Shared.Project.Type.Browser))
            .AddSingleton<OSIC.Shared.Layout.network.Handler, network.Handler>()
            .AddSingleton<OSIC.Shared.Layout.unit.Configation, unit.Configation>()
            .AddSingleton<OSIC.Shared.Layout.certificate.Handler,certificate.Handler>();
        return Services;
    }
}