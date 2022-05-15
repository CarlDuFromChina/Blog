using Sixpence.ORM.Entity;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 是否关注
        /// </summary>
        [DataMember]
        [Column("subscribe", "是否关注", DataType.Int4)]
        public int? subscribe { get; set; }

        /// <summary>
        /// openid
        /// </summary>
        [DataMember]
        [Column("openid", "openid", DataType.Varchar, 100)]
        public string openid { get; set; }

        /// <summary>
        /// 用户的昵称
        /// </summary>
        [DataMember]
        [Column("nickname", "用户的昵称", DataType.Varchar, 100)]
        public string nickname { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        [Column("sex", "性别", DataType.Int4)]
        public int? sex { get; set; }


        /// <summary>
        /// 语言
        /// </summary>
        [DataMember]
        [Column("language", "语言", DataType.Varchar, 100)]
        public string language { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [DataMember]
        [Column("city", "城市", DataType.Varchar, 100)]
        public string city { get; set; }


        /// <summary>
        /// 省份
        /// </summary>
        [DataMember]
        [Column("province", "省份", DataType.Varchar, 100)]
        public string province { get; set; }


        /// <summary>
        /// 国家
        /// </summary>
        [DataMember]
        [Column("country", "国家", DataType.Varchar, 100)]
        public string country { get; set; }

        /// <summary>
        /// 用户画像
        /// </summary>
        [DataMember]
        [Column("headimgurl", "用户画像", DataType.Varchar, 400)]
        public string headimgurl { get; set; }

        /// <summary>
        /// 关注时间
        /// </summary>
        [DataMember]
        [Column("subscribe_time", "关注时间", DataType.Timestamp)]
        public DateTime? subscribe_time { get; set; }

        /// <summary>
        /// unionid
        /// </summary>
        [DataMember]
        [Column("unionid", "unionid", DataType.Varchar, 100)]
        public string unionid { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        [Column("remark", "备注", DataType.Text)]
        public string remark { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        [DataMember]
        [Column("groupid", "分组ID", DataType.Varchar, 100)]
        public int groupid { get; set; }


        /// <summary>
        /// 关注渠道
        /// </summary>
        [DataMember]
        [Column("subscribe_scene", "关注渠道", DataType.Varchar, 100)]
        public string subscribe_scene { get; set; }

        /// <summary>
        /// 二维码扫码场景
        /// </summary>
        [DataMember]
        [Column("qr_scene", "二维码扫码场景", DataType.Int4)]
        public int? qr_scene { get; set; }


        /// <summary>
        /// 二维码扫码场景描述
        /// </summary>
        [DataMember]
        [Column("qr_scene_str", "二维码扫码场景描述", DataType.Varchar, 100)]
        public string qr_scene_str { get; set; }
    }
}

