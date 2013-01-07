using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum MoldWarnType
    { 
        /// <summary>
        /// 占位符
        /// </summary>
        NULL = -1,
        /// <summary>
        /// 超期状态
        /// </summary>
        [Description("超期")]
        OutTime = 0,
        /// <summary>
        /// 未超期状态
        /// </summary>
        [Description("未超期")]
        InTime = 1
    }
}
