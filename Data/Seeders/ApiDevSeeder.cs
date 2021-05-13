using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Data.Contexts;
using UniWall.Data.Entities;

namespace UniWall.Data.Seeders
{
    public class ApiDevSeeder : ISeeder
    {
        private readonly ApiDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public ApiDevSeeder(IApplicationBuilder app, IWebHostEnvironment env)
        {
            IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            DbContextOptions<ApiDbContext> dbOptions = serviceProvider.GetRequiredService<DbContextOptions<ApiDbContext>>();

            _dbContext = new ApiDbContext(dbOptions);
            _env = env;
        }

        public async Task Seed()
        {
            if(_env.EnvironmentName == "Production")
            {
                throw new Exception("Do not seed in production!");
            }

            if(!_dbContext.Trainings.Any())
            {
                Address address = new()
                {
                    Street = "Grunwaldzka",
                    BuildingNumber = "100B",
                    ApartmentNumber = 10,
                    City = "Gdańsk",
                    PostalCode = "80-000"
                };

                _dbContext.Add(address);
                await _dbContext.SaveChangesAsync();

                UploadedFile[] files = new UploadedFile[]
                {
                    new() { Directory = Path.Combine(_env.ContentRootPath, "Uploads", "Defaults", "lecturer-default.png") },
                    new() { Directory = Path.Combine(_env.ContentRootPath, "Uploads", "Defaults", "lecturer-default.png") },
                    new() { Directory = Path.Combine(_env.ContentRootPath, "Uploads", "Defaults", "lecturer-default.png") },
                    new() { Directory = Path.Combine(_env.ContentRootPath, "Uploads", "Defaults", "tech-default.png") },
                    new() { Directory = Path.Combine(_env.ContentRootPath, "Uploads", "Defaults", "tech-default.png") }
                };
                _dbContext.AddRange(files);
                await _dbContext.SaveChangesAsync();

                Subject[] subjects = new Subject[] 
                {
                    new() { Name = "C++", IconId = files[3].Id },
                    new() { Name = "C", IconId = files[4].Id }
                };
                _dbContext.AddRange(subjects);
                await _dbContext.SaveChangesAsync();

                Lecturer[] lecturers = new Lecturer[]
                {
                    new() { FirstName = "Jan", LastName = "Kowalski", Position = "Tech Lead at SomeCompany", Bio = "Hello, I am Janek", AvatarId = files[0].Id, Subjects = subjects },
                    new() { FirstName = "Anna", LastName = "Kowalska", Position = "Tech Lead at SomeOtherCompany", Bio = "Hello, I am Anna", AvatarId = files[1].Id, Subjects = subjects },
                };
                _dbContext.AddRange(lecturers);
                await _dbContext.SaveChangesAsync();

                Training[] trainings = new Training[]
                {
                    new() { Title = "C/C++ Training Gdańsk", Description = "Training in GDA", AddressId = address.Id, MaximumAttendees = 5, IsOnline = false, Subjects = subjects, Lecturers = lecturers, Date = new DateTime(2021, 9, 15) },
                    new() { Title = "C/C++ Training in your house", AddressId = null, Description = "Training in Online",  MaximumAttendees = 250, IsOnline = true, MeetupUrl = "http://localhost", Subjects = subjects, Lecturers = lecturers, Date = new DateTime(2021, 7, 13) },
                };
                _dbContext.AddRange(trainings);
                await _dbContext.SaveChangesAsync();

                Attendee[] attendees = new Attendee[]
                {
                    new() { TrainingId = trainings[0].Id, FirstName = "Jan", LastName = "Pietrzak", Email = "pietrzak@fake-email.com", PhoneNumber = "0123456789", IsEmailSent = true },
                    new() { TrainingId = trainings[0].Id, FirstName = "Marek", LastName = "Kowalski", Email = "kowalski@fake-email.com", PhoneNumber = "0123456789", IsEmailSent = true },
                    new() { TrainingId = trainings[1].Id, FirstName = "Stefania", LastName = "Gieruń", Email = "gierun@fake-email.com", PhoneNumber = "0123456789", IsEmailSent = true },
                    new() { TrainingId = trainings[1].Id, FirstName = "Henryk", LastName = "Szepański", Email = "szczepan@fake-email.com", PhoneNumber = "0123456789", IsEmailSent = true },
                    new() { TrainingId = trainings[1].Id, FirstName = "Filip", LastName = "Bogdanowicz", Email = "bogdan@fake-email.com", PhoneNumber = "0123456789", IsEmailSent = true }
                };
                _dbContext.AddRange(attendees);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
