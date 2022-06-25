using Sixpence.ORM.Entity;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Blog.WeChat.FocusUser
{
    [Entity("wechat_user", "微信用户")]
    public partial class wechat_user : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 是否关注
        /// </summary>
        [DataMember, Column, Description("是否关注")]
        public int? subscribe { get; set; }

        /// <summary>
        /// openid
        /// </summary>
        [DataMember, Column, Description("openid")]
        public string openid { get; set; }

        /// <summary>
        /// 用户的昵称
        /// </summary>
        [DataMember, Column, Description("用户的昵称")]
        public string nickname { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember, Column, Description("性别")]
        public int? sex { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        [DataMember, Column, Description("语言")]
        public string language { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [DataMember, Column, Description("城市")]
        public string city { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [DataMember, Column, Description("省份")]
        public string province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [DataMember, Column, Description("国家")]
        public string country { get; set; }

        /// <summary>
        /// 用户画像
        /// </summary>
        [DataMember, Column, Description("用户画像")]
        public string headimgurl { get; set; }

        /// <summary>
        /// 关注时间
        /// </summary>
        [DataMember, Column, Description("关注时间")]
        public DateTime? subscribe_time { get; set; }

        /// <summary>
        /// unionid
        /// </summary>
        [DataMember, Column, Description("unionid")]
        public string unionid { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember, Column, Description("备注")]
        public string remark { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        [DataMember, Column, Description("分组ID")]
        public int groupid { get; set; }

        /// <summary>
        /// 关注渠道
        /// </summary>
        [DataMember, Column, Description("关注渠道")]
        public string subscribe_scene { get; set; }

        /// <summary>
        /// 二维码扫码场景
        /// </summary>
        [DataMember, Column, Description("二维码扫码场景")]
        public int? qr_scene { get; set; }

        /// <summary>
        /// 二维码扫码场景描述
        /// </summary>
        [DataMember, Column, Description("二维码扫码场景描述")]
        public string qr_scene_str { get; set; }
    }
}

