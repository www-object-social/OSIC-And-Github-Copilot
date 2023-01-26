using OSIC.Shared.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIC.Server.Hosting.binding;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
namespace OSIC.Server.Hosting.gateway;
public partial class Binding
{
    public Task Exchange_Start(Software Software, string MachineName) {
        OSIC.Server.Hosting.binding.Exchange.OutgoingList.Add((Software, MachineName, Context.ConnectionId));
        return Task.CompletedTask;
    }
    private async Task<binding.exchange.Type> Exchange_Is((Software Software, string MachineName) Data)
    {
        if (binding.Exchange.OutgoingList.Any(x => x.Software == Data.Software && x.MachineName == Data.MachineName))
            return binding.exchange.Type.Outgoing;
        if (binding.Exchange.IncomingList.Any(x => x.Software == Data.Software && x.MachineName == Data.MachineName))
            return binding.exchange.Type.Incoming;
        var H = new HubConnectionBuilder().WithUrl($"{Data.Software.Domain()}/{Data.MachineName}.OSIC.Server.Hosting.gateway.Binding").Build();
        await H.StartAsync();
        //add function up to 10
        H.On<string, string>("Exchange_Forward0", async (c, m) => await Exchange_Forward0(c, m));
        H.On<string, string, object>("Exchange_Forward1", async (c, m, o) => await Exchange_Forward1(c, m, o));
        H.On<string, string, object,object>("Exchange_Forward2", async (c, m, o,o1) => await Exchange_Forward2(c, m, o,o1));

        await H.InvokeAsync("Exchange_Start", Data.Software, System.Environment.MachineName);
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        H.Closed += async (s) => binding.Exchange.IncomingList.Remove(binding.Exchange.IncomingList.Single(x => x.Software == Data.Software && x.MachineName == Data.MachineName));
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        binding.Exchange.IncomingList.Add((Data.Software, Data.MachineName, H));
        return binding.exchange.Type.Incoming;
    }
    private async Task Exchange_Forward2(string ConnectionID, string Method, object obj0, object obj1) => await Clients.Client(ConnectionID).SendAsync(Method, obj0, obj1);
    private async Task Exchange_Send2((Software Software, string MachineName) Host, string ConnectionID, string Method, object obj0, object obj1)
    {
        if (SharedProjectConfigation.Software == Host.Software && System.Environment.MachineName == Host.MachineName)
            await Exchange_Forward2(ConnectionID, Method, obj0,  obj1);
        else
            switch (await Exchange_Is(Host))
            {
                case binding.exchange.Type.Incoming:
                    await OSIC.Server.Hosting.binding.Exchange.IncomingHub(Host.Software, Host.MachineName).SendAsync($"Exchange_Forward2", ConnectionID, Method, obj0, obj1);
                    break;
                default:
                    await Clients.Client(OSIC.Server.Hosting.binding.Exchange.OutgoingList.Single(x => x.Software == Host.Software && x.MachineName == Host.MachineName && x.ConnectionID == ConnectionID).ConnectionID).SendAsync($"Exchange_Forward2", ConnectionID, Method, obj0, obj1);
                    break; ;
            }
    }
    private async Task Exchange_Forward1(string ConnectionID, string Method, object obj0) => await Clients.Client(ConnectionID).SendAsync(Method, obj0);
    private async Task Exchange_Send1((Software Software, string MachineName) Host,string ConnectionID,string Method,object obj0) {
        if (SharedProjectConfigation.Software == Host.Software && System.Environment.MachineName == Host.MachineName)
            await Exchange_Forward1(ConnectionID,Method, obj0);
        else
            switch (await Exchange_Is(Host)) {
            case binding.exchange.Type.Incoming:
                await OSIC.Server.Hosting.binding.Exchange.IncomingHub(Host.Software, Host.MachineName).SendAsync($"Exchange_Forward1", ConnectionID, Method, obj0);
                break;
            default:
                await Clients.Client(OSIC.Server.Hosting.binding.Exchange.OutgoingList.Single(x => x.Software == Host.Software && x.MachineName == Host.MachineName && x.ConnectionID == ConnectionID).ConnectionID).SendAsync($"Exchange_Forward1", ConnectionID, Method, obj0);
                break;;
        }
    }
    private async Task Exchange_Forward0(string ConnectionID, string Method) => await Clients.Client(ConnectionID).SendAsync(Method);
    private async Task Exchange_Send0((Software Software, string MachineName) Host, string ConnectionID, string Method)
    {
        if (SharedProjectConfigation.Software == Host.Software && System.Environment.MachineName == Host.MachineName)
            await Exchange_Forward0(ConnectionID, Method);
        else
            switch (await Exchange_Is(Host))
            {
                case binding.exchange.Type.Incoming:
                    await OSIC.Server.Hosting.binding.Exchange.IncomingHub(Host.Software, Host.MachineName).SendAsync($"Exchange_Forward0", ConnectionID, Method);
                    break;
                default:
                    await Clients.Client(OSIC.Server.Hosting.binding.Exchange.OutgoingList.Single(x => x.Software == Host.Software && x.MachineName == Host.MachineName && x.ConnectionID == ConnectionID).ConnectionID).SendAsync($"Exchange_Forward0", ConnectionID, Method);
                    break; ;
            }
    }



    public async Task Exchange_Send((Software Software, string MachineName) Host, string ConnectionID, string Method,params object[] objects) {
        switch (objects.Count()) {
            case 0:
                await Exchange_Send0(Host, ConnectionID, Method);
                break;
            case 1:
                await Exchange_Send1(Host, ConnectionID, Method, objects[0]);
                break;
            case 2:
                await Exchange_Send2(Host, ConnectionID, Method, objects[0], objects[1]);
                break;
            default:throw new Exception($"Exchange_Send{objects.Count()}");
        }
    }
}