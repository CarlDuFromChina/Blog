namespace Blog.Business.Blog.Sync.Juejin
{
    public class JuejinTagInfo
    {
        public string tag_id { get;set; }
        public JuejinTag tag { get;set; }
        public JuejinUserInteract user_interact { get;set; }
    }

    public class JuejinTag
    {
        public int id { get; set; }
        public string tag_id { get; set; }
        public string tag_name { get; set; }
        public string color { get; set; }
        public string icon { get; set; }
        public string back_ground { get; set; }
        public int show_navi { get; set; }
        public int ctime { get; set; }
        public int mtime { get; set; }
        public int id_type { get; set; }
        public string tag_alias { get; set; }
        public int post_article_count { get; set; }
        public int concern_user_count { get; set; }
    }

    public class JuejinUserInteract
    {
        public int id { get; set; }
        public int omitempty { get; set; }
        public int user_id { get; set; }
        public string is_digg { get; set; }
        public string is_follow { get; set; }
        public string is_collect { get; set; }
    }
}
