using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace FileQueueWatcher
{
    public class DirectoryMonitorWorker : BackgroundService
    {

        public DirectoryMonitorWorker()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<DirectoryMonitor> monitors = new List<DirectoryMonitor>();
            monitors.Add(new DirectoryMonitor("C:\\temp"));
            monitors.Add(new DirectoryMonitor("C:\\temp\\test"));
            foreach (DirectoryMonitor monitor in monitors)
            {
                monitor.StartMonitor();
            }
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
            foreach (DirectoryMonitor monitor in monitors)
            {
                monitor.StopMonitor();
            }
        }
    }
}
