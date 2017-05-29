using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Pages
{
    public class DatabaseNotFoundException : ApiException
    {
        public DatabaseNotFoundException(ExceptionResponse response) : base(response)
        {
        }
    }
}
