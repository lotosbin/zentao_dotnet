using System.Text.RegularExpressions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using static zentao.client.Core.Data.Util;

namespace zentao.client.Core.Data.Core {
    /// <summary>
    /// </summary>
    /// <seealso>
    ///     <cref>https://www.zentao.net/book/api/setting-369.html</cref>
    /// </seealso>
    ///     <seealso>
    ///         <cref>https://www.zentao.pm/book/zentao-api-manual.mhtml</cref>
    ///     </seealso>
    ///     <seealso>
    ///         <cref>https://devel.easycorp.cn/book/extension/xtask.api-auth-44.html</cref>
    ///     </seealso>
    internal class ZentaoClient : IZentaoClient {
        private readonly IMemoryCache _cache;

        public ZentaoClient(IMemoryCache cache) {
            _cache = cache;
        }

        public async Task<IList<TaskItem>> GetMyTaskAsync(string host, string account, string password) {
            var client = await GetOrCreateHttpClientAsync(host, account, password);
            await EnsureLogin(client, host, account, password);
            var result = await new ZentaoRequest(client).GetDataAsync($"{host}/my-task-assignedTo-status_asc-0-100-1.json");
            var data = JsonConvert.DeserializeObject<TaskData>(result.data);
            return data.tasks;
        }

        public async Task<List<BugItem>> GetMyBugAsync(string host, string account, string password) {
            var client = await GetOrCreateHttpClientAsync(host, account, password);
            await EnsureLogin(client, host, account, password);
            var url = QueryHelpers.AddQueryString($"{host}/my-bug.json", new Dictionary<String, String>());
            var result = await new ZentaoRequest(client).GetDataAsync(url);
            var data = JsonConvert.DeserializeObject<BugData>(result.data);
            return data.bugs;
        }

        public async Task<IList<StoryItem>> GetMyStoryListAsync(string host, string account, string password) {
            var client = await GetOrCreateHttpClientAsync(host, account, password);
            await EnsureLogin(client, host, account, password);
            //has session
            var result = await new ZentaoRequest(client).GetDataAsync($"{host}/my-story.json");
            var data = JsonConvert.DeserializeObject<StoreData>(result.data);
            return data.stories;
        }

        public async Task<IList<TaskItem>> GetTaskAsync(string host, string account, string password) {
            var client = await GetOrCreateHttpClientAsync(host, account, password);
            await EnsureLogin(client, host, account, password);
            var result = await new ZentaoRequest(client).GetDataAsync($"{host}/project-task-0-all-0-lastEditedDate_desc-0-100.html");
            var data = JsonConvert.DeserializeObject<TaskData>(result.data);
            return data.tasks;
        }
        public async Task<IList<TaskItem>> GetBugAsync(string host, string account, string password) {
            var client = await GetOrCreateHttpClientAsync(host, account, password);
            await EnsureLogin(client, host, account, password);
            var result = await new ZentaoRequest(client).GetDataAsync($"{host}/project-bug-0-all-0-lastEditedDate_desc-0-100.html");
            var data = JsonConvert.DeserializeObject<TaskData>(result.data);
            return data.tasks;
        }
        private async Task EnsureLogin(ZentaoHttpClient client, string host, string account, string password) {
            if (!client.IsLogin()) {
                //no session ,do login
                await Login(client, host, account, password);
            }

            if (!client.IsLogin()) {
                //no permission
                throw new Exception("login failed");
            }
        }

        private async Task Login(ZentaoHttpClient client, string host, string account, string password) {
            //get login.html,get verifyRand
            var verifyRand = await GetLoginVerifyRand(client, host);


            //post login.html
            using var httpClient = client.CreateHttpClient();
            var responseMessage = await httpClient.PostAsync(
                $"{host}/user-login.html",
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>> {
                    new("account", account),
                    new("password", Md5(Md5(password) + verifyRand)),
                    new("verifyRand", verifyRand),
                    new("passwordStrength", "2"),
                    new("keepLogin", "1")
                })
            );
            responseMessage.EnsureSuccessStatusCode();
        }

        private async Task<string> GetLoginVerifyRand(ZentaoHttpClient client, string host) {
            using var httpClient = client.CreateHttpClient();
            var responseMessage = await httpClient.GetAsync($"{host}/user-login.html");
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

        private async Task<ZentaoHttpClient> GetOrCreateHttpClientAsync(string host, string account, string password) => await _cache.GetOrCreateAsync($"{account}:{password}@{host}", _ => Task.FromResult(new ZentaoHttpClient(host)));
    }
}