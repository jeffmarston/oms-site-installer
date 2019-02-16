using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Eze.AdminConsole.Services
{
    public class ServiceInfoController : Controller
    {
        private IHubContext<ServiceMgmtHub> _context;
        private Timer _statusWatchTimer = new Timer();

        public ServiceInfoController(IHubContext<ServiceMgmtHub> context)
        {
            this._context = context;
        }

        // GET: /ServiceInfo/
        public string Index()
        {
            _context.Clients.All.SendAsync("Response", "StatusChangeEvent", "Index");
            return "What to do with this?";
        }

        
        // GET: /ServiceInfo/Services/ 
        public Service[] Services()
        {
            var svcs = ServiceUtils.GetAllEzeServices();
            ServiceWatcher.Init(svcs, _context);
            return svcs.ToArray();
        }
        
        // POST: /ServiceInfo/StartService
        [HttpPost] 
        public ActionResult StartService([FromBody] Service model)
        {
            ServiceUtils.StartService(model.name);
            return null;
        }

        // POST: /ServiceInfo/StopService
        [HttpPost] 
        public ActionResult StopService([FromBody] Service model)
        {
            ServiceUtils.StopService(model.name);
            return View();
        }

    }
}