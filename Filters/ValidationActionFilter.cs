using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UniWall.Exceptions;
using UniWall.Models.Responses;
using UniWall.Validation;

namespace UniWall.Filters
{
    public class ValidationActionFilter : IActionFilter
    {

        private readonly ValidationErrorsConfig _errorsConfig;

        public ValidationActionFilter(ValidationErrorsConfig errorsConfig)
        {
            _errorsConfig = errorsConfig;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                ErrorsListResponse responseValue = CreateResponseValue(context.ModelState);

                HttpResponse response = context.HttpContext.Response;
                response.ContentType = "application/json";
                response.StatusCode = 400;
                response.WriteAsync(JsonSerializer.Serialize(responseValue, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })).Wait();
                
            }
        }

        private ErrorsListResponse CreateResponseValue(ModelStateDictionary state)
        {
            List<ErrorResponse> modelViolations = new List<ErrorResponse>();
            foreach (string key in state.Keys)
            {
                List<ErrorResponse> propertyViolations = GetErrorsForProperty(key, state[key]);
                modelViolations.AddRange(propertyViolations);
            }

            return new() { Errors = modelViolations.ToArray() };
        }

        private List<ErrorResponse> GetErrorsForProperty(string propertyName, ModelStateEntry entry)
        {
            List<ErrorResponse> violations = new List<ErrorResponse>();
            
            foreach(ModelError error in entry.Errors)
            {
                ApiError violation = _errorsConfig.Errors.Single(item => item.Code == error.ErrorMessage || item.Message == error.ErrorMessage);
                if(violation == null)
                {
                    violation = new() { Code = "VALID.000", Message = "Invalid property value" };
                }
                violations.Add(new() { Code = violation.Code, Message = violation.Message, Path = propertyName });
            }

            return violations;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }
    }
}
