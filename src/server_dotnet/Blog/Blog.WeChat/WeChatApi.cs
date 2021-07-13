using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Blog.Core.WebApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Blog.Core;
using Blog.Core.Utils;

namespace Blog.WeChat
{
    internal static class WeChatApi
    {
        public const string BaseUrl = "https://api.weixin.qq.com/";

        public static readonly string GetAccessTokenApi = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        /// <summary>
        /// 获取微信Token
        /// </summary>
        public static (string AccessToken, int Expire) GetAccessToken(string appid, string secret)
        {
            var url = string.Format(GetAccessTokenApi, appid, secret);
            var resp = HttpUtil.Get(url);
            var respJson = JObject.Parse(resp);
            AssertUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "获取微信授权失败", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            return (respJson.GetValue("access_token").ToString(), Convert.ToInt32(respJson.GetValue("expires_in").ToString()));
        }

        public static void CheckWeChatErrorResponse(JObject respJson, string message)
        {
            AssertUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", message, "522F326C-D739-47E4-8824-271582DEEBC6");
        }

        #region 批量获取微信素材
        /// <summary>
        /// 批量获取微信素材API
        /// </summary>
        public static readonly string BatchGetMaterialApi = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}";

        /// <summary>
        /// 批量获取微信素材
        /// </summary>
        public static string BatchGetMaterial(string type, int pageIndex, int pageSize)
        {
            var url = string.Format(BatchGetMaterialApi, WeChatService.AccessToken);
            var postData = new
            {
                type,
                offset = (pageIndex - 1) * pageSize,
                count = pageSize
            };
            var result = HttpUtil.Post(url, JsonConvert.SerializeObject(postData));
            var resultJson = JObject.Parse(result);

            AssertUtil.CheckBoolean<SpException>(resultJson.GetValue("errcode") != null && resultJson.GetValue("errcode").ToString() != "0", "获取微信素材失败", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            return result;
        }
        #endregion

        /// <summary>
        /// 发送微信消息
        /// </summary>
        public static readonly string SendMessage = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";

        #region 获取微信素材
        /// <summary>
        /// 获取微信素材API
        /// </summary>
        public static readonly string GetMaterialApi = "https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={0}";
        /// <summary>
        /// 获取微信素材
        /// </summary>
        public static string GetMaterial(string media_id)
        {
            var result = HttpUtil.Post(string.Format(GetMaterialApi, WeChatService.AccessToken), JsonConvert.SerializeObject(new
            {
                media_id = media_id
            }));
            var resultJson = JObject.Parse(result);

            AssertUtil.CheckBoolean<SpException>(resultJson.GetValue("errcode") != null && resultJson.GetValue("errcode").ToString() != "0", "获取微信素材失败", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            return result;
        }
        #endregion

        #region 新增永久素材
        private static readonly string AddNewsApi = "https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}";
        /// <summary>
        /// 新增永久素材API（图文）
        /// </summary>
        /// <param name="postData">参考： https://developers.weixin.qq.com/doc/offiaccount/Asset_Management/Adding_Permanent_Assets.html</param>
        public static WeChatAddNewsResponse AddNews(string postData)
        {
            var url = string.Format(AddNewsApi, WeChatService.AccessToken);
            var result = HttpUtil.Post(url, postData);
            var resultJson = JObject.Parse(result);
            if (resultJson.GetValue("errcode") != null && resultJson.GetValue("errcode").ToString() != "0")
            {
                var error = JsonConvert.DeserializeObject<WeChatErrorResponse>(result);
                throw new SpException("添加图文素材失败：" + error.errmsg);
            }
            else
            {
                return JsonConvert.DeserializeObject<WeChatAddNewsResponse>(result);
            }
        }

        /// <summary>
        /// 新增永久素材API（音乐、视频、图片）
        /// </summary>
        public static readonly string AddMaterialAPi = "https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}&type={1}";
        /// <summary>
        /// 新增永久素材（音乐、视频、图片）
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static WeChatSuccessUploadResponse AddMaterial(MaterialType type, Stream stream, string fileName, string contentType = "application/octet-stream")
        {
            var url = string.Format(AddMaterialAPi, WeChatService.AccessToken, type.ToMaterialTypeString());
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            var postData = new UploadFile
            {
                Name = "media",
                Filename = fileName,
                ContentType = contentType,
                Data = bytes
            };
            var result = HttpUtil.Post(url, postData);
            var resultJson = JObject.Parse(result);
            if (resultJson.GetValue("errcode") != null && resultJson.GetValue("errcode").ToString() != "0")
            {
                var error = JsonConvert.DeserializeObject<WeChatErrorResponse>(result);
                throw new SpException("添加素材失败：" + error.errmsg);
            }
            else
            {
                return JsonConvert.DeserializeObject<WeChatSuccessUploadResponse>(result);
            }
        }
        #endregion

        #region 修改永久图文素材
        public static readonly string UpdateNewsApi = "https://api.weixin.qq.com/cgi-bin/material/update_news?access_token={0}";
        public static void UpdateNews(WeChatNewsUpdateModel model)
        {
            var result = HttpUtil.Post(string.Format(UpdateNewsApi, WeChatService.AccessToken), JsonConvert.SerializeObject(model));
            var resultJson = JObject.Parse(result);
            if (resultJson.GetValue("errcode") != null && resultJson.GetValue("errcode").ToString() != "0")
            {
                var error = JsonConvert.DeserializeObject<WeChatErrorResponse>(result);
                throw new SpException("修改素材失败：" + error.errmsg);
            }
        }
        #endregion

        #region 删除素材
        /// <summary>
        /// 删除素材Api
        /// </summary>
        public static readonly string DeleteMaterialApi = "https://api.weixin.qq.com/cgi-bin/material/del_material?access_token={0}";
        /// <summary>
        /// 删除素材
        /// </summary>
        /// <param name="mediaId"></param>
        public static void DeleteMaterial(string mediaId)
        {
            var url = string.Format(DeleteMaterialApi, WeChatService.AccessToken);
            var postData = new
            {
                media_id = mediaId
            };
            var result = HttpUtil.Post(url, JsonConvert.SerializeObject(postData));
            var resultJson = JObject.Parse(result);
            if (resultJson.GetValue("errcode") != null && resultJson.GetValue("errcode").ToString() != "0")
            {
                var error = JsonConvert.DeserializeObject<WeChatErrorResponse>(result);
                throw new SpException("删除素材失败：" + error.errmsg);
            }
        }
        #endregion

        #region 获取关注用户列表
        /// <summary>
        /// 获取关注用户列表API
        /// </summary>
        public static readonly string GetFocusUserListApi = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}";

        /// <summary>
        /// 获取关注用户列表
        /// <para>next_openid（当公众号关注者数量超过10000时，可通过填写next_openid的值，从而多次拉取列表的方式来满足需求。）</para>
        /// </summary>
        public static string GetFocusUserList(string nextOpenId = "")
        {
            var resp = HttpUtil.Get(string.Format(GetFocusUserListApi, WeChatService.AccessToken, nextOpenId));
            var respJson = JObject.Parse(resp);
            AssertUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "获取关注用户列表失败", "C84A4B94-2B34-4F9C-9B85-9A260F8F9F98");
            return resp;
        }
        #endregion

