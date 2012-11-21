using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum WarehouseType
    {
        [Description("唯一件仓")]
        UniqWarehouse = 0,
        [Description("批量件仓")]
        PartWarehouse = 1
    }
}
