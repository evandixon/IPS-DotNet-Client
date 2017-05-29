using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    public class NoUsernameOrEmailException : ApiException
    {
        public NoUsernameOrEmailException(ExceptionResponse response) : base(response)
        {
        }
    }
}
