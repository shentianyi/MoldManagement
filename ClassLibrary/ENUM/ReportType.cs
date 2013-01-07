using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum ReportType
    { 
        /// <summary>
        /// 维护状态
        /// </summary>
        [Description("维护")]
        MaintainReport = 0,
        /// <summary>
        /// 实验
        /// </summary>
        [Description("实验")]
        TestReport = 1,
    }
}
