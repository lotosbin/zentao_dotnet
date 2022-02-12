// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace zentao.client.Core.Data.Core {
    public class MyProfileData {
        public User? user { get; set; }

        public class User {
            public string? id { get; set; }
        }
    }
}