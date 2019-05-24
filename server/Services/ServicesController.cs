using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        [HttpGet("{machineName}")]
        public ActionResult<IEnumerable<Service>> Get(string machineName)
        {
            try
            {
                _topology = new EnvironmentController().Load();
                if (_topology.servers.FirstOrDefault(o => o.name.ToLower() == machineName.ToLower()) == null)
                {
                    throw new Exception("Server not configured: " + machineName);
                }
                var svcs = ServiceUtils.GetAllEzeServices(machineName);
                return svcs.ToArray();
            }
            catch (Exception)
            {
                return new Service[] { };
            }
        }

        // POST api/services/logs
        [HttpGet("{machineName}/{svcName}/logs.txt")]
        public async Task<IActionResult> Logs(string machineName, string svcName)
        {
            var filePath = @"D:\temp\logfile.log";
            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            if (stream == null)
                return NotFound(); // returns a NotFoundResult with Status404NotFound response.
            return File(stream, "application/octet-stream"); // returns a FileStreamResult
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