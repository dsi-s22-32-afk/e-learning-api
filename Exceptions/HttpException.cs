using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Exceptions
{
    public class HttpException : Exception
    {
        public int StatusCode { get; set; }

        public ApiException[] Errors { get; set; }

        public HttpException(int statusCode)
        {
            StatusCode = statusCode;
            Errors = Array.Empty<ApiException>();
        }

        public HttpException(int statusCode, ApiException error)
        {
            StatusCode = statusCode;
            Errors = new ApiException[] { error };
        }

        public HttpException(int statusCode, ApiException[] errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public HttpException()
        {
            StatusCode = 500;
            Errors = Array.Empty<ApiException>();
        }
    }
}
