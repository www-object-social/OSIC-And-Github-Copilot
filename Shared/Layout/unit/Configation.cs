using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OSIC.Shared.Layout.unit;
public interface Configation
{
    public OSIC.Shared.Unit.Type Type { get; }
    public double TimeZone { get; }
    public string TwoLetterISORegionName { get; }
    public string TwoLetterISOLanguageName { get; }
}