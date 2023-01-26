using OSIC.Shared.Layout.process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Software.Solution.unit;
class Configation : OSIC.Shared.Layout.unit.Configation
{
    public Shared.Unit.Type Type =>
#if ANDROID
Shared.Unit.Type.Android;
#elif IOS
Shared.Unit.Type.IOS;
#elif WINDOWS
Shared.Unit.Type.Windows;
#elif MACCATALYST
Shared.Unit.Type.Mac;
#else
Shared.Unit.Type.Unknown;
#endif
    public string TwoLetterISORegionName => System.Globalization.RegionInfo.CurrentRegion.TwoLetterISORegionName;
    public string TwoLetterISOLanguageName => System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    public double TimeZone => System.TimeZoneInfo.Local.BaseUtcOffset.TotalMinutes;
    public Configation(OSIC.Shared.Layout.process.Handler PM) =>  _ = PM.Set("OSIC.Shared.Layout.unit.Configation", Status.Done);
}
