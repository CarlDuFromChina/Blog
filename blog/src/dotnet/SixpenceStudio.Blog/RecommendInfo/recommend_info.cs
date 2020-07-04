using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.RecommendInfo
{
    public class recommend_info : BaseEntity
    {
        public recommend_info()
        {
            this.EntityName = "recommend_info";
        }

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
        /// 创建人
        /// </summary>
        private string _createdby;
        [DataMember]
        public string createdBy
        {
            get
            {
                return this._createdby;
            }
            set
            {
                this._createdby = value;
                SetAttributeValue("CreatedBy", value);
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdbyname;
        [DataMember]
        public string createdByName
        {
            get
            {
                return this._createdbyname;
            }
            set
            {
                this._createdbyname = value;
                SetAttributeValue("CreatedByName", value);
            }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        private DateTime? _createdon;
        [DataMember]
        public DateTime? createdOn
        {
            get
            {
                return this._createdon;
            }
            set
            {
                this._createdon = value;
                SetAttributeValue("CreatedOn", value);
            }
        }

        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedby;
        [DataMember]
        public string modifiedBy
        {
            get
            {
                return this._modifiedby;
            }
            set
            {
                this._modifiedby = value;
                SetAttributeValue("ModifiedBy", value);
            }
        }

        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedbyname;
        [DataMember]
        public string modifiedByName
        {
            get
            {
                return this._modifiedbyname;
            }
            set
            {
                this._modifiedbyname = value;
                SetAttributeValue("ModifiedByName", value);
            }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        private DateTime? _modifiedon;
        [DataMember]
        public DateTime? modifiedOn
        {
            get
            {
                return this._modifiedon;
            }
            set
            {
                this._modifiedon = value;
                SetAttributeValue("ModifiedOn", value);
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
