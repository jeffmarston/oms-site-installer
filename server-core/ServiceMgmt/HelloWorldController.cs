using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalRSimpleChat.ServiceMgmt
{
    public class ServiceInfoController : Controller
    {
        private IHubContext<ServiceMgmtHub> _context;
        private List<ServiceInfo> _allServices;
        private Timer _statusWatchTimer = new Timer();

        public ServiceInfoController(IHubContext<ServiceMgmtHub> context)
        {
            this._context = context;
        }

        // 
        // GET: /ServiceInfo/

        public string Index()
        {
            _context.Clients.All.SendAsync("Response", "StatusChangeEvent", "Index");
            return "What to do with this?";
        }

        // 
        // GET: /ServiceInfo/Services/ 
        public string Services()
        {
            if (_allServices == null)
            {
                _allServices = QueryServices().ToList();
            }

            _context.Clients.All.SendAsync("Response", "StatusChangeEvent", "Services");
            return "This is the Welcome action method...";
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
            _statusWatchTimer.Elapsed += StatusWatchTimer_Elapsed;
            _statusWatchTimer.Interval = 1000;
            _statusWatchTimer.Start();

            return _allServices.ToArray();
        }


        private void StatusWatchTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var knownSvc in _allServices)
            {
                var svcCtrl = new ServiceController(knownSvc.Name);
                if (svcCtrl.Status.ToString() != knownSvc.Status)
                {
                    knownSvc.Status = svcCtrl.Status.ToString();

                    _context.Clients.All.SendAsync("Response", "StatusChangeEvent", knownSvc);


                }
            }
        }

    }
}