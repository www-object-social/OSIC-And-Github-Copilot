using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Software.Solution;
public static class Injection
{
    public static IServiceCollection OSICSoftwareSolution(this IServiceCollection Services, Shared.Project.Software Software)
    {
        Services.AddSingleton(x => new OSIC.Shared.Project.Configation(Software, Shared.Project.Type.Software));
        return Services;
    }
}
