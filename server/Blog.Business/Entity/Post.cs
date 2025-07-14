using Newtonsoft.Json.Linq;
using Sixpence.ORM.Entity;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Blog.Business.Entity
{
    [Entity("post", "博客")]
    public partial class Post : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [PrimaryColumn(primaryType: PrimaryType.GUIDNumber)]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DataMember, Column, Description("类型")]
        public string post_type { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DataMember, Column, Description("类型")]
        public string post_type_name { get; set; }

        /// <summary>
        /// Markdown内容
        /// </summary>
        [DataMember, Column, Description("Markdown内容")]
        public string content { get; set; }

        /// <summary>
        /// html内容
        /// </summary>
        [DataMember, Column, Description("html内容")]
        public string html_content { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember, Column, Description("标题")]
        public string title { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        [DataMember, Column, Description("阅读次数")]
        public int? reading_times { get; set; }

        /// <summary>
        /// 是否是系列
        /// </summary>
        [DataMember, Column, Description("是否是系列")]
        public bool? is_series { get; set; }

        /// <summary>
        /// 是否是系列
        /// </summary>
        [DataMember, Column, Description("是否是系列")]
        public string is_series_name { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [DataMember, Column, Description("标签")]
        public JToken tags { get; set; }

        /// <summary>
        /// 禁止评论
        /// </summary>
        [DataMember, Column, Description("禁止评论")]
        public bool? disable_comment { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        [DataMember, Column, Description("封面")]
        public string surfaceid { get; set; }

        /// <summary>
        /// 封面地址
        /// </summary>
        [DataMember, Column, Description("封面地址")]
        public string surface_url { get; set; }

        /// <summary>
        /// 大封面
        /// </summary>
        [DataMember, Column, Description("大封面")]
        public string big_surfaceid { get; set; }

        /// <summary>
        /// 大封面地址
        /// </summary>
        [DataMember, Column, Description("大封面地址")]
        public string big_surface_url { get; set; }

        /// <summary>
        /// 是否展示
        /// </summary>
        [DataMember, Column, Description("是否展示")]
        public bool? is_show { get; set; }

        /// <summary>
        /// 是否展示
        /// </summary>
        [DataMember, Column, Description("是否展示")]
        public string is_show_name { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember, Column, Description("摘要")]
        public string brief { get; set; }

        /// <summary>
        /// 微信素材id
        /// </summary>
        [DataMember, Column, Description("微信素材id")]
        public string wechat_newsid { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        [DataMember, Column, Description("文章类型")]
        public string article_type { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        [DataMember, Column, Description("文章类型")]
        public string article_type_name { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [DataMember, Column, Description("是否置顶")]
        public bool? is_pop { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [DataMember, Column, Description("是否置顶")]
        public string is_pop_name { get; set; }
    }
}

