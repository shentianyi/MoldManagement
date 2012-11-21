using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ToolingWCF.DataModel
{
    [DataContract]
    public class ClientSetting
    {
        private string settingGroup;
        private string key;
        private string value;

        [DataMember]
        public string SettingGroup
        {
            get { return settingGroup; }
            set { settingGroup = value; }
        }

        [DataMember]
        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        [DataMember]
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
