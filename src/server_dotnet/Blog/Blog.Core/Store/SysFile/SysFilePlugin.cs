using Blog.Core.Config;
using Blog.Core.Data;
using Sixpence.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Store.SysFile
{
    public class SysFilePlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
        {
            var entity = context.Entity;
            switch (context.Action)
            {
                case EntityAction.PreCreate:
                    break;
                case EntityAction.PostCreate:
                    break;
                case EntityAction.PreUpdate:
                    break;
                case EntityAction.PostUpdate:
                    break;
                case EntityAction.PreDelete:
                    break;
                case EntityAction.PostDelete:
                    var dataList = context.Broker.RetrieveMultiple<sys_file>(@"
SELECT
	* 
FROM
	sys_file 
WHERE
	hash_code = @hash_code 
	AND sys_fileid <> @Id
", new Dictionary<string, object>() { { "@hash_code", entity.GetAttributeValue<string>("hash_code ") }, { "@id", entity.GetAttributeValue<string>("Id ") } });
                    if (dataList == null || dataList.Count == 0)
                    {
                        var config = StoreConfig.Config;
                        ServiceContainer.Resolve<IStoreStrategy>(config.Type).Delete(new List<string>() { entity.GetAttributeValue<string>("name") });
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
