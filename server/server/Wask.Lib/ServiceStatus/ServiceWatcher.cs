using Wask.Lib.Utils;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Threading;
using Wask.Lib.SignalR;
using Microsoft.AspNet.SignalR;

namespace Wask.Lib.Model
{
    public class ServiceWatcher
    {
        private List<Service> _knownStates = new List<Service>();
        private Timer _pollingTimer;
        private IHubContext _context;
        private string _channel = Constants.ServiceInfoChannel;


        internal static void Init(List<Service> services)
        {
            _instance = new ServiceWatcher(services);
        }

        private static ServiceWatcher _instance;
        public static ServiceWatcher Instance
        {
            get { return _instance; }
        }

        private ServiceWatcher(List<Service> servicesToMonitor)
        {
            _knownStates = servicesToMonitor;
            _context = GlobalHost.ConnectionManager.GetHubContext<EventHub>();
            _pollingTimer = new Timer(DoPoll, null, 0, 1000);
        }

        private void DoPoll(object state)
        {
            foreach (var prevState in _knownStates)
            {
                var newState = ServiceUtils.GetService(new ServiceController(prevState.name));

                if (newState.pid > 0)
                {
                    var cpuTime = Math.Round((newState.cpuTimeSpan.TotalSeconds - prevState.cpuTimeSpan.TotalSeconds) / 10.0, 2);
                    prevState.cpuTimeSpan = newState.cpuTimeSpan;
                    Console.WriteLine($"Publish: [{prevState.name}]. diffTime = {cpuTime} %");
                }

                if (prevState != null && prevState.status != newState.status)
                {
                    prevState.status = newState.status;
                    PublishServiceChange("serviceInfo.status", new List<Service>() { prevState });
                    Console.WriteLine($"Publish: [{prevState.name}] is now {newState.status}.");
                }

            }
        }
        private void PublishServiceChange(string eventName, List<Service> deltas)
        {
            _context.Clients.Group(_channel).OnEvent(Constants.ServiceInfoChannel, new ChannelEvent
            {
                ChannelName = Constants.ServiceInfoChannel,
                Name = eventName,
                Data = deltas
            });
        }

        public List<Service> GetServices()
        {
            return _knownStates;
        }
    }
}
