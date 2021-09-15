using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.RecommendInfo
{
    [Entity("recommend_info", "推荐信息", false)]
    public class recommend_info : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("recommend_infoid", "推荐信息id", AttrType.Varchar, 100)]
        public string recommend_infoId
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
        /// 链接地址
        /// </summary>
        [DataMember]
        [Attr("url", "链接地址", AttrType.Varchar, 400)]
        public string url { get; set; }

        /// <summary>
        /// 推荐类型
        /// </summary>
        [DataMember]
        [Attr("recommend_type", "推荐类型", AttrType.Varchar, 100)]
        public string recommend_type { get; set; }

        /// <summary>
        /// 推荐类型
        /// </summary>
        [DataMember]
        [Attr("recommend_typename", "推荐类型", AttrType.Varchar, 100)]
        public string recommend_typeName { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        [DataMember]
        [Attr("reading_times", "阅读次数", AttrType.Int4)]
        public int? reading_times { get; set; }

    }
}
