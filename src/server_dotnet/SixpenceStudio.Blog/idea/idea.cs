using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.idea
{
    [EntityName("idea")]
    public partial class idea : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        private string _ideaid;
        [DataMember]
        public string ideaId
        {
            get
            {
                return this._ideaid;
            }
            set
            {
                this._ideaid = value;
                SetAttributeValue("ideaId", value);
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        private string _content;
        [DataMember]
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
