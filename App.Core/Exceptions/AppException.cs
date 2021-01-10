using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace App.Core.Exceptions
{
    public class AppException : Exception
    {
        public AppException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
