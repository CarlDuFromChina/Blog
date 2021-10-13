using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Models
{
    /// <summary>
    /// 字段
    /// </summary>
    public class Column
    {
        public string Name { get; set; }
        public string LogicalName { get; set; }
        public DataType Type { get; set; }
        public int? Length { get; set; }
        public bool? IsRequire { get; set; }
    }
}
