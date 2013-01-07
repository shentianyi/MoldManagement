using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum WarehouseType
    { 
        /// <summary>
        /// 唯一件仓库
        /// </summary>
        [Description("唯一件仓")]
        UniqWarehouse = 0,
        /// <summary>
        /// 批量件仓库
        /// </summary>
        [Description("批量件仓")]
        PartWarehouse = 1
    }
}
