using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sixpence.Core.Utils
{
    /// <summary>
    /// 反射帮助类（SixpenceStudio*.dll）
    /// </summary>
    public class AssemblyUtil
    {
        public static readonly ConcurrentDictionary<string, List<Assembly>> assemblies = new ConcurrentDictionary<string, List<Assembly>>();

        static AssemblyUtil() { }

        /// <summary>
        /// 获取所有Assembly实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Assembly> GetAssemblies(params string[] name)
        {
            if (name == null || name.Length == 0) return new List<Assembly>() { Assembly.GetCallingAssembly() };

            var list = new List<Assembly>();
            for (int i = 0; i < name.Length; i++)
            {
                list.AddRange(assemblies.GetOrAdd(name[i], key =>
                {
                    var fileList = FileUtil.GetFileList(key);
                    return fileList?.Select(item => Assembly.Load(AssemblyName.GetAssemblyName(item)))?.ToList();
                }));
            }
            return list;
        }

        /// <summary>
        /// 获取所有程序集中继承对应接口的Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<Type> GetTypes<T>(string name)
        {
            var list = GetAssemblies(name)
                .SelectMany(assembly => assembly.GetTypes().Where(item => !item.IsInterface && !item.IsAbstract && item.GetInterfaces().Contains(typeof(T))))
                .ToList();

            return list;
        }
    }
}
