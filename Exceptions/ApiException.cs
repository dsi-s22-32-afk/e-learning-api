using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string code, string message, string path = null) : base(message)
        {
            Code = code;
            Path = path;
        }

        public string Code { get; set; }

        public string Path { get; set; }
    }
}
