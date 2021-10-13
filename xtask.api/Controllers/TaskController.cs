using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using AutoMapper;
using idea_generic_task_server.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using zentao.client.core;

namespace idea_generic_task_server.Controllers {
    /// <summary>
    /// <seealso cref="https://support.atlassian.com/jira-software-cloud/docs/advanced-search-reference-jql-fields/#Advancedsearchingfieldsreference-TypeType"/>
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase {
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskController> _logger;
        private string _host;

        public TaskController(IMemoryCache memoryCache, IConfiguration configuration, IMapper mapper,
            ILogger<TaskController> logger) {
            _host = configuration.GetValue<String>("ASPNETCORE_ZENTAO_HOST");
            _memoryCache = memoryCache;
            _mapper = mapper;
            _logger = logger;
        }

        /**
         * @see https://www.zentao.net/book/xtask.api/378.html
         */
        [HttpGet]
        public async System.Threading.Tasks.Task<IEnumerable<Task>> Get(String jql) {
            _logger.LogInformation($"get jql={jql}");
            var (username, password) = ParseUsernamePassword();
            var _zentaoClient = new ZentaoClient(_memoryCache, _host, username, password);
            var items = (await _zentaoClient.GetMyTaskAssignedTo()).Select(item => _mapper.Map<Task>(item))
                .Where(item => !item.closed);
            var bugs = (await _zentaoClient.GetMyBug()).Select(item => _mapper.Map<Task>(item))
                .Where(item => !item.closed);
            return bugs.Concat(items);
        }

        private (string username, string password) ParseUsernamePassword() {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] {':'}, 2);
            var username = credentials[0];
            var password = credentials[1];
            return (username, password);
        }
    }
}