using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Gallery
{
    public class GetAlbumsRequest : IPagedRequest
    {
        /// <summary>
        /// Comma-delimited list of categiry IDs - if provided, only albums in those categories are returned
        /// </summary>
        public string categories { get; set; }

        /// <summary>
        /// Comma-delimited list of member IDs - if provided, only albums owned by those members are returned
        /// </summary>
        public string owners { get; set; }

        /// <summary>
        /// What to sort by. Can be 'name', 'count_images' for number of images, or do not specify for ID
        /// </summary>
        public string sortBy { get; set; }

        /// <summary>
        /// Sort direction. Can be 'asc' or 'desc' - defaults to 'asc'
        /// </summary>
        public string sortDir { get; set; }

        /// <summary>
        /// Page number
        /// </summary>
        public int? page { get; set; }
    }
}
