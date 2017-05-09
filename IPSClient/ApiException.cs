using IPSClient.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient
{
    public class ApiException : Exception
    {
        public ApiException(ExceptionResponse response)
        {
            Response = response;
        }

        public ExceptionResponse Response { get; set; }
    }
}
