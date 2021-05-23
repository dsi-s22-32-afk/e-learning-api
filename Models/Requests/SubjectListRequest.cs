using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Requests
{
    public class SubjectListRequest
    {
        public IEnumerable<string> Subject { get; set; }
    }
}
