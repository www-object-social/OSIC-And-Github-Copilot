using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Shared.Layout.frame;
class Setting
{
    public readonly string Name;
    public int Index { get; internal set; }   
    public bool IsOpen { get; internal set; }
    public Setting(string Name) {
        this.Name = Name;
    }
    public event Action Change = null!;
    internal void Update() => this.Change?.Invoke();
}
 