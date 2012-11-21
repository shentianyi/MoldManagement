using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum StorageRecordType
    {
        [Description("正常生产")]
        Produce = 0, 
        [Description("维修")]
        Mantain = 1, 
        [Description("实验")]
        Test = 2, 
        [Description("入库")]
        InStore = 3,
        [Description("出库")]
        OutStore=4,
        [Description("移库")]
        MoveStore=5,
         [Description("报废")]
        ScrapStore=6,
        [Description("归还")]
        Return=7,
        [Description("移动工作台")]
        MoveWStation=8
    }
}
