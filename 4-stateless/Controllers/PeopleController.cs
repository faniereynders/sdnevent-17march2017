using Microsoft.AspNetCore.Mvc;
using stateless.Models;

namespace stateless.Controllers
{
    [Route("~/[controller]")]
    public class PeopleController : ControllerBase
    {

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var result = new Person
            {
                Name = "Joe",
                Surname = "Soap"
            };

            result.Links.Add(
                new Link("self", Url.Link("Get", new { id = id })));
            result.Links.Add(
                new Link("update-person", Url.Link("Put", new { id = id }),"PUT"));
            result.Links.Add(
                new Link("remove-person", Url.Link("Delete", new { id = id }), "DELETE"));
            return new ObjectResult(result);
        }

        [HttpPut("{id}", Name="Put")]
        public IActionResult Put(int id, [FromBody]Person profile)
        {
            return new NoContentResult();
        }

        [HttpDelete("{id}", Name = "Delete")]
        public IActionResult Delete(int id)
        {
            return new NoContentResult();
        }
    }
}
