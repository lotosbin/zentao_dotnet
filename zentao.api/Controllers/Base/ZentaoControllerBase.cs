using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using zentao.client.core;

namespace zentao.api.Controllers.Base;

public class ZentaoControllerBase : ControllerBase {
    private readonly IMemoryCache _memoryCache;

    protected ZentaoControllerBase(IMemoryCache memoryCache) {
        _memoryCache = memoryCache;
    }
    protected ZentaoClient CreateClient(AuthModel authModel) => new(_memoryCache, authModel.host, authModel.account, authModel.password);
}