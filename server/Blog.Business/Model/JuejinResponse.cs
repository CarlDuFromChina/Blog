using System.Collections.Generic;

namespace Blog.Business.Model
{
    public class JuejinResponse<T> where T : class, new()
    {
        /// <summary>
        /// 0 success
        /// </summary>
        public int err_no { get; set; }
        public string err_msg { get; set; }
        public T data { get; set; }
    }

    public class JuejinDraftCreateResponseData
    {
        public string id { get; set; }
        public string article_id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public List<string> tag_ids { get; set; }
        public string link_url { get; set; }
        public string cover_image { get; set; }
        public int is_gfw { get; set; }
        public string title { get; set; }
        public string brief_content { get; set; }
        public int is_english { get; set; }
        public int is_original { get; set; }
        public int edit_type { get; set; }
        public string html_content { get; set; }
        public string mark_content { get; set; }
        public string ctime { get; set; }
        public string mtime { get; set; }
        public int status { get; set; }
        public int original_type { get; set; }
    }

    public class JuejinDraftPublish
    {
        public string article_id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public string tag_ids { get; set; }
        public int visible_level { get; set; }
        public string link_url { get; set; }
        public string cover_image { get; set; }
        public int is_gfw { get; set; }
        public string title { get; set; }
        public string brief_content { get; set; }
        public int is_english { get; set; }
        public int is_original { get; set; }
        public int user_index { get; set; }
        public int original_type { get; set; }
        public string original_author { get; set; }
        public string content { get; set; }
        public string ctime { get; set; }
        public string mtime { get; set; }
        public string rtime { get; set; }
        public int status { get; set; }
        public int verify_status { get; set; }
        public int audit_status { get; set; }
        public string mark_content { get; set; }
        public string org_id { get; set; }
    }
}
