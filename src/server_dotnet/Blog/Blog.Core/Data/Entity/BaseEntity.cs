using Sixpence.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Blog.Core.Data
{
    [DataContract]
    [Serializable]
    public abstract class BaseEntity : IEntity
    {
        private static readonly ConcurrentDictionary<string, string> _entityNameCache = new ConcurrentDictionary<string, string>();
        public BaseEntity() { }
        public BaseEntity(string entityName) { this._entityName = entityName; }

        /// <summary>
        /// 实体名
        /// </summary>
        private string _entityName;
        public string EntityName
        {
            get
            {
                if (string.IsNullOrEmpty(_entityName))
                {
                    var type = GetType();
                    _entityName = _entityNameCache.GetOrAdd(type.FullName, (key) =>
                    {
                        var attr = Attribute.GetCustomAttribute(type, typeof(EntityAttribute)) as EntityAttribute;
                        if (attr == null)
                        {
                            throw new SpException("获取实体名失败，请检查是否定义实体名", "");
                        }
                        return attr.Name;
                    });
                }
                return _entityName;
            }
            set
            {
                _entityName = value;
            }
        }

        /// <summary>
        /// 是否是系统实体
        /// </summary>
        public bool IsSystemEntity()
        {
            var attribute = Attribute.GetCustomAttribute(GetType(), typeof(EntityAttribute)) as EntityAttribute;
            return attribute != null && attribute.IsSystemEntity;
        }

        /// <summary>
        /// 主键名
        /// </summary>
        private string _mainKeyName;
        public string MainKeyName { get { return this._mainKeyName ?? EntityName + "id"; } set { _mainKeyName = value; } }

        #region 实体基础字段
        /// <summary>
        ///  实体id
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember, Attr("name", "名称", AttrType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember, Attr("createdby", "创建人id", AttrType.Varchar, 100, true)]
        public string createdBy { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember, Attr("createdbyname", "创建人名称", AttrType.Varchar, 100, true)]
        public string createdByName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember, Attr("createdon", "创建日期", AttrType.Timestamp, 6, true)]
        public DateTime? createdOn { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [DataMember, Attr("modifiedby", "修改人id", AttrType.Varchar, 100, true)]
        public string modifiedBy { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [DataMember, Attr("modifiedbyname", "修改人名称", AttrType.Varchar, 100, true)]
        public string modifiedByName { get; set; }


        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember, Attr("modifiedon", "创建日期", AttrType.Timestamp, 6, true)]
        public DateTime? modifiedOn { get; set; }

        #endregion

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get => GetAttributeValue(key);
            set => SetAttributeValue(key, value);
        }

        #region Methods
        public IEnumerable<string> GetKeys()
        {
            return this.GetType().GetProperties().Select(item => item.Name);
        }

        public bool ContainKey(string name)
        {
            return GetKeys().Contains(name);
        }

        public IEnumerable<object> GetValues()
        {
            return this.GetType().GetProperties().Select(item => item.GetValue(this));
        }

        public IDictionary<string, object> GetAttributes()
        {
            var attributes = new Dictionary<string, object>();
            this.GetType()
                .GetProperties()
                .Where(item => item.IsDefined(typeof(AttrAttribute), false))
                .ToList().ForEach(item =>
            {
                attributes.Add(item.Name, item.GetValue(this));
            });
            return attributes;
        }

        public object GetAttributeValue(string name)
        {
            if (ContainKey(name))
            {
                return this.GetType().GetProperty(name).GetValue(this);
            }
            return null;
        }

        public T GetAttributeValue<T>(string name) where T : class
        {
            if (ContainKey(name))
            {
                return this.GetType().GetProperty(name).GetValue(this) as T;
            }
            return null;
        }

        public void SetAttributeValue(string name, object value)
        {
            if (ContainKey(name))
            {
                this.GetType().GetProperty(name).SetValue(this, value);
            }
        }

        /// <summary>
        /// 获取实体名
        /// </summary>
        /// <returns></returns>
        public string GetEntityName()
        {
            return this.EntityName;
        }

        /// <summary>
        /// 获取实体所有字段
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Column> GetAttrs()
        {
            return this.GetType()
                .GetProperties()
                .Where(item => item.IsDefined(typeof(AttrAttribute), false))
                ?.Select(item => (item.GetCustomAttributes(typeof(AttrAttribute), false).FirstOrDefault() as AttrAttribute).Attr);
        }

        /// <summary>
        /// 获取逻辑名
        /// </summary>
        /// <returns></returns>
        public string GetLogicalName()
        {
            var attr = Attribute.GetCustomAttribute(GetType(), typeof(EntityAttribute)) as EntityAttribute;
            if (attr == null)
            {
                return string.Empty;
            }
            return attr.LogicalName;
        }
        #endregion

    }
}
