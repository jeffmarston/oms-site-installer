using System;
using System.Collections.Generic;
using System.ServiceProcess;

namespace Eze.AdminConsole.Services
{
    public class ServiceManager
    {
        private List<ServiceInfo> _allServices;

        public ServiceManager()
        {
        }

        public ServiceInfo[] QueryServices()
        {
            _allServices = new List<ServiceInfo>();
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();

            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.ServiceName.StartsWith("a", StringComparison.InvariantCultureIgnoreCase))
                {
                    _allServices.Add(new ServiceInfo()
                    {
                        Name = scTemp.ServiceName,
                        Status = scTemp.Status.ToString(),
                        MachineName = scTemp.MachineName
                    });
                }
            }
            return _allServices.ToArray();
        }

        public void StopService(string name)
        {
            //var svc = ServiceController.GetServices().First(o => o.ServiceName == name);
            var svc = new ServiceController(name);
            svc.Stop();
        }
        public void StartService(string name)
        {
            var svc = new ServiceController(name);
            svc.Start();
        }
    }

    public class ServiceInfo
    {
        public string Name { get; internal set; }
        public string Status { get; internal set; }
        public string MachineName { get; internal set; }
    }
}
