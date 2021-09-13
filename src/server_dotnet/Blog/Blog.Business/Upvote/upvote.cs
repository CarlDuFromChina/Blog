using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Blog.Business.Upvote
{
    [Entity("upvote", "点赞")]
    public class upvote : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("upvoteid", "点赞id", AttrType.Varchar, 100)]
        public string upvoteId
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
        /// 对象Id
        /// </summary>
        private string _objectid;
        [DataMember]
        [Attr("objectid", "实体Id", AttrType.Varchar, 100)]
        public string objectId
        {
            get
            {
                return this._objectid;
            }
            set
            {
                this._objectid = value;
                SetAttributeValue("objectId", value);
            }
        }

        public string _objectidName;
        /// <summary>
        /// 对象名
        /// </summary>
        [DataMember]
        [Attr("objectidname", "实体名", AttrType.Varchar, 100)]
        public string objectIdName
        {
            get
            {
                return this._objectidName;
            }
            set
            {
                this._objectidName = value;
                SetAttributeValue("objectIdName", value);
            }
        }

        public string _object_ownerid;
        /// <summary>
        /// 对象拥有者
        /// </summary>
        [DataMember]
        [Attr("object_ownerid", "对象拥有者", AttrType.Varchar, 100)]
        public string object_ownerid
        {
            get
            {
                return this._object_ownerid;
            }
            set
            {
                this._object_ownerid = value;
                SetAttributeValue("object_ownerid", value);
            }
        }

        public string _object_owneridName;
        /// <summary>
        /// 对象拥有者
        /// </summary>
        [DataMember]
        [Attr("object_owneridName", "对象拥有者", AttrType.Varchar, 100)]
        public string object_owneridName
        {
            get
            {
                return this._object_owneridName;
            }
            set
            {
                this._object_owneridName = value;
                SetAttributeValue("object_owneridName", value);
            }
        }

        /// <summary>
        /// 点赞类型
        /// </summary>
        private string _object_type;
        [DataMember]
        [Attr("object_type", "实体Id", AttrType.Varchar, 100)]
        public string object_type
        {
            get
            {
                return this._object_type;
            }
            set
            {
                this._object_type = value;
                SetAttributeValue("object_type", value);
            }
        }
    }
}
