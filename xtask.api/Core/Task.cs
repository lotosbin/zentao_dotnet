using System;

namespace idea_generic_task_server.Core {
    public class Task {
        public string id { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public string updated { get; set; }
        public string created { get; set; }
        public bool closed { get; set; }
        public string type { get; set; }
        public string issueUrl { get; set; }
    }
}