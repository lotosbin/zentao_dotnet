using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Xunit;
using zentao.client.core;

namespace zentao.client.tests {
    public class ZentaoClientUnitTest {
        private readonly ZentaoClient _zentaoClient;

        public ZentaoClientUnitTest(IConfiguration configuration, IMemoryCache memoryCache) {
            var host = configuration.GetValue<string>("ASPNETCORE_ZENTAO_HOST");
            var account = configuration.GetValue<string>("ASPNETCORE_ZENTAO_ACCOUNT");
            var password = configuration.GetValue<string>("ASPNETCORE_ZENTAO_PASSWORD");
            _zentaoClient = new ZentaoClient(memoryCache, host, account, password);
        }

        [Fact]
        public async void GetMyTasks() {
            var task = await _zentaoClient.GetMyTaskAsync();
            Assert.True(task.Count > 0);
        }
    }
}