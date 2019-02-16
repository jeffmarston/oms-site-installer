using Microsoft.AspNetCore.SignalR;
using Eze.AdminConsole.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Eze.AdminConsole
{
    public class ServiceMgmtHub : Hub
    {

        public ServiceMgmtHub()
        {
            var list = ServiceUtils.GetAllEzeServices();
        }
        
        public async Task Request(string command, string serviceName)
        {
            await Clients.All.SendAsync("Response", command, serviceName);
        }

        public async Task GetServices()
        {
            //var list = ServiceUtils.GetAllEzeServices();
            await Clients.All.SendAsync("GetServices", "GetServices", "GetServices");
        }
    }
}
