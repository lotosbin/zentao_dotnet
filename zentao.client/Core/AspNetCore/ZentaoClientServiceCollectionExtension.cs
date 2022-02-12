using Microsoft.Extensions.DependencyInjection;
using zentao.client.Core.Data.Core;

namespace zentao.client.Core.AspNetCore;

public static class ZentaoClientServiceCollectionExtension {
    // ReSharper disable once UnusedMethodReturnValue.Global
    public static IServiceCollection AddZentaoClient(this IServiceCollection services) {
        services.AddTransient<IZentaoClient, ZentaoClient>();
        return services;
    }
}