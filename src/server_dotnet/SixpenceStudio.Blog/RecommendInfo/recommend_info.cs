using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.RecommendInfo
{
    [EntityName("recommend_info")]
    public class recommend_info : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _recommend_infoid;
        [DataMember]
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
        [DataMember]
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
