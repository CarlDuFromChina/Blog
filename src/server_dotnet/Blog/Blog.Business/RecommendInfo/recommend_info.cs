using Sixpence.ORM.Entity;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column]
        public string name { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [DataMember]
        [Column]
        public string url { get; set; }

        /// <summary>
        /// 推荐类型
        /// </summary>
        [DataMember]
        [Column]
        public string recommend_type { get; set; }

        /// <summary>
        /// 推荐类型
        /// </summary>
        [DataMember]
        [Column]
        public string recommend_type_name { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        [DataMember]
        [Column]
        public int? reading_times { get; set; }

    }
}
