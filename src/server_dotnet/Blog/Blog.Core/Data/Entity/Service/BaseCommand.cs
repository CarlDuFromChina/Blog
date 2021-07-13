using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Data
{
    public class BaseEntityCommand<E>
        where E : BaseEntity, new()
    {
        #region 构造函数
        public BaseEntityCommand()
        {
            Broker = PersistBrokerFactory.GetPersistBroker();
        }
        public BaseEntityCommand(IPersistBroker broker)
        {
            Broker = broker;
        }
        #endregion

        public IPersistBroker Broker { get; set; }

        /// <summary>
        /// 创建实体记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Create(E entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                return "";
            }
            var id = Broker.FilteredCreate(entity);
            return id;
        }

        /// <summary>
        /// 创建或更新历史记录
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string CreateOrUpdateData(E entity)
        {
            var id = entity.Id;
            var isExist = GetEntity(id) != null;
            if (isExist)
            {
                Update(entity);
            }
            else
            {
                id = Create(entity);
            }
            return id;
        }

        /// <summary>
        /// 删除历史记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        public void Delete(IEnumerable<string> ids)
        {
            Broker.ExecuteTransaction(() =>
            {
                ids.Each(id =>
                {
                    var data = Broker.Retrieve<E>(id);
                    Broker.FilteredDelete(new E().EntityName, id);
                });
            });
        }


        /// <summary>
        /// 获取实体记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public E GetEntity(string id)
        {
            return Broker.FilteredRetrieve<E>(id);
        }

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        public void Update(E entity)
        {
            if (string.IsNullOrEmpty(entity?.Id))
            {
                return;
            }

            Broker.FiltededUpdate(entity);
        }
    }
}
