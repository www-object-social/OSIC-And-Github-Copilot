namespace Hosting.folder;
public class Settings
{
    public string Address =
        #if DEBUG
            null!;
        #else
        "Some addreess";
        #endif
}