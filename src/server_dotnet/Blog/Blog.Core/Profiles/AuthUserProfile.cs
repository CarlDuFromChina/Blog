using AutoMapper;
using Blog.Core.Auth;
using Sixpence.Common.Current;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Profiles
{
    public class AuthUserProfile : Profile, IProfile
    {
        public AuthUserProfile()
        {
            CreateMap<auth_user, CurrentUserModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(e => e.code))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.name));

            CreateMap<JwtTokenModel, CurrentUserModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(e => e.Code))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Name));
        }
    }
}
