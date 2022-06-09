using Blog.Core.Entity;
using Sixpence.Common.IoC;
using System.Collections.Generic;

namespace Blog.Core.Module.SysParamGroup
{
    [ServiceRegister]
    public interface IEntityOptionProvider
    {
        IEnumerable<SelectOption> GetOptions();
    }
}
