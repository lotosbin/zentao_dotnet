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
            var sessionName = configuration.GetValue<string>("ASPNETCORE_ZENTAO_SESSION_NAME");
            var sessionId = configuration.GetValue<string>("ASPNETCORE_ZENTAO_SESSION_ID");
            _zentaoClient = new ZentaoClient(memoryCache, host, sessionName, sessionId);
        }

        [Fact]
        public async void GetMyTaskAssignedToTest() {
            var task = await _zentaoClient.GetMyTaskAssignedTo();
            Assert.True(task.Count > 0);
        }
    }
}