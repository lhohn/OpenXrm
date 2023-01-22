using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenXrm.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitiesController : ControllerBase
    {
        // GET: api/<EntitiesController>
        [HttpGet]
        public IEnumerable<string> Get(string entity,string? id)
        {
            return new string[] { "value1", "value2" };
        }



        // POST api/<EntitiesController>
        [HttpPost]
        public void Post(string entity, [FromBody] JsonObject value)
        {
        }

        // PUT api/<EntitiesController>/5
        [HttpPut]
        public void Put(string entity, int id, [FromBody] JsonObject value)
        {
        }

        // DELETE api/<EntitiesController>/5
        [HttpDelete]
        public void Delete(string entity, int id)
        {
        }
    }
}
