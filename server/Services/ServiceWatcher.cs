using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Threading;
using Microsoft.AspNetCore.SignalR;
using Eze.AdminConsole.Machines;
using System.Diagnostics;
using Eze.AdminConsole.Services;

namespace Eze.AdminConsole.Environment
{
    public class ServiceWatcher
    {
        private Dictionary<string, List<Service>> _lastServiceStates = new Dictionary<string, List<Service>>();
        private MachineData _lastMachineData = new MachineData();
        private static Timer _pollingTimer = null;
        private static IHubCallerClients _signalrClients;

        public static void Init(IHubCallerClients clients, string machineName, List<Service> services)
        {
            if (_instance == null)
            {
                _signalrClients = clients;
                _instance = new ServiceWatcher(machineName, services);
            }
        }

        private static ServiceWatcher _instance = null;
        private const int TimerIntervalMs = 1000;

        private ServiceWatcher(string machineName, List<Service> servicesToMonitor)
        {
            _lastServiceStates[machineName] = servicesToMonitor;
            _pollingTimer = new Timer(DoPoll, null, 0, TimerIntervalMs);
        }

        private void DoPoll(object state)
        {
            foreach (var key in _lastServiceStates.Keys)
            {
                foreach (var prevState in _lastServiceStates[key])
                {
                    bool dirty = false;
                    var newState = ServiceUtils.GetServiceDetails(new ServiceController(prevState.name));

                    if (newState.pid > 0)
                    {
                        if (prevState.cpuTimeSpan != newState.cpuTimeSpan)
                        {
                            newState.cpu = (newState.cpuTimeSpan.Ticks - prevState.cpuTimeSpan.Ticks) / (TimeSpan.TicksPerMillisecond * 10);
                            prevState.cpuTimeSpan = newState.cpuTimeSpan;
                            dirty = true;
                        }

                    }

                    if (prevState != null && prevState.status != newState.status)
                    {
                        prevState.status = newState.status;
                        dirty = true;
                    }

                    if (dirty)
                    {
                        PublishServiceChange(key, newState.name, newState);
                    }
                }
            }
        }

        private void PublishServiceChange(string machineName, string svcName, Service svcInfo)
        {
            _signalrClients.All.SendAsync("service", machineName, svcName, svcInfo);
            Console.WriteLine($"SignalR [service] : " + machineName + ", " + svcName + ", " + svcInfo.status);
        }
    }
}
