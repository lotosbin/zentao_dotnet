using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zentao.api.Auth;
using zentao.client.Core;

namespace zentao.api.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class MyStoreController : ControllerBase {
        private readonly IZentaoClient _zentaoClient;

        public MyStoreController(IZentaoClient zentaoClient) {
            _zentaoClient = zentaoClient;
        }

        [HttpGet]
        public Task<IList<StoryItem>> Get([FromQuery] ZentaoApiCredential credential) => _zentaoClient.GetMyStoryListAsync(credential.host, credential.account, credential.password);
    }
}