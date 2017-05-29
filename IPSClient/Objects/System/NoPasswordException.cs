using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    public class NoPasswordException : ApiException
    {
        public NoPasswordException(ExceptionResponse response) : base(response)
        {
        }
    }
}
