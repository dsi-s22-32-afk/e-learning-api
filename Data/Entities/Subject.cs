using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Data.Entities
{
    public class Subject : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int IconId { get; set; }

        public UploadedFile Icon { get; set; }

        public ICollection<Lecturer> Lecturers { get; set; }

        public ICollection<Training> Trainings { get; set; }
    }
}
