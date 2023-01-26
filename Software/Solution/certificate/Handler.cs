using Microsoft.Maui.Controls.PlatformConfiguration;
using OSIC.Shared.Certificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Software.Solution.certificate;
class Handler : OSIC.Shared.Layout.certificate.Handler
{
    private string Combine(string Path) => System.IO.Path.Combine(Path, "Key.OSIC");
    private string Local => this.Combine(FileSystem.Current.AppDataDirectory);
    private string Temporary => this.Combine(FileSystem.Current.CacheDirectory);
    public Task<bool> Have() => Task.FromResult<bool>(File.Exists(Local) || File.Exists(Temporary));
    public Task<string> Get() => File.Exists(Local) ? Task.FromResult<string>(File.ReadAllText(Local)) : Task.FromResult<string>(File.ReadAllText(Temporary));
    public void Set(string Key, Storage Storage)
    {
        byte[] UTF8E = new UTF8Encoding(true).GetBytes(Key);
        using var fs = File.OpenWrite(Storage is OSIC.Shared.Certificate.Storage.Local ? Local : Temporary);
        fs.Write(UTF8E, 0, UTF8E.Length);
        if (Storage is OSIC.Shared.Certificate.Storage.Local)
        {
            if (File.Exists(Temporary))
                File.Delete(Temporary);
        }
        else if (File.Exists(Local))
            File.Delete(Local);
    }
}
