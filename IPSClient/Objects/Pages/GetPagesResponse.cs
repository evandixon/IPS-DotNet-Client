using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Pages
{
    public class GetPagesResponse : IPagedResultResponse<GetPageResponse>
    {
        public int page { get; set; }
        public int perPage { get; set; }
        public int totalResults { get; set; }
        public int totalPages { get; set; }
        public List<GetPageResponse> results { get; set; }
    }
}
