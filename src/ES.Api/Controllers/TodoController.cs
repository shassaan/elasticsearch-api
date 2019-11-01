using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ES.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;

namespace ES.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> logger;
        private readonly IElasticClient elasticClient;
        public TodoController(IElasticClient elasticClient, ILogger<TodoController> logger)
        {
            this.elasticClient = elasticClient;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> Get()
        {
            var posts = await Task.Run<List<TodoItem>>(()=> new List<TodoItem>());
            return posts;
        }
        [HttpPost]
        public async Task<ActionResult<TodoItem>> Post([FromBody]TodoItem item)
        {
            item.Id = Guid.NewGuid();
            await elasticClient.IndexDocumentAsync(item);
            return item;
        }
    }
}