using Microsoft.AspNetCore.SignalR.Client;
using OSIC.Shared.Project;

namespace OSIC.Shared.Layout.network;
class Connection
{
    private readonly Handler Handler;
    public HubConnection Hub { get; private set; } = null!;
    private process.Handler processHandler;
    public event Action Change = null!;
    private readonly HttpClient HttpClient;
    public Connection(Handler Handler, process.Handler processHandler,HttpClient HttpClient)
    {
        this.HttpClient = HttpClient;
        this.Handler = Handler;
        _ = (this.processHandler = processHandler).Install("OSIC.Shared.Layout.network.Connection", Init, "OSIC.Shared.Layout.network.Handler");
        this.Handler.Change += Init;
    }
    private async void Init()
    {
        if (Handler.Status is Status.Online && this.Hub == null) {
#if !DEBUG
            this.Hub = new HubConnectionBuilder().WithUrl(await HttpClient.GetStringAsync("/OSIC.Server.Hosting.server.SignalR")).WithAutomaticReconnect().Build();
#else
            this.Hub = new HubConnectionBuilder().WithUrl($"{HttpClient.BaseAddress}OSIC.Server.Hosting.gateway.Binding").WithAutomaticReconnect().Build();
#endif
        }
        if (Handler.Status is Status.Offline|| Hub ==null|| Hub.State is HubConnectionState.Connected ) return;
        await this.processHandler.Set("OSIC.Shared.Layout.network.Connection", process.Status.Install);
        await this.Hub.StartAsync();
        if (Hub.State is HubConnectionState.Connected) {
            Change?.Invoke();
            await this.processHandler.Set("OSIC.Shared.Layout.network.Connection", process.Status.Done);
        }
    }

}
