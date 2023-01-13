using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace OSIC.Server.Hosting;
public static class Injection
{
    public static IServiceCollection OSICServerHostingInjection(this IServiceCollection Services) {
        Services.AddDbContextFactory<Database.Context>(x => x.UseSqlServer(database.Setting.Connection).UseLazyLoadingProxies(), ServiceLifetime.Singleton);
        return Services;
    }
}
