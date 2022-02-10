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
    public class MyBugController : ZentaoControllerBase {
        private readonly ILogger<MyBugController> _logger;

        public MyBugController(IMemoryCache memoryCache, ILogger<MyBugController> logger) : base(memoryCache) {
            _logger = logger;
        }

        [HttpGet]
        public Task<List<BugItem>> Get([FromQuery] AuthModel authModel) => CreateClient(authModel).GetMyBugAsync();
    }
}