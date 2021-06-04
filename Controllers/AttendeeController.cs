using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Data.Contexts;
using UniWall.Data.Entities;
using UniWall.Exceptions;
using UniWall.Models.Requests;
using UniWall.Models.Responses;
using UniWall.Utilities;
using System.Configuration;
using AutoMapper.Configuration;

namespace UniWall.Controllers
{

    

    [Route("v1/attendees")]
    [ApiController]
    public class AttendeeController : BaseApiController
    {
       

        public AttendeeController(ApiDbContext db, IMapper mapper, IWebHostEnvironment env, UserManager<IdentityUser> userManager) : base(db, mapper, env, userManager)
        {
        }


        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(PaginatedListResponse<AttendeeResponse>))]
        public async Task<ObjectResult> List([FromQuery] AttendeeListRequest request)
        {
            IQueryable<Attendee> query = _db.Attendees.AsQueryable();

            return Ok(Paginate<Attendee, AttendeeResponse>(query, request.Page));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AttendeeResponse))]
        public async Task<ObjectResult> Get([FromRoute] int id)
        {
            Attendee attendee = _db.Attendees
                .Where(l => l.Id == id)
                .FirstOrDefault();

            if (attendee == null)
            {
                    throw new HttpException(404);
            }

            return Ok(Map<AttendeeResponse>(attendee));
        }

        [HttpPost("")]
        [ProducesResponseType(201, Type = typeof(AttendeeResponse))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ObjectResult> Post([FromBody] AttendeeRequest request)
        {
            Attendee attendee = Map<Attendee>(request);
            try
            {
                Training training = new Training();
                training = await _db.Trainings.Where(item => item.Id == attendee.TrainingId).FirstAsync();

                String trainingInfo;
                if (!training.IsOnline){
                    Address address = new Address();
                    address = await _db.Addresses.Where(item => item.Id == training.AddressId).FirstAsync();
                    trainingInfo = "Address: " + address.Street + " " + address.BuildingNumber + " " + address.BuildingNumber + " " + address.City;
                }
                else{
                    trainingInfo = "Url to meetup: " + training.MeetupUrl;
                }

                EmailService _emailService = new EmailService();
                _ = _emailService.SendAsync(
                     to: attendee.Email,
                     subject: "Training attendance confirmation",
                     html: $@"<h4>Thanks for enrolling into " + training.Title + @" </h4>
                         <p>Training data:</p>
                        <p> Description: " + training.Description + @"</p>
                        <p> Date: " + training.Date + @"</p>
                        <p>" + trainingInfo + @"</p>
                        <p>See you soon!</p>
                         "
                 );

                attendee.IsEmailSent = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                attendee.IsEmailSent = false;
            }

            _db.Attendees.Add(attendee);
            await _db.SaveChangesAsync();

            return Created("", Map<AttendeeResponse>(attendee));
        }


        [HttpPatch("{id}")]
        [ProducesResponseType(202, Type = typeof(AttendeeResponse))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ObjectResult> Patch([FromRoute] int id, [FromBody] AttendeeRequest request)
        {
            Attendee attendee = _db.Attendees
               .Where(l => l.Id == id)
               .FirstOrDefault();

            if (attendee == null)
            {
                throw new HttpException(404);
            }

            attendee.FirstName = request.FirstName;
            attendee.LastName = request.LastName;
            attendee.Email = request.Email;
            attendee.PhoneNumber = request.PhoneNumber;
            attendee.Bio = request.Bio;
            
            await _db.SaveChangesAsync();

            return Accepted(Map<AttendeeResponse>(attendee));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<NoContentResult> Delete([FromRoute] int id)
        {
            Attendee attendee = _db.Attendees
               .Where(l => l.Id == id)
               .FirstOrDefault();

            if (attendee == null)
            {
                throw new HttpException(404);
            }

            _db.Attendees.Remove(attendee);

            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
