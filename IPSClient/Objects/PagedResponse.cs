using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects
{
    public class PagedResponse<T>
    {
        public int page { get; set; }
        public int perPage { get; set; }
        public int totalResults { get; set; }
        public int totalPages { get; set; }
        public List<T> results { get; set; }
    }
}
