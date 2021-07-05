using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Pixabay
{
    public class VideosModel : BasePixabayResponseModel<VideoModel> { }
    public class VideoModel
    {
        public int id { get; set; }
        public string pageURL { get; set; }
        public string type { get; set; }
        public int duration { get; set; }
        public string picture_id { get; set; }
        public int views { get; set; }
        public int downloads { get; set; }
        public int favorites { get; set; }
        public int likes { get; set; }
        public int comments { get; set; }
        public int user_id { get; set; }
        public string user { get; set; }
        public string userImageURL { get; set; }
        public Videos videos { get; set; }
    }

    public class Videos
    {
        public Video large { get; set; }
        public Video medium { get; set; }
        public Video small { get; set; }
        public Video tiny { get; set; }
    }

    public class Video
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int size { get; set; }
    }
}
