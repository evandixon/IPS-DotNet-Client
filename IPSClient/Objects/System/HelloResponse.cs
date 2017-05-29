using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    /// <summary>
    /// Basic information about a community
    /// </summary>
    public class HelloResponse
    {
        /// <summary>
        /// Name of the community
        /// </summary>
        public string communityName { get; set; }

        /// <summary>
        /// The community URL
        /// </summary>
        public string communityUrl { get; set; }

        /// <summary>
        /// The IPS Community Suite version number
        /// </summary>
        public string ipsVersion { get; set; }
    }
}
