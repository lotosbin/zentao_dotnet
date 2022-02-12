using Microsoft.Extensions.Configuration;
using Xunit;
using zentao.client.Core;

namespace zentao.client.tests {
    public class ZentaoClientUnitTest {
        private readonly IZentaoClient _zentaoClient;
        private readonly string _host;
        private readonly string _account;
        private readonly string _password;

        public ZentaoClientUnitTest(IConfiguration configuration, IZentaoClient zentaoClient) {
            _host = configuration.GetValue<string>("ASPNETCORE_ZENTAO_HOST");
            _account = configuration.GetValue<string>("ASPNETCORE_ZENTAO_ACCOUNT");
            _password = configuration.GetValue<string>("ASPNETCORE_ZENTAO_PASSWORD");
            _zentaoClient = zentaoClient;
        }

        [Fact]
        public async void GetMyTasks() {
            var task = await _zentaoClient.GetMyTaskAsync(_host, _account, _password);
            Assert.True(task.Count > 0);
        }
    }
}