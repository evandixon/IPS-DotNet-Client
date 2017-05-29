using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    /// <summary>
    /// 
    /// </summary>
    public class Group
    {
        /// <summary>
        /// ID number
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Name with formatting
        /// </summary>
        public string formattedName { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
