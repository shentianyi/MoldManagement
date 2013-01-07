using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum MoldReturnStateType
    { 
        /// <summary>
        /// 正常状态
        /// </summary>
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 需维护状态
        /// </summary>
        [Description("需维护")]
        NeedMantain = 2,
        /// <summary>
        /// 已破损状态
        /// </summary>
        [Description("已破损")]
        Broken = 3
    }
}
