using System.Text.RegularExpressions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using static zentao.client.Util;

namespace zentao.client.core {
    /// <summary>
    /// <seealso>
    ///     <cref>https://devel.easycorp.cn/book/extension/xtask.api-auth-44.html</cref>
    /// </seealso>
    /// </summary>
    public class ZentaoClient {
        private readonly string _host;
        private readonly string _account;
        private readonly string _password;
        private readonly IMemoryCache _cache;

        public ZentaoClient(IMemoryCache cache, string host, string account, string password) {
            _cache = cache;
            _host = host;
            _account = account;
            _password = password;
        }

        public async Task<IList<TaskItem>> GetMyTaskAsync() {
            var client = await GetOrCreateHttpClientAsync();
            await EnsureLogin(client);
            //has session
            var result = await new ZentaoRequest(client).GetDataAsync($"{_host}/my-task.json");
            var data = JsonConvert.DeserializeObject<TaskData>(result.data);
            return data.tasks;
        }

        public async Task<List<BugItem>> GetMyBugAsync() {
            var client = await GetOrCreateHttpClientAsync();
            await EnsureLogin(client);
            var url = QueryHelpers.AddQueryString($"{_host}/my-bug.json", new Dictionary<String, String>());
            var result = await new ZentaoRequest(client).GetDataAsync(url);
            var data = JsonConvert.DeserializeObject<BugData>(result.data);
            return data.bugs;
        }
        public async Task<IList<StoryItem>> GetMyStoryListAsync() {
            var client = await GetOrCreateHttpClientAsync();
            await EnsureLogin(client);
            //has session
            var result = await new ZentaoRequest(client).GetDataAsync($"{_host}/my-story.json");
            var data = JsonConvert.DeserializeObject<StoreData>(result.data);
            return data.stories;
        }
        private async Task EnsureLogin(ZentaoHttpClient client) {
            if (!client.IsLogin()) {
                //no session ,do login
                await Login(client);
            }

            if (!client.IsLogin()) {
                //no permission
                throw new Exception("login failed");
            }
        }

        private async Task Login(ZentaoHttpClient client) {
            //get login.html,get verifyRand
            var verifyRand = await GetLoginVerifyRand(client);


            //post login.html
            using var httpClient = client.CreateHttpClient();
            var responseMessage = await httpClient.PostAsync(
                $"{_host}/user-login.html",
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>> {
                    new("account", _account),
                    new("password", Md5(Md5(_password) + verifyRand)),
                    new("verifyRand", verifyRand),
                    new("passwordStrength", "2"),
                    new("keepLogin", "1")
                })
            );
            responseMessage.EnsureSuccessStatusCode();
        }

        private async Task<string> GetLoginVerifyRand(ZentaoHttpClient client) {
            using var httpClient = client.CreateHttpClient();
            var responseMessage = await httpClient.GetAsync($"{_host}/user-login.html");
            responseMessage.EnsureSuccessStatusCode();
            var text = await responseMessage.Content.ReadAsStringAsync();
            //<input type='hidden' name='verifyRand' id='verifyRand' value='1075594029'  />
            string pattern = @"id='verifyRand' value='(\d+)'";

            var m = Regex.Match(text, pattern);

            var verifyRand = m.Groups[1].Value;
            if (String.IsNullOrEmpty(verifyRand)) {
                throw new Exception("get verifyRand failed");
            }

            return verifyRand;
        }

        private async Task<ZentaoHttpClient> GetOrCreateHttpClientAsync() =>
            await _cache.GetOrCreateAsync($"{_account}@{_host}", _ => Task.FromResult(new ZentaoHttpClient(_host)));
    }
}