using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zentao.api.Auth;
using zentao.client.Core;

namespace zentao.api.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class MyBugController : ControllerBase {
        private readonly IZentaoClient _zentaoClient;

        public MyBugController(IZentaoClient zentaoClient) {
            _zentaoClient = zentaoClient;
        }

        [HttpGet]
        public Task<List<BugItem>> Get([FromQuery] ZentaoApiCredential credential) => _zentaoClient.GetMyBugAsync(credential.host, credential.account, credential.password);
    }
}