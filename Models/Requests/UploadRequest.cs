using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Requests
{
    public class UploadRequest
    {
        public IFormFile File { get; set; }
    }
}
