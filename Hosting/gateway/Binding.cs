namespace OSIC.Server.Hosting.gateway;
public class Binding:Hub
{
    internal new IDbContextFactory<Database.Context> Context { get; init; } = null!;
    public Binding()
    {
        var a = this.Context!=null;
        var b = a;
    }
}
