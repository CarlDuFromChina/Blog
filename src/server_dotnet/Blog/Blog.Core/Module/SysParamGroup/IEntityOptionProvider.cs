using System;
using System.Collections.Generic;
using Sixpence.Common;
using Sixpence.Common.IoC;
using Sixpence.EntityFramework.SelectOption;

namespace Blog.Core.Module.SysParamGroup
{
    [ServiceRegister]
    public interface IEntityOptionProvider
    {
        IEnumerable<SelectOption> GetOptions();
    }
}
