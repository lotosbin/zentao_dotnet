using System.Security.Cryptography;
using System.Text;

namespace zentao.client.Core.Data {
    internal static class Util {
        internal static string Md5(string input) {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            var sBuilder = new StringBuilder();
            foreach (var t in data) {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
