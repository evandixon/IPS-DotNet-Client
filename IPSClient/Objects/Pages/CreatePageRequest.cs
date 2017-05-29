using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Pages
{
    public class CreatePageRequest
    {
        public int category { get; set; }
        public int author { get; set; }

        /// <summary>
        /// Field values. Keys should be the field ID, and the value should be the value.
        /// </summary>
        public Dictionary<int, string> fields { get; set; }

        public string prefix { get; set; }
        public string tags { get; set; }
        public DateTime? date { get; set; }
        public string ip_address { get; set; }
        public int? locked { get; set; }
        public int? hidden { get; set; }
        public int? pinned { get; set; }
        public int? featured { get; set; }
    }
}
