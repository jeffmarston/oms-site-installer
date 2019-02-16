using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Threading;
using Microsoft.AspNetCore.SignalR;
using Eze.AdminConsole.Machines;

namespace Eze.AdminConsole.Services
{
    public class ServiceWatcher
    {
        private List<Service> _lastServiceStates = new List<Service>();
        private MachineData _lastMachineData = new MachineData();

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
            var newSvrStats = ServiceUtils.GetSystemInfo();
            newSvrStats.cpuPercent = 100 - (newSvrStats.idleCpuTime - _lastMachineData.idleCpuTime) / (TimeSpan.TicksPerMillisecond * 10); // only works with 1 second interval;
            _lastMachineData = newSvrStats;
            if (newSvrStats.cpuPercent >= 0 && newSvrStats.cpuPercent <= 100)
            {
                _context.Clients.All.SendAsync("Response", "MachineData", newSvrStats);
            }
            foreach (var prevState in _lastServiceStates)
            {
                var newState = ServiceUtils.GetService(new ServiceController(prevState.name));

                if (newState.pid > 0)
                {
                    var cpuTime = Math.Round((newState.cpuTimeSpan.TotalSeconds - prevState.cpuTimeSpan.TotalSeconds) / 10.0, 2);
                    prevState.cpuTimeSpan = newState.cpuTimeSpan;
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
