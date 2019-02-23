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
            var svc = new Service()
            {
                name = svcTemp.ServiceName,
                status = svcTemp.Status.ToString()
            };
            return svc;
        }

        public static Service GetService(string svcName)
        {
            var svcContollers = ServiceController.GetServices().Where(o => o.ServiceName.ToLower() == svcName.ToLower()).ToList();
            return (svcContollers.Count() == 0) ? null : GetServiceDetails(svcContollers[0]);
        }

        public static List<Service> GetAllEzeServices(string machineName)
        {
            try
            {
                // ConnectionOptions op = new ConnectionOptions();
                // op.Username = "qalab\\analyst";
                // op.Password = "Boston09";
                // ManagementScope scope = new ManagementScope(@"\\" + machineName + @".qalab.net\root\cimv2", op);
                // scope.Connect();
                // ManagementPath path = new ManagementPath("Win32_Service");
                // ManagementClass services;
                // services = new ManagementClass(scope, path, null);

                // var serviceData = new List<Service>();
                // foreach (ManagementObject service in services.GetInstances())
                // {
                //     var svcName = service.GetPropertyValue("Name").ToString();
                //     if (svcName.StartsWith("E")) {
                //         serviceData.Add(new Service() {
                //             name = svcName,
                //             status = service.GetPropertyValue("State").ToString(),
                //             pid = Int32.Parse(service.GetPropertyValue("ProcessId").ToString()),
                //             path = service.GetPropertyValue("PathName").ToString()
                //         });
                //     }
                // }
                var svcContollers = ServiceController.GetServices();
                var ezeSvcs = svcContollers.Where(o => o.ServiceName.StartsWith("E"));

                var serviceData = new List<Service>();
                foreach (var scTemp in ezeSvcs)
                {
                    serviceData.Add(GetServiceDetails(scTemp));
                }
                return serviceData;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Service>();
            }
        }
    }
}