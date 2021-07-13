using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Pixabay
{
    public class ImagesModel : BasePixabayResponseModel<ImageModel> { }

    public class ImageModel
    {
        /// <summary>
        /// A unique identifier for this image.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Source page on Pixabay, which provides a download link for the original image of the dimension imageWidth x imageHeight and the file size imageSize.
        /// </summary>
        public string pageURL { get; set; }
        public string type { get; set; }
        public string tags { get; set; }

        /// <summary>
        /// Low resolution images with a maximum width or height of 150 px (previewWidth x previewHeight).
        /// </summary>
        public string previewURL { get; set; }
        public int previewWidth { get; set; }
        public int previewHeight { get; set; }

        /// <summary>
        /// Medium sized image with a maximum width or height of 640 px (webformatWidth x webformatHeight). URL valid for 24 hours.
        /// </summary>
        public string webformatURL { get; set; }
        public int webformatWidth { get; set; }
        public int webformatHeight { get; set; }

        /// <summary>
        /// Scaled image with a maximum width/height of 1280px.
        /// </summary>
        public string largeImageURL { get; set; }
        public string fullHDURL { get; set; }
        public string imageURL { get; set; }
        public int imageWidth { get; set; }
        public int imageHeight { get; set; }
        public int imageSize { get; set; }

        /// <summary>
        /// Total number of views.
        /// </summary>
        public int views { get; set; }

        /// <summary>
        /// Total number of downloads.
        /// </summary>
        public int downloads { get; set; }

        /// <summary>
        /// Total number of favorites.
        /// </summary>
        public int favorites { get; set; }

        /// <summary>
        /// Total number of likes.
        /// </summary>
        public int likes { get; set; }

        /// <summary>
        /// Total number of comments.
        /// </summary>
        public int comments { get; set; }

        /// <summary>
        /// User ID and name of the contributor. Profile URL: https://pixabay.com/users/{ USERNAME }-{ ID }/
        /// </summary>
        public int user_id { get; set; }
        public string user { get; set; }

        /// <summary>
        /// Profile picture URL (250 x 250 px).
        /// </summary>
        public string userImageURL { get; set; }
    }

}
