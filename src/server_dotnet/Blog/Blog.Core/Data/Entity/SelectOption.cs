using Blog.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Data
{
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

    public static class SelectOptionExtension
    {
        /// <summary>
        /// 转换 BaseEntity 为选项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SelectOption ToSelectOption(this BaseEntity entity)
        {
            AssertUtil.CheckIsNullOrEmpty<SpException>(entity.name, "选项名不能为空", "C54EDDA7-CD30-4F89-A924-A7AB6D22666F");
            AssertUtil.CheckIsNullOrEmpty<SpException>(entity.Id, "选项值不能为空", "236580B0-4A9A-404B-8266-1AFC71716F37");
            return new SelectOption(entity.name, entity.Id);
        }
    }
}
