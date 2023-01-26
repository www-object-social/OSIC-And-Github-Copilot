using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.ResponseCompression;
using OSIC.Server.Hosting;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession(x => x.Cookie.Name = "OSIC");
builder.Services.AddSignalR();
builder.Services.OSICServerHostingInjection(OSIC.Shared.Project.Software.InterConnecting);
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseWebAssemblyDebugging();
else
    app.UseHsts();
app.UseHttpsRedirection();
app.UseSession();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToPage("/index");
app.MapGet("/index.html", context =>{ context.Response.Redirect("/Index"); return Task.CompletedTask;});
#if !DEBUG
app.MapHub<OSIC.Server.Hosting.gateway.Binding>($"/{System.Environment.MachineName}.OSIC.Server.Hosting.gateway.Binding");
#else
app.MapHub<OSIC.Server.Hosting.gateway.Binding>($"/OSIC.Server.Hosting.gateway.Binding");
#endif
app.Run();