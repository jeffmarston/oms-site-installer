using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Management;
using Eze.AdminConsole.Machines;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

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

        private static async Task<Service[]> GetServicesAsync(string machineName)
        {
            HttpClient client = new HttpClient();
            Service[] services = null;
            var path = "http://" + machineName + ":5001/api/services";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                services = await response.Content.ReadAsAsync<Service[]>();
            }
            return services;
        }

        public static List<Service> GetAllEzeServices(string machineName)
        {
            try
            {
                var serviceData = GetServicesAsync(machineName).Result;
                return new List<Service>(serviceData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Service>();
            }
        }
    }
}