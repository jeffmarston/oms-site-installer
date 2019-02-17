using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Management;
using Eze.AdminConsole.Machines;

namespace Eze.AdminConsole.Services
{
    public static class ServiceUtils
    {
        public static void StartService(string machineName, string svcName)
        {
            var serviceController = new ServiceController(svcName, machineName);
            if (serviceController.Status.ToString() == "Stopped")
            {
                serviceController.Start();
            }
        }

        public static void StopService(string machineName, string svcName)
        {
            var serviceController = new ServiceController(svcName, machineName);
            if (serviceController.Status.ToString() == "Running")
            {
                serviceController.Stop();
            }
        }

        public static Service GetServiceDetails(ServiceController svcTemp)
        {
            ManagementObject service = new ManagementObject(@"Win32_service.Name='" + svcTemp.ServiceName + "'");
            object o = service.GetPropertyValue("ProcessId");
            int processId = (int)((UInt32)o);
            Process process = Process.GetProcessById(processId);

            var svc = new Service()
            {
                name = svcTemp.ServiceName,
                status = svcTemp.Status.ToString()
            };
            if (processId > 0)
            {
                svc.pid = processId;
                svc.startTime = process.StartTime;
                svc.cpuTimeSpan = process.TotalProcessorTime;
                svc.memory = process.PrivateMemorySize64;
            }
            return svc;
        }

        public static Service GetService(string svcName)
        {
            var svcContollers = ServiceController.GetServices().Where(o => o.ServiceName.ToLower() == svcName.ToLower()).ToList();
            return (svcContollers.Count() == 0) ? null : GetServiceDetails(svcContollers[0]);
        }

        public static MachineData GetSystemInfo()
        {
            var pcMem = new PerformanceCounter("Memory", "Available MBytes");
            var pcCpu = new PerformanceCounter("Processor", "% Idle Time", "_Total");
            return new MachineData()
            {
                idleCpuTime = pcCpu.RawValue,
                memoryMb = pcMem.RawValue
            };
        }

        public static List<Service> GetAllEzeServices(string machineName)
        {
            var svcContollers = ServiceController.GetServices(machineName).Where(o => o.ServiceName.StartsWith("A"));
            var services = new List<Service>();
            foreach (var scTemp in svcContollers)
            {
                services.Add(GetServiceDetails(scTemp));
            }
            return services;
        }
    }
}