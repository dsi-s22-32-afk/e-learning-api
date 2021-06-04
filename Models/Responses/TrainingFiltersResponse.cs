using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Responses
{
    public class TrainingFiltersResponse
    {
        public ICollection<KeyValResponse<string>> Cities { get; set; }
        public ICollection<KeyValResponse<int>> Subjects { get; set; }
    }
}
