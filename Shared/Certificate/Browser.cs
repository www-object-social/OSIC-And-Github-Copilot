using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Shared.Certificate;
public class Browser
{
    public string Key { get; }
    public Storage Storage { get; }
    public Browser(string Key, Storage Storage) {
        this.Key = Key;
        this.Storage = Storage;
    }
}
