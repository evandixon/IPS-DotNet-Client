using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Gallery
{
    public class CreateImageRequest
    {
        public CreateImageRequest()
        {
        }

        /// <param name="album">The ID number of the album the image should be created in - not required if category is provided (only provide one or the other)</param>
        /// <param name="category">The ID number of the category the image should be created in - not required if album is provided (only provide one or the other)</param>
        /// <param name="author">The ID number of the member uploading the image (0 for guest)</param>
        /// <param name="caption">The image caption</param>
        /// <param name="filename">The image filename (e.g. 'image.png')</param>
        /// <param name="image">Raw data of the image</param>
        public CreateImageRequest(int? album, int? category, int author, string caption, string filename, byte[] image)
        {
            this.album = album;
            this.category = category;
            this.author = author;
            this.caption = caption;
            this.filename = filename;
            SetImage(image);
        }

        /// <summary>
        /// The ID number of the album the image should be created in - not required if category is provided (only provide one or the other)
        /// </summary>
        public int? album { get; set; }

        /// <summary>
        /// The ID number of the category the image should be created in - not required if album is provided (only provide one or the other)
        /// </summary>
        public int? category { get; set; }

        /// <summary>
        /// The ID number of the member uploading the image (0 for guest)
        /// </summary>
        public int author { get; set; }

        /// <summary>
        /// The image caption
        /// </summary>
        public string caption { get; set; }

        /// <summary>
        /// The image filename (e.g. 'image.png')
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// The base64 encoded image contents
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// The description as HTML (e.g. "<p>This is an image.</p>")
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// The copyright
        /// </summary>
        public string copyright { get; set; }

        /// <summary>
        /// The credit information
        /// </summary>
        public string credit { get; set; }

        /// <summary>
        /// If the image contains the location in it's EXIF data, 1/0 indicating if a map should be shown (defaults to 1)
        /// </summary>
        public int gpsShow { get; set; }

        /// <summary>
        /// Prefix tag
        /// </summary>
        public string prefix { get; set; }

        /// <summary>
        /// Comma-separated list of tags (do not include prefix)
        /// </summary>
        public string tags { get; set; }

        /// <summary>
        /// The date/time that should be used for the image post date. If not provided, will use the current date/time
        /// </summary>
        public DateTime? date { get; set; }

        /// <summary>
        /// The IP address that should be stored for the image. If not provided, will use the IP address from the API request
        /// </summary>
        public string ip_address { get; set; }

        /// <summary>
        /// 1/0 indicating if the image should be locked
        /// </summary>
        public int locked { get; set; }

        /// <summary>
        /// 0 = unhidden; 1 = hidden, pending moderator approval; -1 = hidden (as if hidden by a moderator)
        /// </summary>
        public int hidden { get; set; }

        /// <summary>
        /// 1/0 indicating if the image should be locked
        /// </summary>
        public int pinned { get; set; }

        /// <summary>
        /// 1/0 indicating if the image should be featured
        /// </summary>
        public int featured { get; set; }

        /// <summary>
        /// Sets <see cref="image"/> according from the given raw image data
        /// </summary>
        /// <param name="image">Raw data of the image</param>
        public void SetImage(byte[] image)
        {
            this.image = Convert.ToBase64String(image);
        }
    }
}
