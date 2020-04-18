using Serilog;
using System.IO;

namespace FileQueueWatcher
{

    class DirectoryMonitor
    {
        private string monitoredPath;
        private System.IO.FileSystemWatcher fileSystemWatcher;

        public DirectoryMonitor(string path)
        {
            monitoredPath = path;
            Initialise();
        }

        private void Initialise()
        {
            fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = monitoredPath;
            fileSystemWatcher.Created += HandleCreatedEvent;
            fileSystemWatcher.Renamed += HandleRenamedEvent;
            fileSystemWatcher.Deleted += HandleDeletedEvent;
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
            LogEvent(e.Name, "renamed");
        }

        private void HandleDeletedEvent(object sender, FileSystemEventArgs e)
        {
            LogEvent(e.Name, "deleted");
        }

        private void LogEvent(string fileName, string action)
        {
            Log.Information("File {FileName} {Action} in {MonitorPath}.", fileName, action, monitoredPath);
        }
    }
}
