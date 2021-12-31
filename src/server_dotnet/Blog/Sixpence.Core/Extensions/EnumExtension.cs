using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sixpence.Common
{
    public static class EnumExtension
    {
        /// <summary>
        /// 根据枚举的值获取枚举名称
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="status">枚举的值</param>
        /// <returns></returns>
        public static string GetEnumName<T>(this int status)
        {
            return Enum.GetName(typeof(T), status);
        }

        /// <summary>
        /// 获取枚举值的描述
        /// </summary>
        /// <param name="value"></param>
        /// <param name="nameInstead"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value, Boolean nameInstead = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute == null && nameInstead == true)
            {
                return name;
            }
            return attribute?.Description;
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="nameInstead"></param>
        /// <returns></returns>
        public static T GetValue<T>(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return default(T);
            }

            FieldInfo field = type.GetField(name);
            ValueAttribute attribute = System.Attribute.GetCustomAttribute(field, typeof(ValueAttribute)) as ValueAttribute;
            return (T)attribute?.Value;
        }

        /// <summary>
        /// 根据描述找到枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetEnum<T>(this string value)
        {
            Type type = typeof(T);
            var fields = type.GetFields();
            foreach (var item in fields)
            {
                if (item.Name.ToLower() == value.ToLower())
                {
                    return (T)item.GetValue(null);
                }
            }
            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", value));
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ValueAttribute : Attribute
    {
        public ValueAttribute(object value)
        {
            this.Value = value;
        }

        public object Value;
    }
}
