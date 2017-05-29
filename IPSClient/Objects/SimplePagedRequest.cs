using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects
{
    /// <summary>
    /// A simple request with a single `page` parameter
    /// </summary>
    public class SimplePagedRequest : IPagedRequest
    {

        public SimplePagedRequest(int page)
        {
            this.page = page;
        }

        public int? page { get; set; }
    }
}
