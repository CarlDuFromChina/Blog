using Blog.Core;
using Blog.Core.Auth;
using Blog.Core.Auth.UserInfo;
using Blog.Core.Data;
using Blog.Core.Module.Role;
using Blog.Core.Profiles;
using Blog.Core.Utils;
using Blog.WeChat;
using Blog.WeChat.Material;
using Blog.WeChat.WeChatNews;
using System;
using System.Collections.Generic;

namespace Blog.Blog
{
    public class BlogService : EntityService<blog>
    {
        public const string BLOG_SURFACE_NAME = "blog_surface";
        public const string BLOG_CONTENT_NAME = "blog_content";

        #region 构造函数
        public BlogService()
        {
            _context = new EntityContext<blog>();
        }

        public BlogService(IPersistBroker broker)
        {
            _context = new EntityContext<blog>(broker);
        }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Sql = "SELECT * FROM blog",
                    ViewId = "C94EDAAE-0C59-41E6-A373-D4816C2FD882",
                    CustomFilter = new List<string>(){ "title" },
                    Name = "全部博客",
                    OrderBy = "blog.createdOn desc, blog.title, blog.blogid"
                },
                new EntityView()
                {
                    Sql = $@"
SELECT
	blog.blogid,
	blog.title,
	blog.blog_type,
	blog.blog_typeName,
	blog.createdBy,
	blog.createdbyname,
	blog.modifiedBy,
	blog.modifiedbyname,
	blog.createdOn,
	blog.modifiedOn,
	blog.is_series,
	blog.tags,
	COALESCE(blog.reading_times, 0) reading_times,
	COALESCE(blog.upvote_times, 0) upvote_times,
	(SELECT COUNT(1) FROM comments WHERE objectid = blog.blogid) message,
	blog.surfaceid,
	blog.surface_url
FROM
	blog
WHERE 1=1 AND blog.is_show = 1 AND blog.is_series = 0
",
                    ViewId = "463BE7FE-5435-4841-A365-C9C946C0D655",
                    CustomFilter = new List<string>() { "title" },
                    Name = "展示的博客",
                    OrderBy = "blog.modifiedOn desc, blog.title, blog.blogid"
                },
                new EntityView()
                {
                    Sql = $@"SELECT * FROM blog WHERE blog.is_series = 1",
                    ViewId = "ACCE50D6-81A5-4240-BD82-126A50764FAB",
                    CustomFilter = new List<string>() { "title" },
                    Name = "全部系列",
                    OrderBy = "blog.createdOn desc, blog.title, blog.blogid"
                }
            };
        }

        /// <summary>
        /// 根据id查询博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override blog GetData(string id)
        {
            return Broker.ExecuteTransaction(() =>
            {
                var sql = @"
UPDATE blog SET reading_times = COALESCE(reading_times, 0) + 1 WHERE blogid = @id
";
                var data = base.GetData(id);
                Broker.Execute(sql, new Dictionary<string, object>() { { "@id", id } });
                return data;
            });
        }

        /// <summary>
        /// 获取博客路由
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetBlogRouterNameList()
        {
            var sql = @"
SELECT
	router
FROM
	sys_menu 
WHERE
	parentid = '7EB12A4C-2698-4A8B-956D-B2467BE1D886'
";
            return Broker.DbClient.Query<string>(sql);
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="blogId"></param>
        public void Upvote(string blogId)
        {
            var sql = @"
UPDATE blog SET upvote_times = COALESCE(upvote_times, 0) + 1 WHERE blogid = @id
";
            Broker.Execute(sql, new Dictionary<string, object>() { { "@id", blogId } });
        }

        /// <summary>
        /// 同步到微信图文素材
        /// </summary>
        /// <param name="id"></param>
        /// <param name="htmlContent"></param>
        public void SyncToWeChat(string id, string htmlContent)
        {
            Broker.ExecuteTransaction(() =>
            {
                var data = GetData(id);
                var media = new WeChatMaterialService(Broker).CreateData(MaterialType.image, data.surfaceid);
                new WeChatNewsService(Broker).CreateData(data.title, media, data.createdByName, "", true, htmlContent, "", true, false);
            });
        }

        /// <summary>
        /// 获取创作记录日历
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlogActivityModel> GetActivity()
        {
            var sql = @"
SELECT
	to_char(createdon, 'YYYY-MM-DD') AS created_date,
	count(1) AS count
FROM blog
WHERE createdon > to_date(to_char(now(), 'YYYY-01-01'), 'YYYY-MM-DD') AND createdon < to_date(to_char(now(), 'YYYY-12-31'), 'YYYY-MM-DD')
GROUP BY to_char(createdon, 'YYYY-MM-DD')
";
            return Broker.Query<BlogActivityModel>(sql);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public LoginResponse SignUp(LoginRequest model)
        {
            AssertUtil.CheckIsNullOrEmpty<SpException>(model.code, "账号不能为空", "");
            AssertUtil.CheckBoolean<SpException>(!model.code.Contains("@"), "账号格式错误", "");
            AssertUtil.CheckIsNullOrEmpty<SpException>(model.password, "密码不能为空", "");

            return Broker.ExecuteTransaction(() =>
            {
                var authUser = Broker.Retrieve<auth_user>("SELECT * FROM auth_user WHERE lower(code) = lower(@code)", new Dictionary<string, object>() { { "@code", model.code } });
                if (authUser != null)
                {
                    return new AuthUserService(Broker).Login(model.code, model.password, model.publicKey);
                }

                var role = new SysRoleService().GetGuest();
                var user = new user_info() { code = model.code, password = model.password };
                var system = UserIdentityUtil.GetSystem();
                user.Id = Guid.NewGuid().ToString();
                user.name = model.code.Split("@")[0];
                user.avatar = "";
                user.mailbox = model.code;
                user.code = model.code;
                user.roleid = role.Id;
                user.roleidName = role.name;
                user.createdBy = system.Id;
                user.createdByName = system.Name;
                user.createdOn = DateTime.Now;
                user.modifiedBy = system.Id;
                user.modifiedByName = system.Name;
                user.modifiedOn = DateTime.Now;
                user.stateCode = 1;
                user.stateCodeName = "启用";
                Broker.Create(user, false);
                var _authUser = new auth_user()
                {
                    Id = Guid.NewGuid().ToString(),
                    name = user.name,
                    code = user.code,
                    roleid = user.roleid,
                    roleidName = user.roleidName,
                    user_infoid = user.user_infoId,
                    is_lock = false,
                    is_lockName = "否",
                    last_login_time = DateTime.Now,
                    password = RSAUtil.Decrypt(model.password, model.publicKey)
                };
                Broker.Create(_authUser);

                // 返回登录结果、用户信息、用户验证票据信息
                return new LoginResponse
                {
                    result = true,
                    userName = user.code,
                    token = JwtHelper.CreateToken(new JwtTokenModel() { Code = user.code, Name = user.name, Role = user.code, Uid = user.Id }),
                    userId = user.Id,
                    message = "登录成功"
                };
            });
        }
    }
}
