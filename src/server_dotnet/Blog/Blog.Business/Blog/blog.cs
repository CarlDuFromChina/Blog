using Newtonsoft.Json.Linq;
using Sixpence.ORM.Entity;
using System.Runtime.Serialization;

namespace Blog.Business.Blog
{
    [Entity("blog", "博客", false)]
    public partial class blog : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [PrimaryColumn]
        public string id { get; set; }


        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        [Column("blog_typename", "博客类型", DataType.Varchar, 100)]
        public string blog_typeName { get; set; }

        /// <summary>
        /// Markdown内容
        /// </summary>
        [DataMember]
        [Column("content", "内容", DataType.Text)]
        public string content { get; set; }

        /// <summary>
        /// html内容
        /// </summary>
        [DataMember]
        [Column("html_content", "html内容", DataType.Text)]
        public string html_content { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        [Column("title", "标题", DataType.Varchar, 100)]
        public string title { get; set; }


        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        [Column("blog_type", "类型", DataType.Varchar, 100)]
        public string blog_type { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        [DataMember]
        [Column("reading_times", "类型", DataType.Int4)]
        public int? reading_times { get; set; }

        /// <summary>
        /// 是否是系列
        /// </summary>
        [DataMember]
        [Column("is_series", "是否是系列", DataType.Int4)]
        public bool is_series { get; set; }

        /// <summary>
        /// 是否是系列
        /// </summary>
        [DataMember]
        [Column("is_seriesname", "是否是系列", DataType.Varchar, 100)]
        public string is_seriesName { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [DataMember]
        [Column("tags", "标签", DataType.Jsonb)]
        public JToken tags { get; set; }


        /// <summary>
        /// 禁止评论
        /// </summary>
        [DataMember]
        [Column("disable_comment", "禁止评论", DataType.Int4)]
        public bool disable_comment { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        [DataMember]
        [Column("surfaceid", "封面", DataType.Varchar, 100)]
        public string surfaceid { get; set; }

        /// <summary>
        /// 封面地址
        /// </summary>
        [DataMember]
        [Column("surface_url", "封面地址", DataType.Varchar, 200)]
        public string surface_url { get; set; }

        /// <summary>
        /// 大封面
        /// </summary>
        [DataMember]
        [Column("big_surfaceid", "大封面", DataType.Varchar, 100)]
        public string big_surfaceid { get; set; }
        
        /// <summary>
        /// 大封面地址
        /// </summary>
        [DataMember]
        [Column("big_surface_url", "大封面地址", DataType.Varchar, 200)]
        public string big_surface_url { get; set; }

        /// <summary>
        /// 是否展示
        /// </summary>
        [DataMember]
        [Column("is_show", "是否展示", DataType.Int4)]
        public bool is_show { get; set; }

        /// <summary>
        /// 是否展示
        /// </summary>
        [DataMember]
        [Column("is_showname", "是否展示", DataType.Varchar, 100)]
        public string is_showName { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        [Column("brief", "摘要", DataType.Text)]
        public string brief { get; set; }

        /// <summary>
        /// 微信素材id
        /// </summary>
        [DataMember]
        [Column("wechat_newsid", "微信素材id", DataType.Varchar, 100)]
        public string wechat_newsid { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        [DataMember]
        [Column("article_type", "文章类型", DataType.Varchar, 100)]
        public string article_type { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        [DataMember]
        [Column("article_typename", "文章类型", DataType.Varchar, 100)]
        public string article_typeName { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [DataMember]
        [Column("is_pop", "是否置顶", DataType.Int4, true, 0)]
        public bool is_pop { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [DataMember]
        [Column("is_popname", "是否置顶", DataType.Varchar, true, "否")]
        public string is_popName { get; set; }
    }
}

