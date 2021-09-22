using System;
using System.Collections.Generic;
using System.Text;

namespace Sixpence.Core.Extensions
{
    public static class ObjectExtension
    {
        public static Dictionary<string, object> ToDictionary(this object param)
        {
            var dic = new Dictionary<string, object>();

            if (param == null)
                return dic;

            foreach (var item in param.GetType().GetProperties())
            {
                dic.Add(item.Name, item.GetValue(param));
            }
            return dic;
        }
    }
}
