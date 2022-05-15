using Sixpence.Common.Current;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Auth
{
    public static class AuthUserExtension
    {
        public static CurrentUserModel ToCurrentUserModel(this auth_user user)
        {
            return new CurrentUserModel()
            {
                Code = user.code,
                Id = user.user_infoid,
                Name = user.name
            };
        }
    }
}
