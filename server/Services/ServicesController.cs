using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Eze.AdminConsole.Services
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private IHubContext<ServiceMgmtHub> _context;
        private Topology _topology = new Topology();

        public ServicesController(IHubContext<ServiceMgmtHub> context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet("{machineName}")]
        public ActionResult<IEnumerable<Service>> Get(string machineName)
        {
            try
            {
                if (_topology.servers.FirstOrDefault(o => o.name == machineName.ToLower()) == null)
                {
                    throw new Exception("Server not configured: " + machineName);
                }
                var svcs = ServiceUtils.GetAllEzeServices(machineName);
                // ServiceWatcher.Init(_context, machineName, svcs);
                return svcs.ToArray();
            }
            catch (Exception)
            {
                return new Service[] { };
            }
        }

        // POST api/services/start
        [HttpPost("{machineName}/start/{svcName}")]
        public void Start(string machineName, string svcName)
        {
            ServiceUtils.StartService(machineName, svcName);
        }

        // POST api/services/stop
        [HttpPost("{machineName}/stop/{svcName}")]
        public void Stop(string machineName, string svcName)
        {
            ServiceUtils.StopService(machineName, svcName);
        }
    }
}