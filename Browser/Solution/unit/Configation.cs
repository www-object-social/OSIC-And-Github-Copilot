using Microsoft.JSInterop;
using OSIC.Shared.Layout.process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Browser.Solution.unit;
class Configation : OSIC.Shared.Layout.unit.Configation, IAsyncDisposable
{
    public Shared.Unit.Type Type { get; private set; }
    private IJSObjectReference JSObject { get; set; } = null!;
    public Configation(IJSRuntime jsRuntime, OSIC.Shared.Layout.process.Handler PM) => _ = this.Constructor(jsRuntime, PM);
    private async Task Constructor(IJSRuntime jsRuntime, OSIC.Shared.Layout.process.Handler PM)
    {
        this.JSObject = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OSIC.Browser.Solution/unit/Configation.js");
        var UA = (await this.JSObject.InvokeAsync<string>("UserAgent")).ToLower();
        if (UA.ToLower().Contains("firefox/"))
            Type = Shared.Unit.Type.Firefox;
        else if (UA.ToLower().Contains("opr/"))
            Type = Shared.Unit.Type.Oprea;
        else if (UA.ToLower().Contains("edg/"))
            Type = Shared.Unit.Type.Edge;
        else if (UA.ToLower().Contains("chrome/"))
            Type = Shared.Unit.Type.Chrome;
        else if (UA.ToLower().Contains("safari/"))
            Type = Shared.Unit.Type.Safari;
        else
            Type = Shared.Unit.Type.Unknown;
        await PM.Set("OSIC.Shared.Layout.unit.Configation",Status.Done);
    }
    public double TimeZone => System.TimeZoneInfo.Local.BaseUtcOffset.TotalMinutes;
    public string TwoLetterISORegionName => System.Globalization.RegionInfo.CurrentRegion.TwoLetterISORegionName;
    public string TwoLetterISOLanguageName => System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    public async ValueTask DisposeAsync()
    {
        if (JSObject is null) return;
        await JSObject.DisposeAsync();
    }
}
