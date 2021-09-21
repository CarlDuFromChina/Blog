using System;
using System.Collections.Generic;
using Sixpence.Core;
using Sixpence.EntityFramework.SelectOption;

namespace Blog.Core.Module.SysParamGroup
{
    [ServiceRegister]
    public interface IEntityOptionProvider
    {
        IEnumerable<SelectOption> GetOptions();
    }
}
