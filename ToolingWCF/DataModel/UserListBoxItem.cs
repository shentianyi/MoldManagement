using System.Runtime.Serialization;

namespace ToolingWCF.DataModel
{
    [DataContract]
    public class UserListBoxItem
    {
        [DataMember]
        public string Display { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}
