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
    public class MoldBaseInfo
    {
        [DataMember]
        public string MoldNR { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Producer { get; set; }
        [DataMember]
        public string Weight { get; set; }
        [DataMember]
        public string Material { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public MoldStateType State { get; set; }
        [DataMember]
        public string StateCN { get; set; }
        [DataMember]
        public string ProjectId { get; set; }
        [DataMember]
        public string ProjectName { get; set; }
        [DataMember]
        public string CurrentPosition { get; set; }
        [DataMember]
        public string FitKnife { get; set; }
        [DataMember]
        public string FitConnector { get; set; }
        [DataMember]
        public List<Attachment> Attach { get; set; }
    }
}
