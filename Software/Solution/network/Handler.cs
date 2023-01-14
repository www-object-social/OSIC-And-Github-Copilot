using OSIC.Shared.Layout.network;
namespace OSIC.Software.Solution.network;
class Handler : OSIC.Shared.Layout.network.Handler
{
    public Status Status { get; private set; }
    public event Action Change = null!;
    public Handler(OSIC.Shared.Layout.process.Handler PM)
    {
        Connectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        this.Status = Connectivity.Current.NetworkAccess == NetworkAccess.Internet ? Status.Online : Status.Offline;
        _ = PM.Set("OSIC.Shared.Layout.network.Handler", OSIC.Shared.Layout.process.Status.Done);
    }
    private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        this.Status = Connectivity.Current.NetworkAccess == NetworkAccess.Internet ? Status.Online : Status.Offline;
        this.Change();
    }
}