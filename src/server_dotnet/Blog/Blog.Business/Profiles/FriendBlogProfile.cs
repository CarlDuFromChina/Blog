using AutoMapper;
using Blog.Core;
using Blog.Core.Auth;
using Blog.Core.Profiles;
using Sixpence.Core;
using Sixpence.Core.Utils;
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
                .ForMember(dest => dest.createdBy, opt => opt.MapFrom(item => UserIdentityUtil.GetCurrentUserId()))
                .ForMember(dest => dest.createdByName, opt => opt.MapFrom(item => UserIdentityUtil.GetCurrentUser().Name))
                .ForMember(dest => dest.modifiedOn, opt => opt.MapFrom(item => item.updateTime.ToDateTime()))
                .ForMember(dest => dest.modifiedBy, opt => opt.MapFrom(item => UserIdentityUtil.GetCurrentUserId()))
                .ForMember(dest => dest.modifiedByName, opt => opt.MapFrom(item => UserIdentityUtil.GetCurrentUser().Name))
                .ForMember(dest => dest.first_picture, opt => opt.MapFrom(item => item.firstPicture));
        }
    }
}
