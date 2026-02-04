using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.Commons
{
    public class ValidationError
    {
        [JsonPropertyName("field")]
        public string Field { get; set; }
        [JsonPropertyName("errors")]

        public List<string> Errors { get; set; }

    }
}
