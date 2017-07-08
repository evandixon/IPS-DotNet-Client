using IPSClient.Objects.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Gallery
{
    public class Album
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
        /// Description
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// The category
        /// </summary>
        public Category category { get; set; }

        /// <summary>
        /// The owner
        /// </summary>
        public Member owner { get; set; }

        /// <summary>
        /// 'public', 'private' (can only be viewed by owner) or 'restricted' (can only be viewed by owner or approved members)
        /// </summary>
        public string privacy { get; set; }

        /// <summary>
        /// If the album is restricted, the members who can view it, in addition to the owner and moderators with appropriate permission
        /// </summary>
        public List<Member> approvedMembers { get; set; }

        /// <summary>
        /// Number of images
        /// </summary>
        public int images { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string url { get; set; }
    }
}
