using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using System.Management;
using System.ServiceProcess;
using Wask.Lib.Model;

namespace Wask.Lib.Utils
{
    public static class ServiceUtils
    {
        public static void StartService(string svcName)
        {
            var serviceController = new ServiceController (svcName);
            if (serviceController.Status.ToString() == "Stopped")
            {
                serviceController.Start();
            }
        }

        public static void StopService(string svcName)
        {
            var serviceController = new ServiceController (svcName);
            if (serviceController.Status.ToString() == "Running")
            {
                serviceController.Stop();
            }
        }
        
        public static Service GetService(ServiceController svcTemp)
        {
            // ManagementObject service = new ManagementObject(@"Win32_service.Name='" + svcTemp.ServiceName + "'");
            // object o = service.GetPropertyValue("ProcessId");
            // int processId = (int)((UInt32)o);
            // Process process = Process.GetProcessById(processId);
            
            var svc = new Service()
            {
                name = svcTemp.ServiceName,
                status = svcTemp.Status.ToString()
            };
            // if (processId > 0)
            // {
            //     svc.pid = processId;
            //     svc.startTime = process.StartTime;
            //     svc.cpuTimeSpan = process.TotalProcessorTime;
            //     svc.memory = process.PrivateMemorySize64;
            // }

            return svc;
        }

        public static List<Service> GetAllEzeServices()
        {
            var svcContollers = ServiceController.GetServices().ToList().Where(o => o.ServiceName.StartsWith("Mainline"));
            var services = new List<Service>();
            foreach (var scTemp in svcContollers)
            {
                services.Add(GetService(scTemp));
            }
            return services;
        }
    }
}