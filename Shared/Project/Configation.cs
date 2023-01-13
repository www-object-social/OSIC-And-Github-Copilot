namespace OSIC.Shared.Project;
public class Configation
{
    public readonly Software Software;
    public readonly Type Type;
    public Configation(Software Software, Type Type) { 
        this.Software= Software;
        this.Type = Type;
    }
}