using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace zentao.client.core {
    /// <summary>
    /// <seealso>
    ///     <cref>https://devel.easycorp.cn/book/extension/xtask.api-auth-44.html</cref>
    /// </seealso>
    /// </summary>
    public class ZentaoClient {
        private readonly ZentaoRequest _request;
        private readonly IConfiguration _configuration;
        private readonly string _host;
        private string _account;
        private string _password;
        private readonly IMemoryCache _cache;
        private readonly string? _sessionName;
        private readonly string? _sessionId;

        public ZentaoClient(IMemoryCache cache, string host, string? sessionName, string? sessionId) {
            _cache = cache;
            _host = host;
            _sessionName = sessionName;
            _sessionId = sessionId;
            _request = new ZentaoRequest();
        }

        public void Login(string account, string password) {
            _account = account;
            _password = password;
        }

        /// <summary>
        /// <seealso>
        ///     <cref>https://www.zentao.net/ask/4827.html</cref>
        /// </seealso>
        /// </summary>
        /// <returns></returns>
        private async Task<SessionData> GetSessionID() {
            if (!string.IsNullOrEmpty(_sessionName) && !string.IsNullOrEmpty(_sessionId)) {
                return new SessionData() {
                    sessionName = _sessionName,
                    sessionID = _sessionId
                };
            }

            return await _cache.GetOrCreateAsync("session", async (entry) => {
                entry.SlidingExpiration = TimeSpan.FromDays(30);
                var url = $"{_host}/xtask.api-getsessionid.json";
                var result = await _request.GetDataAsync(url);
                return JsonConvert.DeserializeObject<SessionData>(result.data);
            });
        }


        /// <summary>
        /// <seealso>
        ///     <cref>https://devel.easycorp.cn/book/extension/xtask.api-auth-44.html</cref>
        /// </seealso>
        /// </summary>
        /// <returns></returns>
        private async Task<SessionData> PostLogin() {
            var sessionData = await GetSessionID();
            if (!string.IsNullOrEmpty(_account) && !string.IsNullOrEmpty(_password)) {
                var url = QueryHelpers.AddQueryString($"{_host}/user-login.json", new Dictionary<String, String> {
                    {sessionData.sessionName, sessionData.sessionID},
                    {"account", _account},
                    {"password", _password}
                });
                var result = await _request.PostDataAsync(url, new List<KeyValuePair<string?, string?>> {
                });
            }

            return sessionData;
        }

        public async Task<IList<TaskItem>> GetMyTaskAssignedTo() {
            var sessionData = await PostLogin();
            var url = QueryHelpers.AddQueryString($"{_host}/my-task-assignedTo.json", new Dictionary<String, String> {
                {sessionData.sessionName, sessionData.sessionID},
            });
            var result = await _request.GetDataAsync(url);

            var data = JsonConvert.DeserializeObject<TaskData>(result.data);
            return data.tasks;
        }

        public async Task<List<BugItem>> GetMyBug() {
            var sessionData = await PostLogin();
            var url = QueryHelpers.AddQueryString($"{_host}/my-bug.json", new Dictionary<String, String> {
                {sessionData.sessionName, sessionData.sessionID},
            });
            var result = await _request.GetDataAsync(url);

            var data = JsonConvert.DeserializeObject<BugData>(result.data);
            return data.bugs;
        }
    }
}