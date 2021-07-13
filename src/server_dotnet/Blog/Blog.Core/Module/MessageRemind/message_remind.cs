
using Blog.Core.Data;
using System;
using System.Runtime.Serialization;

namespace Blog.Core.Module.MessageRemind
{
    [Entity("message_remind", "消息提醒", true)]
    public partial class message_remind : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("message_remindid", "实体id", AttrType.Varchar, 100)]
        public string message_remindId
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
        /// 关联实体id
        /// </summary>
        private string _object_id;
        [DataMember]
        [Attr("object_id", "关联实体id", AttrType.Varchar, 100)]
        public string object_id
        {
            get
            {
                return this._object_id;
            }
            set
            {
                this._object_id = value;
                SetAttributeValue("object_id", value);
            }
        }


        /// <summary>
        /// 关联实体name
        /// </summary>
        private string _object_idName;
        [DataMember]
        [Attr("object_idname", "关联实体名", AttrType.Varchar, 100)]
        public string object_idName
        {
            get
            {
                return this._object_idName;
            }
            set
            {
                this._object_idName = value;
                SetAttributeValue("object_idName", value);
            }
        }


        /// <summary>
        /// 实体
        /// </summary>
        private string _object_type;
        [DataMember]
        [Attr("object_type", "实体类型", AttrType.Varchar, 100)]
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


        /// <summary>
        /// 实体名
        /// </summary>
        private string _object_typeName;
        [DataMember]
        [Attr("object_typename", "实体名", AttrType.Varchar, 100)]
        public string object_typeName
        {
            get
            {
                return this._object_typeName;
            }
            set
            {
                this._object_typeName = value;
                SetAttributeValue("object_typeName", value);
            }
        }


        /// <summary>
        /// 是否阅读
        /// </summary>
        private bool _is_read;
        [DataMember]
        [Attr("is_read", "实体名", AttrType.Int4)]
        public bool is_read
        {
            get
            {
                return this._is_read;
            }
            set
            {
                this._is_read = value;
                SetAttributeValue("is_read", value);
            }
        }


        /// <summary>
        /// 是否阅读
        /// </summary>
        private string _is_readName;
        [DataMember]
        [Attr("is_readname", "实体名", AttrType.Varchar, 100)]
        public string is_readName
        {
            get
            {
                return this._is_readName;
            }
            set
            {
                this._is_readName = value;
                SetAttributeValue("is_readName", value);
            }
        }


        /// <summary>
        /// 消息内容
        /// </summary>
        private string _content;
        [DataMember]
        [Attr("content", "消息内容", AttrType.Varchar, 500)]
        public string content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
                SetAttributeValue("content", value);
            }
        }

    }
}

