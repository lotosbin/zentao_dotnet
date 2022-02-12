// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace zentao.client.Core {
    public class BugItem {
        public string id { get; set; } = "";
        public string title { get; set; } = "";

        /// <summary>
        /// active|resolved|closed
        /// </summary>
        public string status { get; set; } = "active";

        /// <summary>
        /// 0000-00-00 00:00:00
        /// </summary>
        public string lastEditedDate { get; set; } = "0000-00-00 00:00:00";
    }
}