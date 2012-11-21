using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Utilities;

namespace ClassLibrary.Data
{
    public partial class ReportView
    {
        private string reportTypeCN;

        public string ReportTypeCN
        {
            get
            {
                this.reportTypeCN = EnumUtil.GetEnumDescriptionByEnumValue(this.ReportType);
                return reportTypeCN;
            }
        }
    }
}
