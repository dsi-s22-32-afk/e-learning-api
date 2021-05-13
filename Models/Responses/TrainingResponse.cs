using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Responses
{
    public class TrainingResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long Time { get; set; }

        public int? Seats { get; set; }

        public int? BannerId { get; set; }

        public LocationResponse Location { get; set; }

        public LecturerPartialResponse[] Lecturers { get; set; }

        public SubjectPartialResponse[] Subjects { get; set; }
    }
}
