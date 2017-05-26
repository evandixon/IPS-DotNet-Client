using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Downloads
{
    /// <summary>
    /// Information about a single file inside a logical file in the Downloads app.
    /// </summary>
    public class FilePart
    {
        public string name { get; set; }
        public string url { get; set; }
        public int size { get; set; }
    }
}
