using Newtonsoft.Json;
using zentao.client.Core.Data.Core;
using static zentao.client.Core.Data.Util;

namespace zentao.client.Core.Data {
    internal class ZentaoRequest {
        private readonly ZentaoHttpClient _httpClient;

        public ZentaoRequest(ZentaoHttpClient httpClient) {
            _httpClient = httpClient;
        }

        internal async Task<Result> GetDataAsync(string url) {
            using var httpClient = _httpClient.CreateHttpClient();
            var responseMessage = await httpClient.GetAsync(url);
            responseMessage.EnsureSuccessStatusCode();
            var json = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(json);
            if (result.status != "success") {
                //todo
                throw new Exception("");
            }

            if (result.data != null && result.md5 != null) {
                if (Md5(result.data) != result.md5) {
                    //todo
                    throw new Exception("md5 not match");
                }
            }

            return result;
        }

        internal async Task<Result> PostDataAsync(string url, IList<KeyValuePair<string?, string?>> data) {
            using var httpClient = _httpClient.CreateHttpClient();
            var responseMessage = await httpClient.PostAsync(url, new FormUrlEncodedContent(data.ToList()));
            responseMessage.EnsureSuccessStatusCode();
            var json = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(json);
            if (result.status != "success") {
                //todo
                throw new Exception("");
            }

            if (result.data != null && result.md5 != null) {
                if (Md5(result.data) != result.md5) {
                    //todo
                    throw new Exception("md5 not match");
                }
            }

            return result;
        }
    }
}