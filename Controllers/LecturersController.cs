using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Data.Contexts;
using UniWall.Data.Entities;
using UniWall.Exceptions;
using UniWall.Models.Requests;
using UniWall.Models.Responses;

namespace UniWall.Controllers
{
    [Route("v1/lecturers")]
    [ApiController]
    public class LecturersController : BaseApiController
    {
        public LecturersController(ApiDbContext db, IMapper mapper, IWebHostEnvironment env, UserManager<IdentityUser> userManager) : base(db, mapper, env, userManager)
        {
        }

        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(PaginatedListResponse<LecturerResponse>))]
        public async Task<ObjectResult> List([FromQuery] LecturersListRequest request)
        {
            IQueryable<Lecturer> query = _db.Lecturers.AsQueryable()
                .Include(l => l.Subjects);

            if (request.Subjects != null && request.Subjects.Any())
            {
                query = query.Where(l => l.Subjects.Any(s => request.Subjects.Contains(s.Id)));
            }

            return Ok(Paginate<Lecturer, LecturerResponse>(query, request.Page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(LecturerResponse))]
        public async Task<ObjectResult> Get([FromRoute] int id)
        {
            Lecturer lecturer = _db.Lecturers
               .Include(l => l.Subjects)
               .Where(l => l.Id == id)
               .FirstOrDefault();

            if (lecturer == null)
            {
                throw new HttpException(404);
            }

            return Ok(Map<LecturerResponse>(lecturer));
        }

        [HttpPost("")]
        [ProducesResponseType(201, Type = typeof(LecturerResponse))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ObjectResult> Post([FromBody] LecturerRequest request)
        {
            Lecturer lecturer = Map<Lecturer>(request);

            lecturer.Subjects = new List<Subject>();
            foreach (int id in request.Subjects)
            {
                Subject subject = await _db.Subjects.Where(item => item.Id == id).FirstAsync();
                lecturer.Subjects.Add(subject);
            }

            _db.Lecturers.Add(lecturer);
            await _db.SaveChangesAsync();

            return Created("", Map<LecturerResponse>(lecturer));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(202, Type = typeof(LecturerResponse))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ObjectResult> Patch([FromRoute] int id, [FromBody] LecturerRequest request)
        {
            Lecturer lecturer = _db.Lecturers
               .Include(l => l.Subjects)
               .Where(l => l.Id == id)
               .FirstOrDefault();

            if (lecturer == null)
            {
                throw new HttpException(404);
            }

            lecturer.FirstName = request.FirstName;
            lecturer.LastName = request.LastName;
            lecturer.Position = request.Position;
            lecturer.Bio = request.Bio;
            lecturer.AvatarId = request.AvatarId;

            lecturer.Subjects = UnifyCollections<Subject>(lecturer.Subjects, request.Subjects);

            await _db.SaveChangesAsync();

            return Accepted(Map<LecturerResponse>(lecturer));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<NoContentResult> Delete([FromRoute] int id)
        {
            Lecturer lecturer = _db.Lecturers
               .Include(l => l.Subjects)
               .Where(l => l.Id == id)
               .FirstOrDefault();

            if (lecturer == null)
            {
                throw new HttpException(404);
            }

            _db.Lecturers.Remove(lecturer);

            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
