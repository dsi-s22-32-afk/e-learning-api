using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Data.Entities
{
    public class UploadedFile : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Directory { get; set; }

        public string FileName { get; set; }

        public string MimeType { get; set; }

        public long Size { get; set; }
    }
}
