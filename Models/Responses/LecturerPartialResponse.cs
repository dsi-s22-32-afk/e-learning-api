using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Responses
{
    public class LecturerPartialResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AvatarId { get; set; }

        public string Position { get; set; }
    }
}
