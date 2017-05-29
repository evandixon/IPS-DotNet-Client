using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    public class EmailExistsException : ApiException
    {
        public EmailExistsException(ExceptionResponse response) : base(response)
        {
        }
    }
}
