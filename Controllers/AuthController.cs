using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Data.Contexts;
using UniWall.Exceptions;
using UniWall.Models.Requests;
using UniWall.Models.Responses;
using UniWall.Security.Authenticators;

namespace UniWall.Controllers
{
    [Route("v1", Name = "auth_")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly ApiAuthenticator _authenticator;

        public AuthController(IMapper mapper, ApiDbContext db, IWebHostEnvironment env, ApiAuthenticator authenticator, UserManager<IdentityUser> userManager) 
            : base(db, mapper, env, userManager)
        {
            _authenticator = authenticator;
        }

        [HttpPost("login", Name = "login")]
        public async Task<ObjectResult> Login(LoginRequest request)
        {
            IdentityUser user = GetUser();
            string token = await _authenticator.GetJwtForCredentials(request);

            return Ok(new TokenResponse(){ Token = token });
        }

    }
}
