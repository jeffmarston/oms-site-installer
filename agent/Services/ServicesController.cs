using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eze.AdminConsole.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Eze.AdminConsole.Environment
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
        [HttpGet()]
        public ActionResult<IEnumerable<Service>> Get()
        {
            try
            {
                var svcs = ServiceUtils.GetAllEzeServices();
                return svcs.ToArray();
            }
            catch (Exception)
            {
                return new Service[] { };
            }
        }

        // POST api/services/start
        [HttpPost("start/{svcName}")]
        public void Start(string svcName)
        {
            ServiceUtils.StartService( svcName);
        }

        // POST api/services/stop
        [HttpPost("stop/{svcName}")]
        public void Stop(string svcName)
        {
            ServiceUtils.StopService(svcName);
        }
    }
}