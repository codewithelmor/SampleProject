using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;

namespace SampleProject.Common.Exceptions
{
    public class CustomException : Exception
    {
        public IEnumerable<IdentityError> IdentityErrors { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public override string Message { get; }

        public CustomException(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
            IdentityErrors = new List<IdentityError>();
        }

        public CustomException(IEnumerable<IdentityError> errors)
        {
            StatusCode = HttpStatusCode.UnprocessableEntity;
            Message = "Oops. Something went wrong.";
            IdentityErrors = errors;
        }
    }
}