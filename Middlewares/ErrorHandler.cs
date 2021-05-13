using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UniWall.Exceptions;
using UniWall.Models.Responses;

namespace UniWall.Middlewares
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public ErrorHandler(RequestDelegate next, IMapper mapper, IWebHostEnvironment env)
        {
            _next = next;
            _mapper = mapper;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";
                if(typeof(HttpException).IsInstanceOfType(error))
                {
                    await ProcessHttpException(response, (HttpException)error);
                }
                else
                {
                    await ProcessUnexpectedException(response, error);
                }

            }
        }


        private async Task ProcessHttpException(HttpResponse response, HttpException exception)
        {
            response.StatusCode = exception.StatusCode;
            object responseValue;
            if(exception.Errors.Length > 1)
            {
                responseValue = _mapper.Map<ErrorsListResponse>(exception);
            }
            else if(exception.Errors.Length == 1)
            {
                responseValue = _mapper.Map<ErrorResponse>(exception.Errors[0]);
            }
            else 
            {
                responseValue = null;
            }
            await response.WriteAsync(SerializeResponse(responseValue));
        }

        private async Task ProcessUnexpectedException(HttpResponse response, Exception error)
        {
            response.StatusCode = 500;
            object responseValue = _env.EnvironmentName == "Development" ? _mapper.Map<UnexpectedErrorResponse>(error) : null;
            
            await response.WriteAsync(SerializeResponse(responseValue));
        }

        private string SerializeResponse(object responseValue)
        {
            string result = "";
            
            if(responseValue != null)
            {
                result = JsonSerializer.Serialize(responseValue, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }

            return result;
        }

        
    }
}
