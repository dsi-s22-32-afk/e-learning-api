using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Responses
{
    public class UnexpectedErrorResponse
    {
        public string Code { get; set; } = "U.0";
        public string Message { get; set; }

        public string Source { get; set; }

        public string StackTrace { get; set; }
    }
}
