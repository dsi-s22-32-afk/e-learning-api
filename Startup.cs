using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniWall.Data.Contexts;
using UniWall.Data.Seeders;
using UniWall.Filters;
using UniWall.Middlewares;
using UniWall.Security.Authenticators;
using UniWall.Security.Configs;
using UniWall.Uploads;
using UniWall.Utilities;
using UniWall.Validation;

namespace UniWall
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("App")));
            services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Auth")));
            
            JwtConfig jwtConfig = new() { Secret = Configuration["JwtConfig:Secret"] };
            services.Add(new ServiceDescriptor(typeof(JwtConfig), jwtConfig));

            UploadConfig uploadConfig = new();
            Configuration.GetSection("FileUpload").Bind(uploadConfig);
            services.Add(new ServiceDescriptor(typeof(UploadConfig), uploadConfig));

            services.AddTransient<ApiAuthenticator>();
            services.AddTransient<FileService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt => {
                var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

            ValidationErrorsConfig validationConfig = new();
            Configuration.GetSection("ApiErrors:Validation").Bind(validationConfig);

            services.AddControllers(config =>
            {
                config.Filters.Add(new ValidationActionFilter(validationConfig));
                config.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "University Wall", Version = "0.0.1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Name = "Bearer",
                          In = ParameterLocation.Header
                        },
                        new List<string>()
                      }
                });
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniWall v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseMiddleware<ErrorHandler>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SeedData(app, env).Wait();
        }
        private async Task SeedData(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AuthSeeder authSeeder = new(app);
            await authSeeder.Seed();

            if(env.IsDevelopment())
            {
                ApiDevSeeder devSeeder = new(app, env);
                await devSeeder.Seed();
            }
            
        }
    }
}
