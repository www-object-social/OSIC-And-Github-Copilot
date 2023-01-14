using Microsoft.JSInterop;
using OSIC.Shared.Layout.network;

namespace OSIC.Browser.Solution.network;
class Handler : OSIC.Shared.Layout.network.Handler,IAsyncDisposable
{
    private DotNetObjectReference<Handler> DotNetObject { get; set; } = null!;
    public event Action Change = null!;
    public Status Status { get; private set; }
    private IJSObjectReference JSObject { get; set; } = null!;
    public Handler(IJSRuntime jsRuntime, OSIC.Shared.Layout.process.Handler PM) => _ = this.Constructor(jsRuntime, PM);
    private async Task Constructor(IJSRuntime jsRuntime, OSIC.Shared.Layout.process.Handler PM)
    {
        this.JSObject = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OSIC.Browser.Solution/network/Handler.js");
        this.Status = (await this.JSObject.InvokeAsync<bool>("Handler", this.DotNetObject = DotNetObjectReference.Create(this))) ? Status.Online : Status.Offline;
        await PM.Set("OSIC.Shared.Layout.network.Handler", OSIC.Shared.Layout.process.Status.Done);
    }
    [JSInvokable]
    public void JSChange(bool Status)
    {
        this.Status = Status ? OSIC.Shared.Layout.network.Status.Online : OSIC.Shared.Layout.network.Status.Offline;
        this.Change();
    }
    public async ValueTask DisposeAsync()
    {
        if (JSObject is null) return;
        await JSObject.DisposeAsync();
        if (DotNetObject is null) return;
        DotNetObject.Dispose();
    }
}