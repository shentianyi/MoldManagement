using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ToolingWCF.DataModel
{
    public enum MsgType
    {
        [Description("正确")]
        OK = 0,
        [Description("提醒")]
        Warn = 1,
        [Description("异常")]
        Error = 2
    }
}
