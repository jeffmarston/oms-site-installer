using Microsoft.AspNetCore.SignalR;
using SignalRSimpleChat.ServiceMgmt;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SignalRSimpleChat
{
    public class ServiceMgmtHub : Hub
    {
        private ServiceManager svcMgr = null;

        public ServiceMgmtHub()
        {
            svcMgr = new ServiceManager();
        }
        
        public async Task Request(string command, string serviceName)
        {
            try
            {
                if (command == "start")
                {
                    svcMgr.StartService(serviceName);
                }
                if (command == "stop")
                {
                    svcMgr.StopService(serviceName);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            //serviceName = "eze." + serviceName.ToLower() + ".svc";
            await Clients.All.SendAsync("Response", command, serviceName);
        }

        public async Task GetServices()
        {
            var list = svcMgr.QueryServices();
            await Clients.Caller.SendAsync("AllServices", list);
        }
    }
}
