namespace OSIC.Shared.Network;
public interface Handler
{
    public Status Status { get; }
    public event Action Change;
}
