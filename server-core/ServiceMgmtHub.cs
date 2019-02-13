using Microsoft.AspNetCore.SignalR;
using SignalRSimpleChat.ServiceMgmt;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Wask.Lib.Utils;

namespace SignalRSimpleChat
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
            var list = ServiceUtils.GetAllEzeServices();
        }
    }
}
