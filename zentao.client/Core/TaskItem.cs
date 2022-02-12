// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace zentao.client.Core {
    public class TaskItem {
        public string id { get; set; } = "";
        public string name { get; set; } = "";
        public string projectID { get; set; } = "";
        public string projectName { get; set; } = "";
        /// <summary>
        /// wait|doing|done
        /// </summary>
        public string status { get; set; } = "";
    }
}