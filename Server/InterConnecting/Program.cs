using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using OSIC.Server.Hosting;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddSignalR();
builder.Services.OSICServerHostingInjection();
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseWebAssemblyDebugging();
else
    app.UseHsts();
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToPage("/index");
app.MapHub<OSIC.Server.Hosting.gateway.Binding>("/OSIC.Server.Hosting.gateway.Binding");
app.Run();