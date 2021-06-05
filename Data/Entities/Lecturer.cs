using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Data.Entities
{
    public class Lecturer : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string FirstName { get; set;  }


        public string LastName { get; set; }

        public string Position { get; set; }

        public string Bio { get; set; }

        public int AvatarId { get; set; }

        public UploadedFile Avatar { get; set; }

        public ICollection<Subject> Subjects { get; set; }

        public ICollection<Training> Trainings { get; set; }
    }

   
}
