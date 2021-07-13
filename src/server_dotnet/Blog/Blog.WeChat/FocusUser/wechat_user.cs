using Blog.Core.Data;
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
        private int? _subscribe;
        [DataMember]
        [Attr("subscribe", "是否关注", AttrType.Int4)]
        public int? subscribe
        {
            get
            {
                return this._subscribe;
            }
            set
            {
                this._subscribe = value;
                SetAttributeValue("subscribe", value);
            }
        }


        /// <summary>
        /// openid
        /// </summary>
        private string _openid;
        [DataMember]
        [Attr("openid", "openid", AttrType.Varchar, 100)]
        public string openid
        {
            get
            {
                return this._openid;
            }
            set
            {
                this._openid = value;
                SetAttributeValue("openid", value);
            }
        }


        /// <summary>
        /// 用户的昵称
        /// </summary>
        private string _nickname;
        [DataMember]
        [Attr("nickname", "用户的昵称", AttrType.Varchar, 100)]
        public string nickname
        {
            get
            {
                return this._nickname;
            }
            set
            {
                this._nickname = value;
                SetAttributeValue("nickname", value);
            }
        }


        /// <summary>
        /// 性别
        /// </summary>
        private int? _sex;
        [DataMember]
        [Attr("sex", "性别", AttrType.Int4)]
        public int? sex
        {
            get
            {
                return this._sex;
            }
            set
            {
                this._sex = value;
                SetAttributeValue("sex", value);
            }
        }


        /// <summary>
        /// 语言
        /// </summary>
        private string _language;
        [DataMember]
        [Attr("language", "语言", AttrType.Varchar, 100)]
        public string language
        {
            get
            {
                return this._language;
            }
            set
            {
                this._language = value;
                SetAttributeValue("language", value);
            }
        }


        /// <summary>
        /// 城市
        /// </summary>
        private string _city;
        [DataMember]
        [Attr("city", "城市", AttrType.Varchar, 100)]
        public string city
        {
            get
            {
                return this._city;
            }
            set
            {
                this._city = value;
                SetAttributeValue("city", value);
            }
        }


        /// <summary>
        /// 省份
        /// </summary>
        private string _province;
        [DataMember]
        [Attr("province", "省份", AttrType.Varchar, 100)]
        public string province
        {
            get
            {
                return this._province;
            }
            set
            {
                this._province = value;
                SetAttributeValue("province", value);
            }
        }


        /// <summary>
        /// 国家
        /// </summary>
        private string _country;
        [DataMember]
        [Attr("country", "国家", AttrType.Varchar, 100)]
        public string country
        {
            get
            {
                return this._country;
            }
            set
            {
                this._country = value;
                SetAttributeValue("country", value);
            }
        }


        /// <summary>
        /// 用户画像
        /// </summary>
        private string _headimgurl;
        [DataMember]
        [Attr("headimgurl", "用户画像", AttrType.Varchar, 400)]
        public string headimgurl
        {
            get
            {
                return this._headimgurl;
            }
            set
            {
                this._headimgurl = value;
                SetAttributeValue("headimgurl", value);
            }
        }


        /// <summary>
        /// 关注时间
        /// </summary>
        private DateTime? _subscribe_time;
        [DataMember]
        [Attr("subscribe_time", "关注时间", AttrType.Timestamp)]
        public DateTime? subscribe_time
        {
            get
            {
                return this._subscribe_time;
            }
            set
            {
                this._subscribe_time = value;
                SetAttributeValue("subscribe_time", value);
            }
        }


        /// <summary>
        /// unionid
        /// </summary>
        private string _unionid;
        [DataMember]
        [Attr("unionid", "unionid", AttrType.Varchar, 100)]
        public string unionid
        {
            get
            {
                return this._unionid;
            }
            set
            {
                this._unionid = value;
                SetAttributeValue("unionid", value);
            }
        }


        /// <summary>
        /// 备注
        /// </summary>
        private string _remark;
        [DataMember]
        [Attr("remark", "备注", AttrType.Text)]
        public string remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
                SetAttributeValue("remark", value);
            }
        }


        /// <summary>
        /// 分组ID
        /// </summary>
        private int _groupid;
        [DataMember]
        [Attr("groupid", "分组ID", AttrType.Varchar, 100)]
        public int groupid
        {
            get
            {
                return this._groupid;
            }
            set
            {
                this._groupid = value;
                SetAttributeValue("groupid", value);
            }
        }


        /// <summary>
        /// 关注渠道
        /// </summary>
        private string _subscribe_scene;
        [DataMember]
        [Attr("subscribe_scene", "关注渠道", AttrType.Varchar, 100)]
        public string subscribe_scene
        {
            get
            {
                return this._subscribe_scene;
            }
            set
            {
                this._subscribe_scene = value;
                SetAttributeValue("subscribe_scene", value);
            }
        }


        /// <summary>
        /// 二维码扫码场景
        /// </summary>
        private int? _qr_scene;
        [DataMember]
        [Attr("qr_scene", "二维码扫码场景", AttrType.Int4)]
        public int? qr_scene
        {
            get
            {
                return this._qr_scene;
            }
            set
            {
                this._qr_scene = value;
                SetAttributeValue("qr_scene", value);
            }
        }


        /// <summary>
        /// 二维码扫码场景描述
        /// </summary>
        private string _qr_scene_str;
        [DataMember]
        [Attr("qr_scene_str", "二维码扫码场景描述", AttrType.Varchar, 100)]
        public string qr_scene_str
        {
            get
            {
                return this._qr_scene_str;
            }
            set
            {
                this._qr_scene_str = value;
                SetAttributeValue("qr_scene_str", value);
            }
        }

    }
}

