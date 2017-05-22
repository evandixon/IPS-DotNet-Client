using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Downloads
{
    public class InvalidVersionException : ApiException
    {
        public InvalidVersionException(ExceptionResponse response) : base(response)
        {
        }
    }
}
