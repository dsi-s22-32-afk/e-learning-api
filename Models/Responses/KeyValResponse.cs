using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Responses
{
    public class KeyValResponse<T>
    {
        public string Key { get; set; }

        public T Value { get; set; }
    }
}
