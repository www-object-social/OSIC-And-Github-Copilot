namespace OSIC.Shared.Layout.process;
public class Data
{
    public string Type { get; private set; }
    private Status _Status;
    public event Action Change = null!;
    public Status Status
    {
        get => _Status;
        internal set
        {
            _Status = value;
            this.Change.Invoke();
        }
    }
    internal Data(string Type, Status Status)
    {
        this.Type = Type;
        this._Status = Status;
    }
}