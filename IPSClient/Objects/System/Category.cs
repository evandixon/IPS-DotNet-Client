using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
