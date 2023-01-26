using Microsoft.AspNetCore.SignalR;
using OSIC.Server.Database;
using OSIC.Shared.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
namespace OSIC.Server.Hosting.binding;

public static class Exchange
{
    public static List<(Software Software, string MachineName,HubConnection Hub)> IncomingList = new();
    public static List<(Software Software, string MachineName, string ConnectionID)> OutgoingList = new();
    public static HubConnection IncomingHub(Software Software, string MachineName) => IncomingList.Single(x => x.Software == Software && x.MachineName == MachineName).Hub;
    public static string OutgoingConnectionID(Software Software, string MachineName) => OutgoingList.Single(x => x.Software == Software && x.MachineName == MachineName).ConnectionID;
}
