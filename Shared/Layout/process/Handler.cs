using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Shared.Layout.process;
public class Handler
{
    public event Action Change = null!;
    private Status _Status = Status.StartUp;
    public Status Status
    {
        get => _Status;
        private set
        {
            if (_Status == value || (_Status == Status.StartUp && value is Status.Install or Status.Download) || (_Status == Status.Install && value is Status.Download)) return;
            _Status = value;
            this.Change?.Invoke();
        }
    }
    private List<Data> Datas = new List<Data>();
    public Task Set(string Type, Status Status)
    {
        if (this.Datas.Any(x => x.Type == Type))
        {
            var D = this.Datas.Single(x => x.Type == Type);
            D.Status = Status;
            return Task.CompletedTask;
        }
        var ND = new Data(Type, Status);
        ND.Change += ND_Change;
        this.Datas.Add(ND);
        this.ND_Change();
        return Task.CompletedTask;
    }
    private async void ND_Change()
    {
        if (this.Installers.Any())
            await this.RunInstaller();
        this.Status = Datas.Any(x => x.Status == Status.Install) ? Status.Install : Datas.Any(x => x.Status == Status.Download) ? Status.Download : Status.Done;
    }
    public Data Get(string Type) => this.Datas.Any(x => x.Type == Type) ? this.Datas.Single(x => x.Type == Type) : new Data(Type, Status.Install);
    private List<Installer> Installers = new List<Installer>();
    private Task RunInstaller()
    {
        if (!this.Installers.Any()) return Task.CompletedTask;
        var A = this.Installers.Where(x => { return x.Types.All(z => Get(z).Status == Status.Done); });
        this.Installers = this.Installers.Where(x => { return !x.Types.All(z => Get(z).Status == Status.Done); }).ToList();
        if (!A.Any()) return Task.CompletedTask;
        foreach (var I in A)
            I.Start();
        return Task.CompletedTask;
    }
    public async Task Install(string Type, Action Start, params string[] Types)
    {
        await Set(Type,Status.Install);
        this.Installers.Add(new Installer(Start, Types));
        await this.RunInstaller();
    }
}
