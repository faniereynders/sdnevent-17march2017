using System.Collections.Generic;
using Newtonsoft.Json;

namespace stateless.Models
{
    public abstract class Resource{
        [JsonProperty("_links", Order = -2)]
        public List<Link> Links { get; set; } = new List<Link>();
        
    }

    public class Link{
        public Link(string rel, string href, string method = null)
        {
            Rel = rel;
            Href = href;
            Method = method;
        }
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
    }
    public class Person : Resource
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

}