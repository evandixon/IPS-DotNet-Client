using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects
{
    public interface IPagedRequest
    {
        int? page { get; set; }
    }
}
