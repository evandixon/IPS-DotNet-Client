using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Downloads
{
    public class GetFilesResponse : IPagedResultResponse<GetFileResponse>
    {
        public int page { get; set; }
        public int perPage { get; set; }
        public int totalResults { get; set; }
        public int totalPages { get; set; }
        public List<GetFileResponse> results { get; set; }
    }
}
