using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Forums
{
    public class Forum
    {
        public int id { get; set; }
        public string name { get; set; }
        public int topics { get; set; }
        public string url { get; set; }
    }
}
