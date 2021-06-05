using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Requests
{
    public class LecturersListRequest
    {
        public int Page { get; set; }

        public IEnumerable<int> Subjects { get; set; } 
    }
}
