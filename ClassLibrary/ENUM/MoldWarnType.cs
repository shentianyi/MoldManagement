using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum MoldWarnType
    {
        NULL = -1,
        [Description("超期")]
        OutTime = 0,
        [Description("未超期")]
        InTime = 1
    }
}
