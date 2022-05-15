using AutoMapper;
using Sixpence.Common;
using Sixpence.Common.IoC;
using Sixpence.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Blog.Core.Profiles
{
    public class MapperHelper
    {
        public static TDestination Map<TDestination>(object source)
        {
            var mapper = ServiceContainer.Resolve<IMapper>();
            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// 通过反射自动化注册
        /// </summary>
        /// <returns></returns>
        public static Type[] MapType()
        {
            Type[] types = AssemblyUtil.GetTypes<IProfile>("Blog.*.dll").ToArray();

            List<Type> allList = new List<Type>();

            foreach (Type item in types)
            {
                if (item.IsInterface) continue;//判断是否是接口
                Type[] ins = item.GetInterfaces();
                foreach (Type ty in ins)
                {
                    if (ty == typeof(IProfile))
                    {
                        allList.Add(item);
                    }
                }
            }

            Type[] alltypes = allList.ToArray();
            return alltypes;
        }
    }
}
