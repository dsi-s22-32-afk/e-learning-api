using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UniWall.Models.Responses
{
    public class ErrorResponse
    {
        public string Code { get; set; }

        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Path { get; set; }
    }
}
