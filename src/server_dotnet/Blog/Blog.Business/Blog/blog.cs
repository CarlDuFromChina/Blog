using Newtonsoft.Json.Linq;
using Sixpence.EntityFramework.Entity;
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
        [Attr("blogid", "博客id", AttrType.Varchar, 100)]
        public string blogId
        {
            get
            {
                return this.Id;
            }
            set
            {
                this.Id = value;
            }
        }


        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        [Attr("blog_typename", "博客类型", AttrType.Varchar, 100)]
        public string blog_typeName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        [Attr("content", "内容", AttrType.Text)]
        public string content { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        [Attr("title", "标题", AttrType.Varchar, 100)]
        public string title { get; set; }


        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        [Attr("blog_type", "类型", AttrType.Varchar, 100)]
        public string blog_type { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        [DataMember]
        [Attr("reading_times", "类型", AttrType.Int4)]
        public int? reading_times { get; set; }

        /// <summary>
        /// 是否是系列
        /// </summary>
        [DataMember]
        [Attr("is_series", "是否是系列", AttrType.Int4)]
        public bool is_series { get; set; }

        /// <summary>
        /// 是否是系列
        /// </summary>
        [DataMember]
        [Attr("is_seriesname", "是否是系列", AttrType.Varchar, 100)]
        public string is_seriesName { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [DataMember]
        [Attr("tags", "标签", AttrType.JToken)]
        public JToken tags { get; set; }


        /// <summary>
        /// 禁止评论
        /// </summary>
        [DataMember]
        [Attr("disable_comment", "禁止评论", AttrType.Int4)]
        public bool disable_comment { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        [DataMember]
        [Attr("surfaceid", "封面", AttrType.Varchar, 100)]
        public string surfaceid { get; set; }

        /// <summary>
        /// 封面地址
        /// </summary>
        [DataMember]
        [Attr("surface_url", "封面地址", AttrType.Varchar, 200)]
        public string surface_url { get; set; }

        /// <summary>
        /// 大封面
        /// </summary>
        [DataMember]
        [Attr("big_surfaceid", "大封面", AttrType.Varchar, 100)]
        public string big_surfaceid { get; set; }
        
        /// <summary>
        /// 大封面地址
        /// </summary>
        [DataMember]
        [Attr("big_surface_url", "大封面地址", AttrType.Varchar, 200)]
        public string big_surface_url { get; set; }

        /// <summary>
        /// 是否展示
        /// </summary>
        [DataMember]
        [Attr("is_show", "是否展示", AttrType.Int4)]
        public bool is_show { get; set; }

        /// <summary>
        /// 是否展示
        /// </summary>
        [DataMember]
        [Attr("is_showname", "是否展示", AttrType.Varchar, 100)]
        public string is_showName { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        [Attr("brief", "摘要", AttrType.Text)]
        public string brief { get; set; }

        /// <summary>
        /// 微信素材id
        /// </summary>
        [DataMember]
        [Attr("wechat_newsid", "微信素材id", AttrType.Varchar, 100)]
        public string wechat_newsid { get; set; }
    }
}

