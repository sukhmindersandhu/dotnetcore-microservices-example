using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backgroundservice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace backgroundservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IListenerService listenerService;

        public ValuesController(IHostedService listenerService,
                               TestDataContext testDataContext)
        {
            this.listenerService = listenerService as IListenerService;

            if (this.listenerService is null)
                throw new ArgumentException("IHostedService type must be of IListenerService");
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            listenerService.CreateNewListerner();
            return "Added new Lsiterner";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
