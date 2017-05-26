using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    public class GetMembersRequest : IPagedRequest
    {
        public int? page { get; set; }
        public string sortBy { get; set; }
        public SortDirection sortDir { get; set; } 
    }
}
