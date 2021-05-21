using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Responses
{
    public class AttendeeResponse
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Bio { get; set; }

        public int TrainingId { get; set; }

        public bool IsEmailSent { get; set; }
    }
}
