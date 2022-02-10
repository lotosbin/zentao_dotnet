using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using zentao.api.Controllers.Base;
using zentao.client;
using zentao.client.core;

namespace zentao.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyTaskController : ZentaoControllerBase
    {
        private readonly ILogger<MyTaskController> _logger;

        public MyTaskController(IMemoryCache memoryCache, ILogger<MyTaskController> logger) : base(memoryCache) {
            _logger = logger;
        }

        [HttpGet]
        public Task<IList<TaskItem>> Get([FromQuery] AuthModel authModel) => CreateClient(authModel).GetMyTaskAsync();

    }
}

