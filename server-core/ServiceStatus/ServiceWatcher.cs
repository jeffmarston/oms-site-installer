using Eze.AdminConsole.Utils;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Threading;
using Eze.AdminConsole.SignalR;
using Microsoft.AspNetCore.SignalR;
using SignalRSimpleChat;

namespace Eze.AdminConsole.Model
{
    public class ServiceWatcher
    {
        private List<Service> _lastServiceStates = new List<Service>();
        private ServerStats _lastServerStats = new ServerStats();

        private Timer _pollingTimer;
        private static IHubContext<ServiceMgmtHub> _context;


        internal static void Init(List<Service> services, IHubContext<ServiceMgmtHub> context)
        {
            if (_instance == null)
            {
                _context = context;
                _instance = new ServiceWatcher(services);
            }
        }

        private static ServiceWatcher _instance = null;
        private const int TimerIntervalMs = 1000;

        private ServiceWatcher(List<Service> servicesToMonitor)
        {
            _lastServiceStates = servicesToMonitor;
            _pollingTimer = new Timer(DoPoll, null, 0, TimerIntervalMs);
        }



        private void DoPoll(object state)
        {
            var newSvrStats = ServiceUtils.GetSystemInfo();// only works with 1 second interval;
            newSvrStats.cpuPercent = 100 - (newSvrStats.idleCpuTime - _lastServerStats.idleCpuTime) / (TimeSpan.TicksPerMillisecond * 10); // only works with 1 second interval;
            _lastServerStats = newSvrStats;
            _context.Clients.All.SendAsync("Response", "serverStats", newSvrStats);

            foreach (var prevState in _lastServiceStates)
            {
                var newState = ServiceUtils.GetService(new ServiceController(prevState.name));

                if (newState.pid > 0)
                {
                    var cpuTime = Math.Round((newState.cpuTimeSpan.TotalSeconds - prevState.cpuTimeSpan.TotalSeconds) / 10.0, 2);
                    prevState.cpuTimeSpan = newState.cpuTimeSpan;
                    // Console.WriteLine($"Publish: [{prevState.name}]. diffTime = {cpuTime} %");
                }

                if (prevState != null && prevState.status != newState.status)
                {
                    prevState.status = newState.status;
                    PublishServiceChange("serviceInfo.status", newState);
                    Console.WriteLine($"Publish: [{prevState.name}] is now {newState.status}.");
                }

            }
        }
        private void PublishServiceChange(string eventName, Service svcInfo)
        {
            _context.Clients.All.SendAsync("Response", "ServiceChanged", svcInfo);
        }

        public List<Service> GetServices()
        {
            return _lastServiceStates;
        }
    }
}
