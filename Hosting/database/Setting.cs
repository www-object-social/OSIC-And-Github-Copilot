using System.Formats.Tar;

namespace OSIC.Server.Hosting.database;
static class Setting
{
    public static string Path => System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
    private static string CombinePathAndFile => System.IO.Path.Combine(Path, "Database.OSIC");
    private static bool PathAndFileExist => System.IO.File.Exists(CombinePathAndFile);
    public static string Connection => PathAndFileExist ? System.IO.File.ReadAllText(CombinePathAndFile) : throw new Exception(string.Join(System.Environment.NewLine, new[] { $"OSIC.Server.Hosting.database.Connection", $"Missing file:{CombinePathAndFile}", "Must contain Structured Query Language connection." }));
}