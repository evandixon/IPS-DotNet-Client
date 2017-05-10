using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects
{
    public interface IPagedResultResponse<T>
    {
        int page { get; set; }
        int perPage { get; set; }
        int totalResults { get; set; }
        int totalPages { get; set; }
        List<T> results { get; set; }
    }
}
