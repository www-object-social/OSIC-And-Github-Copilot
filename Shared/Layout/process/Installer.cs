namespace OSIC.Shared.Layout.process;
class Installer
{
    public Action Start { get; private set; }
    public string[] Types { get; private set; }
    internal Installer(Action Start, string[] Types)
    {
        this.Start = Start;
        this.Types = Types;
    }
}