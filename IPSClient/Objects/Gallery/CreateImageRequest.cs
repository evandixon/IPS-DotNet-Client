using System;
using System.Collections.Generic;
using System.Text;

namespace IPSClient.Objects.Gallery
{
    public class CreateImageRequest : EditImageRequest
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
        /// The image filename (e.g. 'image.png')
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// The base64 encoded image contents
        /// </summary>
        public string image { get; set; }       

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
