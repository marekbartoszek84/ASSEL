using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assel.University.Console
{
    public class University
    {
        public string? Name { get; set; }
        [JsonPropertyName("state-province")]
        public string? StateProvince { get; set; }
        public List<string>? Domains { get; set; }
        [JsonPropertyName("web_pages")]
        public List<string>? WebPages { get; set; }
        [JsonPropertyName("alpha_two_code")]
        public string? AlphaTwoCode {get; set;}
        public string? Country { get; set; }
    }
}
