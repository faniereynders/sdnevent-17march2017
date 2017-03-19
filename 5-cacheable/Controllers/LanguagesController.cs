using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace cacheable.Controllers
{
    [Route("~/[controller]")]
    public class LanguagesController
    {
        public IActionResult Get()
        {
            var result = new
            {
                languageCodes = new Dictionary<string, string>(){
                    { "da", "Danish" },
                    { "no", "Norwegian" },
                    { "en", "English" }
                }
            };

            return new OkObjectResult(result);
        }
    }
}
