using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    public class UsernameExistsException : ApiException
    {
        public UsernameExistsException(ExceptionResponse response) : base(response)
        {
        }
    }
}
