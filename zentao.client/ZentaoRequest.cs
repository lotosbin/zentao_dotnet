using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace zentao.client {
    internal class ZentaoRequest {
        private readonly HttpClient _httpClient;

        public ZentaoRequest() {
            _httpClient = new HttpClient();
        }

        internal async Task<Result> GetDataAsync(string url) {
            var responseMessage = await _httpClient.GetAsync(url);
            responseMessage.EnsureSuccessStatusCode();
            var json = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(json);
            if (result.status != "success") {
                //todo
                throw new Exception("");
            }

            if (result.data != null && result.md5 != null) {
                if (Util.md5(result.data) != result.md5) {
                    //todo
                    throw new Exception("md5 not match");
                }
            }

            return result;
        }

        internal async Task<Result> PostDataAsync(string url, IList<KeyValuePair<string?, string?>> data) {
            var responseMessage = await _httpClient.PostAsync(url, new FormUrlEncodedContent(data.ToList()));
            responseMessage.EnsureSuccessStatusCode();
            var json = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(json);
            if (result.status != "success") {
                //todo
                throw new Exception("");
            }

            if (result.data != null && result.md5 != null) {
                if (Util.md5(result.data) != result.md5) {
                    //todo
                    throw new Exception("md5 not match");
                }
            }

            return result;
        }
    }
}