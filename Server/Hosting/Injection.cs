using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace OSIC.Server.Hosting;
public static class Injection
{
    public static IServiceCollection OSICServerHostingInjection(this IServiceCollection Services, Shared.Project.Software Software) {
        Services.AddDbContextFactory<Database.Context>(x => x.UseSqlServer(database.Setting.Connection).UseLazyLoadingProxies(), ServiceLifetime.Singleton);
        Services.AddSingleton(x => new OSIC.Shared.Project.Configation(Software, Shared.Project.Type.Server))
            .AddScoped<Automatic, gateway.Automatic>();
        return Services;
    }
}
