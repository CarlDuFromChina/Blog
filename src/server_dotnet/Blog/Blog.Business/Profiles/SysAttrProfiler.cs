using AutoMapper;
using Blog.Core;
using Sixpence.EntityFramework.Entity;
using Blog.Core.Module.SysAttrs;
using Blog.Core.Profiles;
using Sixpence.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sixpence.EntityFramework.Models;

namespace Blog.Business.Profiles
{
    public class SysAttrProfiler : Profile, IProfile
    {
        public SysAttrProfiler()
        {
            CreateMap<sys_attrs, Column>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.code))
                .ForMember(dest => dest.LogicalName, opt => opt.MapFrom(e => e.name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(e => e.attr_type.GetEnum<DataType>()))
                .ForMember(dest => dest.Length, opt => opt.MapFrom(e => e.attr_length))
                .ForMember(dest => dest.IsRequire, opt => opt.MapFrom(e => e.isrequire));
        }
    }
}
