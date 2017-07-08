using IPSClient.Objects.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Gallery
{
    public class Image
    {
        /// <summary>
        /// ID number
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Caption
        /// </summary>
        public string caption { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Original file name (e.g. 'image.png')
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// Original file size, in bytes
        /// </summary>
        public string filesize { get; set; }

        /// <summary>
        /// URLs to where the images are stored
        /// </summary>
        public ImageUrls images { get; set; }

        /// <summary>
        /// The album, if in one
        /// </summary>
        public Album album { get; set; }

        /// <summary>
        /// The category (if in an album, this will be the category that the album is in)
        /// </summary>
        public Category category { get; set; }

        /// <summary>
        /// The author
        /// </summary>
        public Member author { get; set; }

        /// <summary>
        /// Copyright
        /// </summary>
        public string copyright { get; set; }

        /// <summary>
        /// Credit
        /// </summary>
        public string credit { get; set; }

        /// <summary>
        /// The location where the picture was taken, if it was able to be retreived from the EXIF data
        /// </summary>
        public Geolocation location { get; set; }

        /// <summary>
        /// The raw EXIF data
        /// </summary>
        public object exif { get; set; }

        /// <summary>
        /// Date image was uploaded
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// Number of comments
        /// </summary>
        public int comments { get; set; }

        /// <summary>
        /// Number of reviews
        /// </summary>
        public int reviews { get; set; }

        /// <summary>
        /// Number of views
        /// </summary>
        public int views { get; set; }

        /// <summary>
        /// The prefix tag, if there is one
        /// </summary>
        public string prefix { get; set; }

        /// <summary>
        /// The tags
        /// </summary>
        public List<string> tags { get; set; }

        /// <summary>
        /// Image is locked
        /// </summary>
        public bool locked { get; set; }

        /// <summary>
        /// Image is hidden
        /// </summary>
        public bool hidden { get; set; }

        /// <summary>
        /// Image is featured
        /// </summary>
        public bool featured { get; set; }

        /// <summary>
        /// Image is pinned
        /// </summary>
        public bool pinned { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// Average Rating
        /// </summary>
        public float rating { get; set; }
    }
}
