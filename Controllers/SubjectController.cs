using AutoMapper;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UniWall.Controllers
{     public class SubjectController : BaseApiController
    {
        public SubjectController(ApiDbContext db, IMapper mapper, IWebHostEnvironment env, UserManager<IdentityUser> userManager) : base(db, mapper, env, userManager)
        {
        }

        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(SubjectResponse[]))]
        public async Task<ObjectResult> List()
        {
            IQueryable<Subject> query = _db.Subjects;
            var list = await query.ToListAsync();
            return Ok(Map<SubjectResponse>(list.ToArray()));
        }

        [HttpPost("/")]
        [ProducesResponseType(200, Type = typeof(SubjectResponse))]
        public async Task<ObjectResult> Post([FromBody] SubjectRequest request)
        {
            Subject subject = Map<Subject>(request);




            await _db.AddAsync(subject);
            await _db.SaveChangesAsync();

            return Created("", Map<SubjectResponse>(subject));


        }
        [HttpPatch("l/{id}")]
        [ProducesResponseType(202, Type = typeof(SubjectResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorsListResponse))]
        public async Task<ObjectResult> Patch([FromRoute] int id, [FromBody] SubjectRequest request)
        {
            Subject current = await _db.Subjects
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            Subject received = Map<Subject>(request);

            if (current == null)
            {
                throw new HttpException(404);
            }
            

            

            
            current.Name = received.Name;
            current.IconId = current.IconId;
            

            await _db.SaveChangesAsync();


            return Accepted(Map<SubjectResponse>(current));
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(202)]
        public async Task<StatusCodeResult> Delete([FromRoute] int id)
        {
            Subject subject = await _db.Subjects
                 
                 .Where(t => t.Id == id)
                 .FirstOrDefaultAsync();

            if (subject == null)
            {
                throw new HttpException(404);
            }

            _db.Subjects.Remove(subject);
            await _db.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(SubjectResponse))]
        public async Task<ObjectResult> Get([FromRoute] int id)
        {
            Subject subject = await _db.Subjects
                
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (subject == null)
            {
                throw new HttpException(404);
            }

            return Ok(Map<SubjectResponse>(subject));
        }
    }
  }
