using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum MoldStateType
    {
        NULL = -1, 
        [Description("未归还")]
        NotReturned = 0, 
        //[Description("已归还")]
        //Returned = 1,
        [Description("正常")]
        Normal =1,
        [Description("需维护")]
        NeedMantain = 2,
        [Description("已破损")]
        Broken = 3
    }
}
