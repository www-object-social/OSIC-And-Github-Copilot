using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OSIC.Shared.Layout.frame;
class Handler
{
    private List<Setting> Settings = new List<Setting>();
    public Task<Setting> Init(string Name)
    {
        var a = new Setting(Name);
        this.Settings.Add(a);
        return Task.FromResult(a);
    }
    private void Update() {
        var i = 0;
        foreach (var a in this.Settings.Where(x => x.IsOpen).OrderBy(x => x.Index))
        {
            i++;
            a.Index = i;
            Console.WriteLine(i.ToString());
            a.Update();
        }
    }
    public Task Open(string Name)
    {
      
        var s = this.Settings.Single(x => x.Name == Name);
        s.Index = this.Settings.Count+1;
        s.IsOpen = true;
        this.Update();
        return Task.CompletedTask;
    }
    public Task Close(string Name)
    {
        var s = this.Settings.Single(x => x.Name == Name);
        s.IsOpen = false;
        this.Update();
        return Task.CompletedTask;
    }
}
