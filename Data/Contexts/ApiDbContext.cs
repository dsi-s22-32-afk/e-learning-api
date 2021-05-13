using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Data.Entities;

namespace UniWall.Data.Contexts
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<UploadedFile> UploadedFiles { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Attendee> Attendees { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Training> Trainings { get; set; }
    }
}
