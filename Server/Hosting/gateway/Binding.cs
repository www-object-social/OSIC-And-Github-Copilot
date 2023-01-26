using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OSIC.Server.Hosting.server;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
namespace OSIC.Server.Hosting.gateway;
public partial class Binding:Hub
{

    private readonly IDbContextFactory<Database.Context> DBContextFactory;
    public readonly Shared.Project.Configation SharedProjectConfigation;
    public Binding(IHubContext<Binding> OutgoingHub, IEnumerable<OSIC.Server.Hosting.Automatic> Automatics, IDbContextFactory<Database.Context> DBContextFactory,Shared.Project.Configation SharedProjectConfigation)
    {
        this.SharedProjectConfigation = SharedProjectConfigation;
        this.DBContextFactory = DBContextFactory;
    }

}