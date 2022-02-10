using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using zentao.api.Controllers.Base;
using zentao.client;
using zentao.client.core;

namespace zentao.api.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class MyStoreController : ZentaoControllerBase {
        private readonly ILogger<MyStoreController> _logger;

        public MyStoreController(IMemoryCache memoryCache, ILogger<MyStoreController> logger) : base(memoryCache) {
            _logger = logger;
        }

        [HttpGet]
        public Task<IList<StoryItem>> Get([FromQuery] AuthModel authModel) => CreateClient(authModel).GetMyStoryListAsync();
    }
}