        #region 获取关注用户基本信息
        /// <summary>
        /// 获取关注用户基本信息API
        /// </summary>
        public static readonly string GetFocusUserApi = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}";

        /// <summary>
        /// 获取关注用户基本信息
        /// </summary>
        public static string GetFocusUser(string openId, string lang = "zh_CN")
        {
            var resp = HttpUtil.Get(string.Format(GetFocusUserApi, WeChatService.AccessToken, openId, lang));
            var respJson = JObject.Parse(resp);
            AssertUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "获取关注用户基本信息失败", "4F52B0D5-0C83-492B-B420-373DB156229E");
            return resp;
        }
        #endregion

        #region 批量获取关注用户基本信息
        /// <summary>
        /// 批量获取关注用户基本信息API
        /// </summary>
        public static readonly string BatchGetFocusUserApi = "https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={0}";

        /// <summary>
        /// 批量获取关注用户基本信息
        /// </summary>
        public static string BatchGetFocusUser(string postData)
        {
            var resp = HttpUtil.Post(string.Format(BatchGetFocusUserApi, WeChatService.AccessToken), postData);
            var respJson = JObject.Parse(resp);
            AssertUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "批量获取关注用户基本信息失败", "D8E6DEC8-6887-4EAE-B685-9F175C1FB806");
            return resp;
        }
        #endregion

        #region 菜单
        /// <summary>
        /// 创建菜单 Api
        /// </summary>
        public static readonly string CreateMenuApi = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <returns></returns>
        public static string CreateMenu(string postData)
        {
            var resp = HttpUtil.Post(string.Format(CreateMenuApi, WeChatService.AccessToken), postData);
            CheckWeChatErrorResponse(JObject.Parse(resp), "创建菜单失败");
            return resp;
        }

        /// <summary>
        /// 获取菜单 Api
        /// </summary>
        public static readonly string GetMenuApi = "https://api.weixin.qq.com/cgi-bin/get_current_selfmenu_info?access_token={0}";
        
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public static string GetMenu()
        {
            var resp = HttpUtil.Get(string.Format(GetMenuApi, WeChatService.AccessToken));
            CheckWeChatErrorResponse(JObject.Parse(resp), "获取微信菜单失败");
            return resp;
        }

       /// <summary>
       /// 删除微信菜单api
       /// </summary>
        public static readonly string DeleteMenuApi = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}";
        
        /// <summary>
        /// 删除微信菜单
        /// </summary>
        /// <param name="postData"></param>
        public static void DeleteMenu()
        {
            var resp = HttpUtil.Get(string.Format(DeleteMenuApi, WeChatService.AccessToken));
            CheckWeChatErrorResponse(JObject.Parse(resp), "删除微信菜单失败");
        }
        #endregion

    }
}