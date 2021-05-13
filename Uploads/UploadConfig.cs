using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Uploads
{
    public class UploadConfig
    {
        public string[] AllowedMimeTypes { get; set; }

        public int MaxSize { get; set; }

        public string DestinationDir { get; set; }
    }
}
