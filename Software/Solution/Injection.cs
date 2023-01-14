namespace OSIC.Software.Solution;
public static class Injection
{
    public static IServiceCollection OSICSoftwareSolution(this IServiceCollection Services, Shared.Project.Software Software)
    {
        Services.AddSingleton(x => new OSIC.Shared.Project.Configation(Software, Shared.Project.Type.Software));
        Services.AddSingleton<OSIC.Shared.Layout.network.Handler, network.Handler>();
        return Services;
    }
}
