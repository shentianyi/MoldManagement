using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum WorkstationType
    {
        [Description("工作台")]
        Workstation = 0,
        [Description("维修台")]
        Maintainsation= 1,
        [Description("实验台")]
        Teststation = 2
    }
}
