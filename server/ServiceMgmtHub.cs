using Microsoft.AspNetCore.SignalR;
using Eze.AdminConsole.Environment;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Eze.AdminConsole.Services;
using Eze.AdminConsole.Machines;

namespace Eze.AdminConsole
{
    public class ServiceMgmtHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var connId = Context.ConnectionId;
            Console.WriteLine("Connected = " + connId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connId = Context.ConnectionId;
            Console.WriteLine("Disconnected = " + connId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task Subscribe(string command, string serviceName)
        {
            var svcs = ServiceUtils.GetAllEzeServices("localhost");            
            ServiceWatcher.Init(Clients, "localhost", svcs);
            MachineWatcher.Init(Clients, new Topology());
            
            await Clients.All.SendAsync("subscribed", command, serviceName);
        }
    }
}
