using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Gallery
{
    public class GetImagesRequest : IPagedRequest
    {
        /// <summary>
        /// Comma-delimited list of category IDs (will also include images in albums in those categories)
        /// </summary>
        public string categories { get; set; }

        /// <summary>
        /// Comma-delimited list of album IDs
        /// </summary>
        public string albums { get; set; }

        /// <summary>
        /// Comma-delimited list of member IDs - if provided, only images started by those members are returned
        /// </summary>
        public string authors { get; set; }

        /// <summary>
        /// If 1, only images which are locked are returned, if 0 only unlocked
        /// </summary>
        public int? locked { get; set; }

        /// <summary>
        /// If 1, only images which are hidden are returned, if 0 only not hidden
        /// </summary>
        public int? hidden { get; set; }

        /// <summary>
        /// If 1, only images which are featured are returned, if 0 only not featured
        /// </summary>
        public int? featured { get; set; }

        /// <summary>
        /// What to sort by. Can be 'date' for creation date, 'title' or leave unspecified for ID
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
