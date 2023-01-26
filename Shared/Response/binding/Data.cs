using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Shared.Response.binding;
public class Data
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string Key { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public OSIC.Shared.Certificate.Storage Storage { get; set; }
    public OSIC.Shared.Project.Region Region { get; set; }
    public OSIC.Shared.Project.Language Language { get; set; }
}
