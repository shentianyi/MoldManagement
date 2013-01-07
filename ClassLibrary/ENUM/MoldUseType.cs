using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum MoldUseType
    {
        /// <summary>
        /// 正常生产使用
        /// </summary>
        [Description("正常生产")]
        Produce = 0, // 0 the mold is applied for producing
        /// <summary>
        /// 维护使用
        /// </summary>
        [Description("维护")]
        Mantain = 1, // 1 the mold is applied for repairing
        /// <summary>
        /// 实验使用
        /// </summary>
        [Description("实验")]
        Test = 2 // 2 the mold is applied for testing
    }
}
