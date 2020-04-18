# FileQueueWatcher
A simple implementation to monitor a list of specified directories on a machine and record the file changes that occure within that directory.

Presently the following is monitored for:
* File Creation
* File Modification
* File Renaming
* File Deletion


This project uses the folloing packages:
* Microsoft.Extensions.Hosting.WindowsServices
* Serilog.Settings.Configuration
* Serilog.Formatting.Compact
* Serilog.Sinks.File
* Serilog.Sinks.Console