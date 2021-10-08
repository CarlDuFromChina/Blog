using Sixpence.EntityFramework.Entity;
using System;
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
        [Attr("wechat_userid", "实体id", AttrType.Varchar, 100)]
        public string wechat_userId
        {
            get
            {
                return this.Id;
            }
            set
            {
                this.Id = value;
            }
        }

        /// <summary>
        /// 是否关注
        /// </summary>
        [DataMember]
        [Attr("subscribe", "是否关注", AttrType.Int4)]
        public int? subscribe { get; set; }

        /// <summary>
        /// openid
        /// </summary>
        [DataMember]
        [Attr("openid", "openid", AttrType.Varchar, 100)]
        public string openid { get; set; }

        /// <summary>
        /// 用户的昵称
        /// </summary>
        [DataMember]
        [Attr("nickname", "用户的昵称", AttrType.Varchar, 100)]
        public string nickname { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        [Attr("sex", "性别", AttrType.Int4)]
        public int? sex { get; set; }


        /// <summary>
        /// 语言
        /// </summary>
        [DataMember]
        [Attr("language", "语言", AttrType.Varchar, 100)]
        public string language { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [DataMember]
        [Attr("city", "城市", AttrType.Varchar, 100)]
        public string city { get; set; }


        /// <summary>
        /// 省份
        /// </summary>
        [DataMember]
        [Attr("province", "省份", AttrType.Varchar, 100)]
        public string province { get; set; }


        /// <summary>
        /// 国家
        /// </summary>
        [DataMember]
        [Attr("country", "国家", AttrType.Varchar, 100)]
        public string country { get; set; }

        /// <summary>
        /// 用户画像
        /// </summary>
        [DataMember]
        [Attr("headimgurl", "用户画像", AttrType.Varchar, 400)]
        public string headimgurl { get; set; }

        /// <summary>
        /// 关注时间
        /// </summary>
        [DataMember]
        [Attr("subscribe_time", "关注时间", AttrType.Timestamp)]
        public DateTime? subscribe_time { get; set; }

        /// <summary>
        /// unionid
        /// </summary>
        [DataMember]
        [Attr("unionid", "unionid", AttrType.Varchar, 100)]
        public string unionid { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        [Attr("remark", "备注", AttrType.Text)]
        public string remark { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        [DataMember]
        [Attr("groupid", "分组ID", AttrType.Varchar, 100)]
        public int groupid { get; set; }


        /// <summary>
        /// 关注渠道
        /// </summary>
        [DataMember]
        [Attr("subscribe_scene", "关注渠道", AttrType.Varchar, 100)]
        public string subscribe_scene { get; set; }

        /// <summary>
        /// 二维码扫码场景
        /// </summary>
        [DataMember]
        [Attr("qr_scene", "二维码扫码场景", AttrType.Int4)]
        public int? qr_scene { get; set; }


        /// <summary>
        /// 二维码扫码场景描述
        /// </summary>
        [DataMember]
        [Attr("qr_scene_str", "二维码扫码场景描述", AttrType.Varchar, 100)]
        public string qr_scene_str { get; set; }
    }
}

