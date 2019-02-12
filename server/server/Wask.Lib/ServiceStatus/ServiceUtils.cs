using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using Wask.Lib.Model;

namespace Wask.Lib.Utils
{
    public static class ServiceUtils
    {
        public static void StartService(string svcName)
        {
            var serviceController = new ServiceController { ServiceName = svcName };
            if (serviceController.Status == ServiceControllerStatus.Stopped)
            {
                serviceController.Start();
            }
        }

        public static void StopService(string svcName)
        {
            var serviceController = new ServiceController { ServiceName = svcName };
            if (serviceController.Status == ServiceControllerStatus.Running)
            {
                serviceController.Stop();
            }
        }

        public static string GetServiceStatus(string svcName)
        {
            ServiceController svcController = ServiceController.GetServices().ToList().Find(o => o.ServiceName == svcName);
            if (svcController == null)
                return "";

            switch (svcController.Status)
            {
                case ServiceControllerStatus.StopPending:
                    return "Stop Pending";
                case ServiceControllerStatus.Stopped:
                    return "Stopped";
                case ServiceControllerStatus.StartPending:
                    return "Start Pending";
                case ServiceControllerStatus.Running:
                    return "Running";
            }
            return "";
        }

        private const string serverBin = @"\Server\Bin";

        private static string GetServicePrefix(string dirRoot)
        {
            string[] pieces = dirRoot.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            return (pieces.Length > 0) ? pieces[pieces.Length - 1] : string.Empty;
        }
        private static string GenerateServiceName(string prefix, string filename)
        {
            string p = string.IsNullOrEmpty(prefix) ? string.Empty : prefix + " ";

            string s = filename;
            if (s.StartsWith("Eze.", StringComparison.InvariantCultureIgnoreCase))
            {
                s = s.Substring(4);
            }

            if (s.EndsWith(".Svc.exe", StringComparison.InvariantCultureIgnoreCase))
            {
                s = s.Substring(0, s.Length - 8);
            }
            s = s.Replace(".", " ");
            return p + s;
        }

        public static List<Service> GetAllEzeServices()
        {
            var svcContollers = ServiceController.GetServices().ToList().Where(o => o.ServiceName.StartsWith("Mainline"));
            var services = new List<Service>();
            foreach (var svc in svcContollers)
            {
                services.Add(new Service() { path = svc.ServiceType.ToString(), name = svc.ServiceName });
            }
            return services;
        }

        public static void InstallAndStart(string svcName, string svcDesc, string svcPath)
        {
            var processInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller { Parent = processInstaller };

            var context = new InstallContext("install.log", null);
            context.Parameters["assemblypath"] = svcPath;
            serviceInstaller.Context = context;
            serviceInstaller.ServiceName = svcName;
            serviceInstaller.DisplayName = svcDesc;
            serviceInstaller.Description = svcDesc;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.Parent = processInstaller;
            serviceInstaller.Install(new ListDictionary());

            var serviceController = new ServiceController { ServiceName = svcName };
            serviceController.Start();
        }
    }
}