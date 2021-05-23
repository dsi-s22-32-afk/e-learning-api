using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Attributes;
using UniWall.Data.Entities;

namespace UniWall.Models.Requests
{
    public class SubjectRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "VALID.301")]
        public string Name { get; set; }
        [ExistsInDatabase(typeof(UploadedFile), ErrorMessage = "VALID.302")]
        public int IconId { get; set; }


    }
}
