using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum PositionPrefixType
    {
        /// <summary>
        /// 工作台前缀
        /// </summary>
        [Description("工作台：")]
        Workstation = 0,
        /// <summary>
        /// 维护台前缀
        /// </summary>
        [Description("维护台：")]
        Mantainstation = 1,
        /// <summary>
        /// 实验台前缀
        /// </summary>
        [Description("实验台：")]
        Teststation = 2,
        /// <summary>
        /// 库位前缀
        /// </summary>
        [Description("库位：")]
        InPosition = 3
    }
}
