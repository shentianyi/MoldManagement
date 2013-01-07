using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum StorageRecordType
    { 
        /// <summary>
        /// 正常生产记录
        /// </summary>
        [Description("正常生产")]
        Produce = 0,
        /// <summary>
        /// 维护记录
        /// </summary>
        [Description("维护")]
        Mantain = 1,
        /// <summary>
        /// 实验记录
        /// </summary>
        [Description("实验")]
        Test = 2,
        /// <summary>
        /// 入库记录
        /// </summary>
        [Description("入库")]
        InStore = 3,
        /// <summary>
        /// 出库记录
        /// </summary>
        [Description("出库")]
        OutStore=4,
        /// <summary>
        /// 移库记录
        /// </summary>
        [Description("移库")]
        MoveStore=5,
        /// <summary>
        /// 报废记录
        /// </summary>
         [Description("报废")]
        ScrapStore=6,
         /// <summary>
         /// 归还记录
         /// </summary>
        [Description("归还")]
        Return=7,
        /// <summary>
        /// 移动工作台记录
        /// </summary>
        [Description("移动工作台")]
        MoveWStation=8
    }
}
