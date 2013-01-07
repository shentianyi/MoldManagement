using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum MoldStateType
    {
        /// <summary>
        /// 占位符
        /// </summary>
        NULL = -1,
        /// <summary>
        /// 未归还状态
        /// </summary>
        [Description("未归还")]
        NotReturned = 0,
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
