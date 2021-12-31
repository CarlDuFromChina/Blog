using Sixpence.Common;
using Sixpence.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.SelectOption
{
    /// <summary>
    /// 选项集
    /// </summary>
    public class SelectOption
    {
        public SelectOption() { }

        public SelectOption(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
