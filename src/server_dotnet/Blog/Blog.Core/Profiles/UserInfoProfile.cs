using AutoMapper;
using Blog.Core.Auth.UserInfo;
using Sixpence.Common.Current;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Profiles
{
    public class UserInfoProfile : Profile, IProfile
    {
        public UserInfoProfile()
        {
            CreateMap<user_info, CurrentUserModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(e => e.code))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.name));
        }
    }
}
