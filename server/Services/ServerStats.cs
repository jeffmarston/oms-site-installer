namespace Eze.AdminConsole.Services
{
    public class ServerStats
    {
        public long idleCpuTime { get; internal set; }
        public long cpuPercent { get; internal set; }
        public long memoryMb { get; internal set; }
    }
}