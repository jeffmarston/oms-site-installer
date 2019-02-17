namespace Eze.AdminConsole.Machines
{
    public class MachineData
    {
        public long idleCpuTime { get; internal set; }
        public long cpuPercent { get; internal set; }
        public long memoryMb { get; internal set; }
    }
}