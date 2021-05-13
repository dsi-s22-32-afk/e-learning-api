using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Data.Entities
{
    public class Training : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int? MaximumAttendees { get; set; }

        public bool IsOnline { get; set; }

        public string MeetupUrl { get; set; }

        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        public int? BannerId { get; set; }

        public UploadedFile Banner { get; set; }

        public ICollection<Lecturer> Lecturers { get; set; }

        public ICollection<Attendee> Attendees { get; set; }

        public ICollection<Subject> Subjects { get; set; }

    }
}
