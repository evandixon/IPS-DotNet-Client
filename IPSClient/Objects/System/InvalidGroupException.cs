using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    public class InvalidGroupException : ApiException
    {
        public InvalidGroupException(ExceptionResponse response) : base(response)
        {
        }
    }
}
