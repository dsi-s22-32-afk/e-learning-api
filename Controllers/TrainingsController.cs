using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
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

namespace UniWall.Controllers
{
    [Route("v1/trainings")]
    [ApiController]
    public class TrainingsController : BaseApiController
    {
        public TrainingsController(ApiDbContext db, IMapper mapper, IWebHostEnvironment env, UserManager<IdentityUser> userManager) : base(db, mapper, env, userManager)
        {
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PaginatedListResponse<TrainingResponse>))]
        public async Task<ObjectResult> List([FromQuery] TrainingListRequest request)
        {
            IQueryable<Training> query = _db.Trainings
                .Include(t => t.Address)
                .Include(t => t.Lecturers)
                .Include(t => t.Subjects)
                .Where(t => t.Date > DateTime.Now)
                .OrderBy(t => t.Date);

            if (request.City != null)
            {
                query = query.Where(t => t.Address != null && t.Address.City == request.City);
            }
            if (request.MaxTime != null)
            {
                DateTime time = TimeUtil.FromMiliseconds((long)request.MaxTime);
                query = query.Where(t => t.Date <= time);
            }
            if (request.MinTime != null)
            {
                DateTime time = TimeUtil.FromMiliseconds((long)request.MinTime);
                query = query.Where(t => t.Date >= time);
            }
            if (request.Subjects != null)
            {
                query = query.Where(t => t.Subjects != null && t.Subjects.Select(s => s.Id).Intersect(request.Subjects).Any());
            }
            if (request.OnlineOnly != null)
            {
                query = query.Where(t => t.IsOnline);
            }

            return Ok(Paginate<Training, TrainingResponse>(query, request.Page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TrainingResponse))]
        public async Task<ObjectResult> Get([FromRoute] int id)
        {
            Training training = await _db.Trainings
                .Include(t => t.Address)
                .Include(t => t.Lecturers)
                .Include(t => t.Subjects)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
            
            if(training == null)
            {
                throw new HttpException(404);
            }

            return Ok(Map<TrainingResponse>(training));
        }

        [ProducesResponseType(201, Type = typeof(TrainingResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorsListResponse))]
        [HttpPost]
        public async Task<ObjectResult> Post([FromBody] TrainingRequest request)
        {
            Training training = Map<Training>(request);
            training.Attendees = new List<Attendee>();
            training.Subjects = new List<Subject>();
            training.Lecturers = new List<Lecturer>();
            
            foreach(int id in request.Subjects)
            {
                Subject subject = await _db.Subjects.Where(item => item.Id == id).FirstAsync();
                training.Subjects.Add(subject);
            }

            foreach (int id in request.Lecturers)
            {
                Lecturer lecturer = await _db.Lecturers.Where(item => item.Id == id).FirstAsync();
                training.Lecturers.Add(lecturer);
            }



            await _db.AddAsync(training);
            await _db.SaveChangesAsync();

            return Created("", Map<TrainingResponse>(training));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(202, Type = typeof(TrainingResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorsListResponse))]
        public async Task<ObjectResult> Patch([FromRoute] int id, [FromBody] TrainingRequest request)
        {
            Training current = await _db.Trainings
                 .Include(t => t.Address)
                 .Include(t => t.Lecturers)
                 .Include(t => t.Subjects)
                 .Where(t => t.Id == id)
                 .FirstOrDefaultAsync();

            if (current == null)
            {
                throw new HttpException(404);
            }

            Training received = Map<Training>(request);

            if (!current.Address.Equals(received.Address))
            {
                current.AddressId = null;
                current.Address = received.Address;
            }

            current.Subjects = UnifyCollections(current.Subjects, request.Subjects);
            current.Lecturers = UnifyCollections(current.Lecturers, request.Lecturers);

            current.Title = received.Title;
            current.Description = received.Description;
            current.Date = current.Date;
            current.MaximumAttendees = received.MaximumAttendees;
            current.IsOnline = received.IsOnline;
            current.MeetupUrl = received.MeetupUrl;
            current.BannerId = received.BannerId;
            current.Lecturers = received.Lecturers;
            current.Subjects = received.Subjects;

            await _db.SaveChangesAsync();

            return Accepted(Map<TrainingResponse>(current));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<StatusCodeResult> Delete([FromRoute] int id)
        {
            Training training = await _db.Trainings
                 .Include(t => t.Address)
                 .Include(t => t.Lecturers)
                 .Include(t => t.Subjects)
                 .Where(t => t.Id == id)
                 .FirstOrDefaultAsync();

            if (training == null)
            {
                throw new HttpException(404);
            }

            _db.Trainings.Remove(training);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
