using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Eze.AdminConsole.Environment
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        // GET api/environment
        [HttpGet]
        public ActionResult<Topology> Get()
        {
            // var envTopology = new Topology();
            var envTopology = Load();
            return envTopology;
        }

        // PUT api/environment
        [HttpPut()]
        public void Put([FromBody] Topology topology)
        {
            Save(topology);
        }

        private Topology Load()
        {
            string folderbase = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            string txt = System.IO.File.ReadAllText(folderbase + @"\Eze\Admin\environment.json");
            return JsonConvert.DeserializeObject<Topology>(txt);
        }

        private void Save(Topology topology)
        {
            string json = JsonConvert.SerializeObject(topology);
            
            string folderbase = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            System.IO.File.WriteAllText(folderbase + @"\Eze\Admin\environment.json", json);
        }
    }
}