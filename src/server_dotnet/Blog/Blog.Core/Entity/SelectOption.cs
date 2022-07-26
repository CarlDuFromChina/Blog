using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entity
{
    public class SelectOption
    {
        #region 构造函数
        public SelectOption() { }

        public SelectOption(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
        #endregion

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
