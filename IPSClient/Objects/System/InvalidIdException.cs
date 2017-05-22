using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    public class InvalidIdException : ApiException
    {
        public InvalidIdException(ExceptionResponse response) : base(response)
        {
        }
    }
}
