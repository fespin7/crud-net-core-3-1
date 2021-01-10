using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace App.Core.Exceptions
{
    public class BusinessException : AppException
    {
        public BusinessException(string message) : base(HttpStatusCode.BadRequest, message)
        {

        }
    }
}
