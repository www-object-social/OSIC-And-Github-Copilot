using Microsoft.Extensions.DependencyInjection;
namespace OSIC.Shared.Layout;
public static class Injection
{
    public static IServiceCollection OSICSharedLayoutInjection(this IServiceCollection Services) {
        Services
            .AddSingleton<process.Handler>()
            .AddSingleton<network.Connection>()
            .AddSingleton<binding.Configation>()
            .AddSingleton<frame.Handler>()
            .AddSingleton<infomation.Handler>();
        return Services;
    }
}
