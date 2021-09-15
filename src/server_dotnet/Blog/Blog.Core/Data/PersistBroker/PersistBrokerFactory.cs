using Blog.Core.Config;
using Blog.Core.Data.Driver;
using Sixpence.Core;
using Sixpence.Core.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Data
{
    /// <summary>
    /// 持久化存储工厂类
    /// </summary>
    public class PersistBrokerFactory
    {
        /// <summary>
        /// 缓存数据库链接字符串，避免重复读取开销
        /// </summary>
        private static readonly ConcurrentDictionary<DbType, DBNode> dbDic = new ConcurrentDictionary<DbType, DBNode>();

        /// <summary>
        /// 获取Broker
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IPersistBroker GetPersistBroker(DbType type = DbType.Main)
        {
            var node = dbDic.GetOrAdd(type, key =>
            {
                var config = DBSourceConfig.Config;
                AssertUtil.CheckBoolean<SpException>(config == null || config.Main == null, "未找到数据库配置", "AD4BC4F2-CF8D-4A4E-ACE8-F68EBD89DE42");
                DBNode dbConfig;
                switch (type)
                {
                    case Config.DbType.Main:
                        dbConfig = config.Main;
                        break;
                    case Config.DbType.StandBy:
                        dbConfig = config.StandBy;
                        break;
                    default:
                        throw new NotSupportedException();
                }
                AssertUtil.CheckIsNullOrEmpty<SpException>(dbConfig.Value, "数据库连接字符串为空", "AD4BC4F2-CF8D-4A4E-ACE8-F68EBD89DE42");
                AssertUtil.CheckBoolean<SpException>(!Enum.TryParse<DriverType>(dbConfig.DriverType, out var driverType), "数据库类型错误", "AD4BC4F2-CF8D-4A4E-ACE8-F68EBD89DE42");
                return new DBNode() { Value = DecryptAndEncryptHelper.AESDecrypt(dbConfig.Value), DriverType = driverType.ToString() };
            });

            return new PersistBroker(node.Value, Enum.Parse<DriverType>(node.DriverType));
        }

        /// <summary>
        /// 获取Broker新实例
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="isEncrypted">是否加密（仅支持AES）</param>
        /// <returns></returns>
        public static IPersistBroker GetPersistBroker(string connectionString, bool isEncrypted = false, DriverType Driver = DriverType.Postgresql)
        {
            AssertUtil.CheckIsNullOrEmpty<SpException>(connectionString, "数据库连接字符串为空", "AD4BC4F2-CF8D-4A4E-ACE8-F68EBD89DE42");
            if (isEncrypted)
            {
                return new PersistBroker(DecryptAndEncryptHelper.AESDecrypt(connectionString), Driver);
            }
            return new PersistBroker(connectionString, Driver);
        }
    }
}
