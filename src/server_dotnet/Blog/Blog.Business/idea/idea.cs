using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.idea
{
    [Entity("idea", "想法", false)]
    public partial class idea : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        [Attr("ideaid", "想法id", AttrType.Varchar, 100)]
        public string ideaId
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
        /// 内容
        /// </summary>
        private string _content;
        [DataMember]
        [Attr("content", "想法id", AttrType.Text)]
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
