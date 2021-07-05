using System.Collections.Generic;

namespace Blog.Core.Pixabay
{
    public class BasePixabayResponseModel<T>
    {
        /// <summary>
        /// The total number of hits.
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// The number of images accessible through the API. By default, the API is limited to return a maximum of 500 images per query.
        /// </summary>
        public int totalHits { get; set; }

        /// <summary>
        /// The total number of hits.
        /// </summary>
        public IList<T> hits { get; set; }
    }
}
