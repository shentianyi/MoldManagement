using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;
using System.Runtime.Serialization;
using ClassLibrary.ENUM;

namespace ToolingWCF.DataModel
{
    [DataContract]
    public class MoldReleaseInfo
    {
        [DataMember]
        public string TesterName { get; set; }
        [DataMember]
        public string TesterNR { get; set; }
        [DataMember]
        public string TargetNR { get; set; }
        [DataMember]
        public ReportType ReportType { get; set; }
        [DataMember]
        public string ReportTypeCN { get; set; }
        [DataMember]
        public DateTime? Date { get; set; }
        [DataMember]
        public List<Attachment> Attach { get; set; }
    }
}
