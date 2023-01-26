using OSIC.Browser.InterConnecting;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OSIC.Shared.Layout;
using OSIC.Browser.Solution;
var builder = WebAssemblyHostBuilder.CreateDefault(args); 
builder.RootComponents.Add<App>("main");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.OSICSharedLayoutInjection().OSICBrowserSolution(OSIC.Shared.Project.Software.InterConnecting);
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();