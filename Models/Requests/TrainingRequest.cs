using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Attributes;
using UniWall.Data.Entities;

namespace UniWall.Models.Requests
{
    public class TrainingRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "VALID.101")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VALID.102")]
        public string Description { get; set; }

        [FutureTimestamp(ErrorMessage = "VALID.103")]
        public long Time { get; set; }

        public int? MaximumAttendees { get; set; }

        public bool Online { get; set; }

        public string MeetupUrl { get; set; }

        [ExistsInDatabase(typeof(UploadedFile), ErrorMessage = "VALID.108")]
        public int? BannerId { get; set; }

        public AddressRequest Address { get; set; }

        [ExistsInDatabase(typeof(Subject), ErrorMessage = "VALID.109")]
        public ICollection<int> Subjects { get; set; }

        [ExistsInDatabase(typeof(Lecturer), ErrorMessage = "VALID.110")]
        public ICollection<int> Lecturers { get; set; }
    }
}
