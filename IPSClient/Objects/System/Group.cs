using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public string formattedName { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
