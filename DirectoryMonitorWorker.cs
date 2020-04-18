using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace FileQueueWatcher
{
    public class DirectoryMonitorWorker : BackgroundService
    {

        private List<Monitor> Monitors;

        public DirectoryMonitorWorker()
        {
            Initialise();
        }

        private void Initialise()
        {
            using (StreamReader r = new StreamReader("monitor.json"))
            {
                string json = r.ReadToEnd();
                Monitors = JsonSerializer.Deserialize<List<Monitor>>(json);
            }
            Log.Information("Total of {count} directories to monitor", Monitors.Count);
            Log.Debug("{items}", Monitors);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<DirectoryMonitor> directoryMonitors = new List<DirectoryMonitor>();
            foreach(Monitor monitor in Monitors)
            {
                Log.Debug("Adding {Description} monitor", monitor.Description);
                directoryMonitors.Add(new DirectoryMonitor(monitor));
            }
            foreach (DirectoryMonitor directoryMonitor in directoryMonitors)
            {
                directoryMonitor.StartMonitor();
            }
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
            foreach (DirectoryMonitor directoryMonitor in directoryMonitors)
            {
                directoryMonitor.StopMonitor();
            }
        }
    }
}
