using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    public class FieldGroup
    {
        public string name { get; set; }
        public List<Field> fields { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
