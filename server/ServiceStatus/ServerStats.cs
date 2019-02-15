namespace Eze.AdminConsole.Model
{
    public class ServerStats
    {
        public long idleCpuTime { get; internal set; }
        public long cpuPercent { get; internal set; }
        public long memoryMb { get; internal set; }
    }
}