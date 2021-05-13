using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
    [Route("v1/resources", Name = "upload_")]
    public class ResourcesController : BaseApiController
    {
        private readonly FileService _fileService;
        public ResourcesController(FileService fileService, ApiDbContext db, IMapper mapper, IWebHostEnvironment env, UserManager<IdentityUser> userManager)
            : base(db, mapper, env, userManager)
        {
            _fileService = fileService;
        }

        [HttpPost("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ObjectResult> Post([FromForm] UploadRequest request)
        {
            UploadedFile file = await _fileService.ProcessFile(request.File);
            UploadResponse responseData = Map<UploadResponse>(file);
            string url = Url.Action("Get", "Resource", file.Id) ?? "";
            
            return Created(url, responseData);
        }

        [HttpGet("{id}")]
        public async Task<FileContentResult> Get([FromRoute] int id)
        {
            UploadedFile file = await _db.FindAsync<UploadedFile>(id);
            if(file == null)
            {
                throw new HttpException(404);
            }

            byte[] content = System.IO.File.ReadAllBytes(Path.Combine(file.Directory, file.FileName));

            return File(content, file.MimeType);
        }
    }
}
