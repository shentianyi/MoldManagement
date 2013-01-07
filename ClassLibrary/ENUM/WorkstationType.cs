using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum WorkstationType
    {
        /// <summary>
        /// 工作台
        /// </summary>
        [Description("工作台")]
        Workstation = 0,
        /// <summary>
        /// 为后台
        /// </summary>
        [Description("维护台")]
        Maintainsation= 1,
        /// <summary>
        /// 实验台
        /// </summary>
        [Description("实验台")]
        Teststation = 2
    }
}
