using Microsoft.AspNetCore.Mvc;
using OSIC.Shared.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Server.Hosting.server;
[ApiController]
public class Controller : ControllerBase
{
    [HttpGet("/OSIC.Server.Hosting.server.SignalR")]
    public string SignalR() => $"https://{SharedProjectConfigation.Software.Domain()}/{System.Environment.MachineName}.OSIC.Server.Hosting.gateway.Binding".ToLower();
    private readonly OSIC.Shared.Project.Configation SharedProjectConfigation;
    public Controller(OSIC.Shared.Project.Configation SharedProjectConfigation) {
        this.SharedProjectConfigation = SharedProjectConfigation;
    }
}
