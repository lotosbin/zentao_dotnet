using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace idea_generic_task_server {
    public class Program {
        public static void Main(string[] args) {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            CreateHostBuilder(args).ConfigureHostConfiguration(builder => builder.AddConfiguration(config))
                .Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>();
            });
        }
    }
}