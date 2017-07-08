using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Gallery
{
    public class Geolocation
    {
        /// <summary>
        /// Latitude
        /// </summary>
        public float lat { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public float @long { get; set; }

        /// <summary>
        /// Lines of the street address
        /// </summary>
        public List<string> addressLines { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// State/Region
        /// </summary>
        public string region { get; set; }

        /// <summary>
        /// 2-letter country code
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// ZIP/Postal Code
        /// </summary>
        public string postalCode { get; set; }
    }
}
