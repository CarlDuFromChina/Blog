using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.RecommendInfo
{
    [Entity("recommend_info", "推荐信息", false)]
    public class recommend_info : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _recommend_infoid;
        [DataMember]
        [Attr("recommend_infoid", "推荐信息id", AttrType.Varchar, 100)]
        public string recommend_infoId
        {
            get
            {
                return this._recommend_infoid;
            }
            set
            {
                this._recommend_infoid = value;
                SetAttributeValue("recommend_infoId", value);
            }
        }

        /// <summary>
        /// 链接地址
        /// </summary>
        private string _url;
        [DataMember]
        [Attr("url", "链接地址", AttrType.Varchar, 400)]
        public string url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
                SetAttributeValue("url", value);
            }
        }

        /// <summary>
        /// 推荐类型
        /// </summary>
        private string _recommend_type;
        [Attr("recommend_type", "推荐类型", AttrType.Varchar, 100)]
        public string recommend_type
        {
            get
            {
                return this._recommend_type;
            }
            set
            {
                this._recommend_type = value;
                SetAttributeValue("recommend_type", value);
            }
        }

        /// <summary>
        /// 推荐类型
        /// </summary>
        private string _recommend_typeName;
        [DataMember]
        [Attr("recommend_typename", "推荐类型", AttrType.Varchar, 100)]
        public string recommend_typeName
        {
            get
            {
                return this._recommend_typeName;
            }
            set
            {
                this._recommend_typeName = value;
                SetAttributeValue("recommend_typeName", value);
            }
        }

        /// <summary>
        /// 阅读次数
        /// </summary>
        private int? _reading_times;
        [DataMember]
        [Attr("reading_times", "阅读次数", AttrType.Int4)]
        public int? reading_times
        {
            get
            {
                return this._reading_times;
            }
            set
            {
                this._reading_times = value;
                SetAttributeValue("reading_times", value);
            }
        }

    }
}
