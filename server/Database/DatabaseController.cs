using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Eze.AdminConsole.Database
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        // GET api/values/5
        [HttpGet("diagnostics")]
        public ActionResult<string> Diagnostics()
        {
            var diag = new DatabaseRunner();
            return diag.RunDiagnostics();
        }
        // GET api/values/5
        [HttpGet("whatsrunning")]
        public ActionResult<dynamic> WhatsRunning()
        {
            var diag = new DatabaseRunner();
            return diag.WhatsRunning();
        }
    }
}