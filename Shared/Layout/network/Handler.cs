namespace OSIC.Shared.Layout.network;
public interface Handler
{
    public Status Status { get; }
    public event Action Change;
}
