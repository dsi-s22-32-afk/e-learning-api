using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Attributes;
using UniWall.Data.Entities;

namespace UniWall.Models.Requests
{
    public class LecturerRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "VALID.201")]
        public string FirstName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "VALID.202")]
        public string LastName { get; set; }

        public string Position { get; set; }

        public string Bio { get; set; }

        [ExistsInDatabase(typeof(UploadedFile), ErrorMessage = "VALID.203")]
        public int AvatarId { get; set; }


        [ExistsInDatabase(typeof(Subject), ErrorMessage = "VALID.204")]
        public ICollection<int> Subjects { get; set; }
    }
}

