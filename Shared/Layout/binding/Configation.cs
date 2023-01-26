using Microsoft.AspNetCore.SignalR.Client;
using OSIC.Shared.Layout.network;
using OSIC.Shared.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Shared.Layout.binding;
class Configation
{
    private readonly network.Handler networkHandler;
    private readonly process.Handler processHandler;
    private readonly unit.Configation unitConfigation;
    private readonly network.Connection networkConnection;
    private readonly OSIC.Shared.Project.Configation SharedProjectConfigation;
    private readonly certificate.Handler certificateHandler;
    private Language _Language = Language.EN;
    public event Action LanguageChange = null!;
    public Language Language {
        get => _Language;
        set {
            if (_Language == value) return;
            _Language = value;
            _=this.networkConnection.Hub.InvokeAsync("SetLanguage", this.Language);
            LanguageChange?.Invoke();
;        }
    }
    private Region _Region;
    public event Action RegionChange = null!;
    public Region Region
    {
        get => _Region;
        set {
            if (_Region == value) return;
            _Region = value;
            _ = this.networkConnection.Hub.InvokeAsync("SetRegion", this.Region);
            RegionChange?.Invoke();
        }
    }
    private string _Key = null!;
    public Configation(network.Handler networkHandler, process.Handler processHandler, unit.Configation unitConfigation,network.Connection networkConnection,OSIC.Shared.Project.Configation SharedProjectConfigation,certificate.Handler certificateHandler)
    {
        this.certificateHandler = certificateHandler;
        this.SharedProjectConfigation = SharedProjectConfigation;
        this.unitConfigation = unitConfigation;
        this.networkConnection = networkConnection;
        (this.networkHandler = networkHandler).Change += Init; ;
        _ = (this.processHandler = processHandler).Install("OSIC.Shared.Layout.binding.Configation", PreInit, "OSIC.Shared.Layout.network.Connection", "OSIC.Shared.Layout.network.Handler", "OSIC.Shared.Layout.unit.Configation");
    
    }
    public void PreInit() {
        this.networkConnection.Hub.On<Language>("SetLanguage", (Language) => this.Language = Language);
        this.networkConnection.Hub.On<Region>("SetRegion", (Region) => this.Region = Region);
        Init();
    }
    private async Task Create() => await this.networkConnection.Hub.InvokeAsync("Create",SharedProjectConfigation.Software,SharedProjectConfigation.Type,unitConfigation.Type, unitConfigation.TwoLetterISORegionName,unitConfigation.TwoLetterISOLanguageName, unitConfigation.TimeZone);
    private async void Init()
    {
        if (networkHandler.Status is Status.Offline) return;
        await processHandler.Set("OSIC.Shared.Layout.binding.Configation", process.Status.Install);
        OSIC.Shared.Response.binding.Data Data= await this.certificateHandler.Have() ? await this.networkConnection.Hub.InvokeAsync<OSIC.Shared.Response.binding.Data>("Validation", SharedProjectConfigation.Software, SharedProjectConfigation.Type, unitConfigation.Type, unitConfigation.TwoLetterISORegionName, unitConfigation.TwoLetterISOLanguageName, unitConfigation.TimeZone, await this.certificateHandler.Get()): await this.networkConnection.Hub.InvokeAsync<OSIC.Shared.Response.binding.Data>("Create", SharedProjectConfigation.Software, SharedProjectConfigation.Type, unitConfigation.Type, unitConfigation.TwoLetterISORegionName, unitConfigation.TwoLetterISOLanguageName, unitConfigation.TimeZone);
        certificateHandler.Set(this._Key= Data.Key, Data.Storage);
        this._Language = Data.Language;
        this._Region = Data.Region;
        LanguageChange?.Invoke();
        await processHandler.Set("OSIC.Shared.Layout.binding.Configation", process.Status.Done);
    }
}
