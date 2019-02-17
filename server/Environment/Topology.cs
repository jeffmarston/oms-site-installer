namespace Eze.AdminConsole.Environment
{
    public class Topology
    {
        public MachineSpec[] servers { get; set; }
        public MachineSpec[] clients { get; set; }
        public MachineSpec[] database { get; set; }
        public Topology()
        {
            servers = new MachineSpec[] { 
                // new MachineSpec("eze-app01"),
                // new MachineSpec("eze-app02"),
                // new MachineSpec("eze-app03"),
                new MachineSpec("localhost")
            };
            clients = new MachineSpec[] { 
                new MachineSpec("venom.qalab.net"),
                new MachineSpec("marston9020b")
            };
            database = new MachineSpec[] { 
                new MachineSpec("eze-db")
            };
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