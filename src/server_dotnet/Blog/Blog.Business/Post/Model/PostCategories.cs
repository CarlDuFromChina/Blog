using System.Collections.Generic;

namespace Blog.Business.Post.Model
{
    public class PostCategories
    {
        public int count { get; set; }
        public List<Category> data { get; set; }
    }

    public class Category
    {
        public string category { get; set; }
        public string category_name { get; set; }
        public List<CategoryData> data { get; set; }
    }

    public class CategoryData
    {
        public string id { get; set; }
        public string title { get; set; }
    }
}
