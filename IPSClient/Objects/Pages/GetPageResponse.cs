using IPSClient.Objects.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Pages
{
    public class GetPageResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public Category category { get; set; }
        public Dictionary<string, string> fields { get; set; }
        public Member author { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public int comments { get; set; }
        public int reviews { get; set; }
        public int views { get; set; }
        public string prefix { get; set; }
        public List<string> tags { get; set; }
        public bool locked { get; set; }
        public bool hidden { get; set; }
        public bool pinned { get; set; }
        public bool featured { get; set; }
        public string url { get; set; }
        public float rating { get; set; }
    }
}
