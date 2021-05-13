using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Requests
{
    public class TrainingListRequest
    {
        public int? Page { get; set; }

        public long? MinTime { get; set; }

        public long? MaxTime { get; set; }


        public string City { get; set; }

        public int[] Subjects { get; set; }

        public bool? OnlineOnly { get; set; }
    }
}
