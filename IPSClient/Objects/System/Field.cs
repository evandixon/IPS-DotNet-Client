using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    public class Field
    {
        public string name { get; set; }
        public string value { get; set; }

        public override string ToString()
        {
            return name + "=" + value;
        }
    }

}
