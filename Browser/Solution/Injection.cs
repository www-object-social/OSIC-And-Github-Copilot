using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Browser.Solution;

public static class Injection
{
    public static IServiceCollection OSICBrowserSolution(this IServiceCollection Services, Shared.Project.Software Software)
    {
        Services.AddSingleton(x => new OSIC.Shared.Project.Configation(Software, Shared.Project.Type.Browser));
        return Services;
    }
}