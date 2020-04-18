using Serilog;
using System.IO;

namespace FileQueueWatcher
{

    class DirectoryMonitor
    {
        private string monitoredPath;
        private System.IO.FileSystemWatcher fileSystemWatcher;

        public DirectoryMonitor(Monitor monitor)
        {
            monitoredPath = monitor.Path;
            Initialise(monitor);
        }

        private void Initialise(Monitor monitor)
        {
            fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = monitoredPath;
            if (monitor.Create)
            {
                fileSystemWatcher.Created += HandleCreatedEvent;
            }
            if (monitor.Rename)
            {
                fileSystemWatcher.Renamed += HandleRenamedEvent;
            }
            if (monitor.Delete)
            {
                fileSystemWatcher.Deleted += HandleDeletedEvent;
            }
            if (monitor.Modify)
            {
                fileSystemWatcher.Changed += HandleModifiedEvent;
            }
            // TO DO : ADD File Filters (watcher.Filters.Add("*.yaml");)
        }

        public void StartMonitor()
        {
            Log.Information("Monitor for directory {MonitorPath} is STARTING.", monitoredPath);
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void StopMonitor()
        {
            Log.Information("Monitor for directory {MonitorPath} is STOPPING.", monitoredPath);
            fileSystemWatcher.EnableRaisingEvents = false;
        }

        private void HandleCreatedEvent(object sender, FileSystemEventArgs e)
        {
            LogEvent(e.Name, "created");
        }

        private void HandleRenamedEvent(object sender, FileSystemEventArgs e)
        {
            if (!IsFolder(e))
            {
                LogEvent(e.Name, "renamed");
            }
        }

        private void HandleDeletedEvent(object sender, FileSystemEventArgs e)
        {
            LogEvent(e.Name, "deleted");
        }

        private void HandleModifiedEvent(object sender, FileSystemEventArgs e)
        {
            if (!IsFolder(e))
            {
                LogEvent(e.Name, "modifed");
            }
        }

        private void LogEvent(string fileName, string action)
        {
            Log.Information("File {FileName} {Action} in {MonitorPath}.", fileName, action, monitoredPath);
        }

        private bool IsFolder(FileSystemEventArgs e)
        {
            return File.GetAttributes(e.FullPath).HasFlag(FileAttributes.Directory);
        }
    }
}
