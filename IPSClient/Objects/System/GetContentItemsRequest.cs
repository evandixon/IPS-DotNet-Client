using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.System
{
    public class GetContentItemsRequest : IPagedRequest
    {
        /// <summary>
        /// Comma-delimited list of category IDs
        /// </summary>
        public string categories { get; set; }

        /// <summary>
        /// Comma-delimited list of member IDs - if provided, only files started by those members are returned
        /// </summary>
        public string authors { get; set; }

        /// <summary>
        /// If 1, only files which are locked are returned, if 0 only unlocked
        /// </summary>
        public bool? locked { get; set; }

        /// <summary>
        /// If 1, only files which are hidden are returned, if 0 only not hidden
        /// </summary>
        public bool? hidden { get; set; }

        /// <summary>
        /// If 1, only files which are pinned are returned, if 0 only not pinned
        /// </summary>
        public bool? pinned { get; set; }

        /// <summary>
        /// If 1, only files which are featured are returned, if 0 only not featured
        /// </summary>
        public bool? featured { get; set; }

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
