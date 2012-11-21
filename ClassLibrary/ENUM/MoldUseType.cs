using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum MoldUseType
    {
        [Description("正常生产")]
        Produce = 0, // 0 the mold is applied for producing
        [Description("维修")]
        Mantain = 1, // 1 the mold is applied for repairing
        [Description("实验")]
        Test = 2 // 2 the mold is applied for testing
    }
}
