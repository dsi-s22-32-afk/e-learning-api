using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Attributes;
using UniWall.Data.Entities;

namespace UniWall.Models.Requests
{
    public class AttendeeRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "VALID.301")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VALID.302")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VALID.303")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Bio { get; set; }


        [ExistsInDatabase(typeof(Training), ErrorMessage = "VALID.304")]
        public int TrainingId { get; set; }

    }
}
