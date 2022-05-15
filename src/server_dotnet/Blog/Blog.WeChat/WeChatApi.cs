using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Blog.Core.WebApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Blog.Core;
using Sixpence.Common.Utils;
using Sixpence.Common;

namespace Blog.WeChat
{
    internal static class WeChatApi
    {
        public static readonly string BaseUrl = WeChatApiConfig.GetValue("Url");

        /// <summary>
        /// 获取微信Token
        /// </summary>
        public static (string AccessToken, int Expire) GetAccessToken(string appid, string secret)
        {
            var url = string.Format(WeChatApiConfig.GetValue("GetAccessTokenApi"), appid, secret);
            var resp = HttpUtil.Get(url);
            var respJson = JObject.Parse(resp);
            AssertUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "获取微信授权失败", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            return (respJson.GetValue("access_token").ToString(), Convert.ToInt32(respJson.GetValue("expires_in").ToString()));
        }

        public static void CheckWeChatErrorResponse(JObject respJson, string message)
        {
            AssertUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", message, "522F326C-D739-47E4-8824-271582DEEBC6");
        }

        #region 微信素材
        /// <summary>
        /// 批量获取微信素材
        /// </summary>
        public static string BatchGetMaterial(string type, int pageIndex, int pageSize)
        {
            var url = string.Format(WeChatApiConfig.GetValue("BatchGetMaterialApi"), WeChatService.AccessToken);
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

        /// <summary>
        /// 获取微信素材
        /// </summary>
        public static string GetMaterial(string media_id)
        {
            var result = HttpUtil.Post(string.Format(WeChatApiConfig.GetValue("GetMaterialApi"), WeChatService.AccessToken), JsonConvert.SerializeObject(new
            {
                media_id = media_id
            }));
            var resultJson = JObject.Parse(result);

            AssertUtil.CheckBoolean<SpException>(resultJson.GetValue("errcode") != null && resultJson.GetValue("errcode").ToString() != "0", "获取微信素材失败", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            return result;
        }

        /// <summary>
        /// 新增永久素材API（图文）
        /// </summary>
        /// <param name="postData">参考： https://developers.weixin.qq.com/doc/offiaccount/Asset_Management/Adding_Permanent_Assets.html</param>
        public static WeChatAddNewsResponse AddNews(string postData)
        {
            var url = string.Format(WeChatApiConfig.GetValue("AddNewsApi"), WeChatService.AccessToken);
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
        /// 上传图文消息素材
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static WeChatSuccessUploadResponse UploadImg(Stream stream, string fileName, string contentType)
        {
            var url = string.Format(WeChatApiConfig.GetValue("UploadImage"), WeChatService.AccessToken);
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
            var url = string.Format(WeChatApiConfig.GetValue("AddMaterialAPi"), WeChatService.AccessToken, type.ToMaterialTypeString());
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

        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="model"></param>
        public static void UpdateNews(string postData)
        {
            var url = string.Format(WeChatApiConfig.GetValue("UpdateNewsApi"), WeChatService.AccessToken);
            var result = HttpUtil.Post(url, postData);
            var resultJson = JObject.Parse(result);
            if (resultJson.GetValue("errcode") != null && resultJson.GetValue("errcode").ToString() != "0")
            {
                var error = JsonConvert.DeserializeObject<WeChatErrorResponse>(result);
                throw new SpException("修改素材失败：" + error.errmsg);
            }
        }

        /// <summary>
        /// 删除素材
        /// </summary>
        /// <param name="mediaId"></param>
        public static void DeleteMaterial(string mediaId)
        {
            var url = string.Format(WeChatApiConfig.GetValue("DeleteMaterialApi"), WeChatService.AccessToken);
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

        #region 用户
        /// <summary>
        /// 获取关注用户列表
        /// <para>next_openid（当公众号关注者数量超过10000时，可通过填写next_openid的值，从而多次拉取列表的方式来满足需求。）</para>
        /// </summary>
        public static string GetFocusUserList(string nextOpenId = "")
        {
            var resp = HttpUtil.Get(string.Format(WeChatApiConfig.GetValue("GetFocusUserListApi"), WeChatService.AccessToken, nextOpenId));
            var respJson = JObject.Parse(resp);
            AssertUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "获取关注用户列表失败", "C84A4B94-2B34-4F9C-9B85-9A260F8F9F98");
            return resp;
        }

        /// <summary>
        /// 获取关注用户基本信息
        /// </summary>
        public static string GetFocusUser(string openId, string lang = "zh_CN")
        {
            var resp = HttpUtil.Get(string.Format(WeChatApiConfig.GetValue("GetFocusUserApi"), WeChatService.AccessToken, openId, lang));
            var respJson = JObject.Parse(resp);
            AssertUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "获取关注用户基本信息失败", "4F52B0D5-0C83-492B-B420-373DB156229E");
            return resp;
        }

        /// <summary>
        /// 批量获取关注用户基本信息
        /// </summary>
        public static string BatchGetFocusUser(string postData)
        {
            var resp = HttpUtil.Post(string.Format(WeChatApiConfig.GetValue("BatchGetFocusUserApi"), WeChatService.AccessToken), postData);
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
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public static string GetMenu()
        {
            var resp = HttpUtil.Get(string.Format(WeChatApiConfig.GetValue("GetMenuApi"), WeChatService.AccessToken));
            CheckWeChatErrorResponse(JObject.Parse(resp), "获取微信菜单失败");
            return resp;
        }

        /// <summary>
        /// 删除微信菜单
        /// </summary>
        /// <param name="postData"></param>
        public static void DeleteMenu()
        {
            var resp = HttpUtil.Get(string.Format(WeChatApiConfig.GetValue("DeleteMenuApi"), WeChatService.AccessToken));
            CheckWeChatErrorResponse(JObject.Parse(resp), "删除微信菜单失败");
        }
        #endregion
    }
}