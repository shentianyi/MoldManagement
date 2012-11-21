using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum ReportType
    {
        [Description("维护")]
        MaintainReport = 0,
        [Description("实验")]
        TestReport = 1,
    }
}
