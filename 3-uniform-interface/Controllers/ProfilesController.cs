using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using uniform_interface.Models;

namespace uniform_interface.Controllers
{
    [Route("~/[controller]")]
    public class ProfilesController
    {
        private IList<Profile> profiles = new List<Profile>();
        private IEnumerable<Profile> profilesWithFriends;

        public ProfilesController(IOptions<List<Profile>> profileOptions)
        {
            foreach (var profile in profileOptions.Value)
            {
                profiles.Add(new Profile
                {
                    Id = profile.Id,
                    Name = profile.Name,
                    Surname = profile.Surname
                });
                profilesWithFriends = profileOptions.Value;
            }
        }

        [HttpGet]
        public IActionResult GetAll(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new OkObjectResult(profiles);
            }
            else
            {
                var result = profiles.Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                return new OkObjectResult(result);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var result = profiles.SingleOrDefault(p => p.Id == id);
            return new ObjectResult(result);
        }

        [HttpGet("{id}/friends")]
        public IActionResult GetOneFriends(int id)
        {
            var result = profilesWithFriends.SingleOrDefault(p => p.Id == id);
            return new ObjectResult(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Profile profile)
        {
            return new CreatedAtActionResult("GetOne", "Profiles", new { id = 1 }, profile);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Profile profile)
        {
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new NoContentResult();
        }
    }
}
