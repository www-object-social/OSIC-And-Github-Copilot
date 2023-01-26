namespace OSIC.Software.Solution;
public static class Injection
{
    public static IServiceCollection OSICSoftwareSolution(this IServiceCollection Services, Shared.Project.Software Software)
    {
        Services
            .AddSingleton(x => new OSIC.Shared.Project.Configation(Software, Shared.Project.Type.Software))
            .AddSingleton<OSIC.Shared.Layout.network.Handler, network.Handler>()
            .AddSingleton<OSIC.Shared.Layout.unit.Configation, unit.Configation>()
            .AddSingleton<OSIC.Shared.Layout.certificate.Handler, certificate.Handler>();
        return Services;
    }
}
