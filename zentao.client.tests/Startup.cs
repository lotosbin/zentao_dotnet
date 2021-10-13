using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using zentao.client.core;

namespace zentao.client.tests {
    public class Startup {
        public void ConfigureHost(IHostBuilder hostBuilder) {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            hostBuilder.ConfigureHostConfiguration(builder => builder.AddConfiguration(config))
                .ConfigureServices((context, services) => {
                    services.AddMemoryCache();
                    services.AddSingleton<ZentaoClient>();
                });
        }
    }
}