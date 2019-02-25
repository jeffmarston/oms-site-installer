using System.IO;
using Newtonsoft.Json;

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
                new MachineSpec("aesdrrzq00db01"),
                new MachineSpec("aesdrrzq00app01"),
                new MachineSpec("aefhrwjf00app01"),
                new MachineSpec("marston9020b")
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