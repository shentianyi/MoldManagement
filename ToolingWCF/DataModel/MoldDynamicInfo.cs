using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ToolingWCF.DataModel
{
    [DataContract]
    public class MoldDynamicInfo : MoldBaseInfo
    {
        [DataMember]
        public string Operator { get; set; }
        [DataMember]
        public string OperateTime { get; set; }
        [DataMember]
        public int? AllowedCuttedTime { get; set; }
        [DataMember]
        public int? CurrentCuttedTime { get; set; }
        [DataMember]
        public int? ReleaseCycle { get; set; }
        [DataMember]
        public string LastReleasedTime { get; set; }
        [DataMember]
        public string LastMantainTime { get; set; }
        [DataMember]
        public int? MantainCycle { get; set; }
    }
}
