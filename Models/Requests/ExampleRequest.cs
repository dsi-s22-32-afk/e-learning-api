using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Requests
{
    public class ExampleRequest
    {
        [StringLength(10, MinimumLength = 3, ErrorMessage = "VALID.002")]
        public string SomeStringValue { get; set; }

        [Range(10, 20, ErrorMessage = "VALID.001")]
        public int SomeIntValue { get; set; }
    }
}
