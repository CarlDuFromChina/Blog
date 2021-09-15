using AutoMapper;
using Blog.Core;
using Blog.Core.Data;
using Blog.Core.Module.SysAttrs;
using Blog.Core.Profiles;
using Sixpence.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Profiles
{
    public class SysAttrProfiler : Profile, IProfile
    {
        public SysAttrProfiler()
        {
            CreateMap<sys_attrs, Column>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.code))
                .ForMember(dest => dest.LogicalName, opt => opt.MapFrom(e => e.name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(e => e.attr_type.GetEnum<AttrType>()))
                .ForMember(dest => dest.Length, opt => opt.MapFrom(e => e.attr_length))
                .ForMember(dest => dest.IsRequire, opt => opt.MapFrom(e => e.isrequire));
        }
    }
}
