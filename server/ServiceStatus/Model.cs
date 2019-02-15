using System.Collections.Generic;
using System;

namespace Eze.AdminConsole.Model
{
    public class Service
    {
        public string path { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public long memory { get; set; }
        public string instance { get; set; }
        public string exe { get; set; }
        public int pid { get; set; }
        public DateTime startTime { get; internal set; }
        
        public TimeSpan cpuTimeSpan { get; internal set; }

        public override string ToString()
        {
            return $"{name} - {status}";
        }
    }


}
