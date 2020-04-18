using System;
using System.Collections.Generic;
using System.Text;

namespace FileQueueWatcher
{
    class Monitor
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public bool Create { get; set; } = true;
        public bool Delete { get; set; } = true;
        public bool Rename { get; set; } = true;
        public bool Modify { get; set; } = true;
    }
}
