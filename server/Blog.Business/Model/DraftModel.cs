using System.Collections.Generic;

namespace Blog.Business.Model
{

    public class DraftModel
    {
        public DraftModel()
        {
            category_id = "0";
            edit_type = 10;
            html_content = "deprecated";
            tag_ids = new List<string>();
        }

        public string id { get; set; }

        public string title { get; set; }

        public string category_id { get; set; }

        public string brief_content { get; set; }

        public string cover_image { get; set; }

        public string html_content { get; set; }

        public int edit_type { get; set; }

        public string link_url { get; set; }

        public string mark_content { get; set; }

        public List<string> tag_ids { get; set; }

        public int is_english { get; set; }
        public int is_gfw { get; set; }
        public int is_original { get; set; }
    }
}
