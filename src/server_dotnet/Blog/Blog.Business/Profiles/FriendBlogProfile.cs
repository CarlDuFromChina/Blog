using AutoMapper;
using Blog.Core;
using Blog.Core.Profiles;
using Blog.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Profiles
{
    public class FriendBlogProfile : Profile, IProfile
    {
        public FriendBlogProfile()
        {
            CreateMap<Jobs.SyncBlogJob.BlogDetail, FriendBlog.friend_blog>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(item => item.id))
                .ForMember(dest => dest.name, opt => opt.MapFrom(item => item.title))
                .ForMember(dest => dest.content, opt => opt.MapFrom(item => item.content))
                .ForMember(dest => dest.description, opt => opt.MapFrom(item => item.description))
                .ForMember(dest => dest.createdOn, opt => opt.MapFrom(item => item.createTime.ToDateTime()))
                .ForMember(dest => dest.modifiedOn, opt => opt.MapFrom(item => item.updateTime.ToDateTime()))
                .ForMember(dest => dest.first_picture, opt => opt.MapFrom(item => item.firstPicture));
        }
    }
}
