using Microsoft.AspNetCore.SignalR;
using Eze.AdminConsole.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Eze.AdminConsole
{
    public class ServiceMgmtHub : Hub
    {
        private ServiceWatcher _sw = null;

        public ServiceMgmtHub()
        {
        }

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
            await Clients.All.SendAsync("subscribed", command, serviceName);
        }

        public async Task GetServices()
        {
            //var list = ServiceUtils.GetAllEzeServices();
            await Clients.All.SendAsync("GetServices", "GetServices", "GetServices");
        }
    }
}
