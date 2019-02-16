namespace Eze.AdminConsole.ServiceMgmt
{
    public class Topology
    {
        public MachineSpec[] servers { get; set; }
        public MachineSpec[] clients { get; set; }
        public MachineSpec[] database { get; set; }
        public Topology()
        {
            servers = new MachineSpec[] { 
                new MachineSpec("eze-app01"),
                new MachineSpec("eze-app02"),
                new MachineSpec("eze-app03")
            };
            clients = new MachineSpec[] { 
                new MachineSpec("venom.qalab.net"),
                new MachineSpec("marston9020b")
            };
            database = new MachineSpec[] { 
                new MachineSpec("eze-db")
            };

// { 
//   "servers": [
//     { "name": "eze-app01" },
//     { "name": "eze-apple02" },
//     { "name": "eze-app99" }
//   ],
//   "clients": [
//     { "name": "marston9020b" },
//     { "name": "brucewayne1000" }
//   ],
//   "database": [
//     { "name": "eze-db01" }
//   ]
// }

        }
    }

    public class MachineSpec
    {
        public string name { get; set; }

        public MachineSpec(string name)
        {
            this.name = name;
        }
    }
}