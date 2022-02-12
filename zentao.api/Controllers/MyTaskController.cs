using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zentao.api.Auth;
using zentao.client.Core;

namespace zentao.api.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class MyTaskController : ControllerBase {
        private readonly IZentaoClient _zentaoClient;

        public MyTaskController(IZentaoClient zentaoClient) {
            _zentaoClient = zentaoClient;
        }

        [HttpGet]
        public Task<IList<TaskItem>> Get([FromQuery] ZentaoApiCredential credential) => _zentaoClient.GetMyTaskAsync(credential.host, credential.account, credential.password);
    }
}